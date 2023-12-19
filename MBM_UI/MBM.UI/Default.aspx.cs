#region Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MBM.BillingEngine;
using MBM.Entities;
using MBM.Authenticate;
using System.Configuration;
using System.Globalization;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Data.SqlClient;
using MBM.Library;
using CBTSService;
using OfficeOpenXml;
#endregion

public partial class _Default : Page
{

    #region Constants
    /// <summary>
    /// 
    /// </summary>
    public const string DEFAULT_INVOICETYPE_TEXT = "------------------------------------- Select Customer -------------------------------------";
    public const string DEFAULT_INVOICENUMBER_TEXT = "- Invoice Number -";
    public const string DEFAULT_INVOICE_VALUE = "0";
    public const string DEFAULT_INVOICETYPE_TEXTFIELD = "Prefix";
    public const string DEFAULT_INVOICETYPE_VALUEFIELD = "ID";
    public const string DEFAULT_INVOICENUMBER_TEXTFIELD = "InvoiceNumber";
    public const string DEFAULT_INVOICENUMBER_VALUEFIELD = "ID";
    public const string DEFAULT_FileType_TEXTFIELD = "FileType";
    public const string DEFAULT_FileType_VALUEFIELD = "FileTypeId";
    private const string DEFAULT_IMPORT_CURRENCYID = "USD";
    private const string DEFAULT_EXPORT_CURRENCYID = "USD";
    public const string FILETYPE_DB = "EHCS Production Database";
    public const string FILETYPE_SELECT = "Select FileType";
    #endregion

    public int TypeCreated { get; private set; }
    public int MonthCreated { get; private set; }
    public int YearCreated { get; private set; }
    int InvoiceBillingMonth { get; set; }
    int InvoiceBillingYear { get; set; }
    public long InvoiceSnapshotId { get; set; }
    public DateTime? InvoiceBillEndDate { get; set; }
    public DateTime? InvoiceBillStartDate { get; set; }

    private BillingEngine _billing;
    string strConnectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();

    //Automation   
    private bool IsQueryString { get; set; }
    private string InvoiceTypeQS { get; set; }
    private string InvoiceIdQS { get; set; }

    #region Enum ActionTypes
    /// <summary>
    /// 
    /// </summary>
    enum ActionTypes
    {
        AddBillItem = 1,
        DeleteBillItem = 2,
        AddCharge = 3,
        UpdateCharge = 4,
        DeleteCharge = 5,
        AddTelephone = 6,
        DeleteTelephone = 7,
        ChangeTelephone = 8,
    };

    enum CSGActionTypes
    {
        AddService = 1,
        AddServiceFeature = 2,
        RemoveServiceFeature = 3,
        RemoveService = 4
    };
    #endregion

    #region Page_Load
    /// <summary>
    /// Page load event logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Authenticate objAuth = new Authenticate();
            bool blIsValid = false;
            string strLoginUser = string.Empty;
            strLoginUser = GetUserName();

            btnCreateInvoice.Enabled = true;
            blIsValid = ValidateUser(strConnectionString, strLoginUser);


            InvoiceTypeQS = Request.QueryString["customer"];   //Automation 
            InvoiceIdQS = Request.QueryString["invoiceId"];
            if (!string.IsNullOrWhiteSpace(InvoiceTypeQS) && !string.IsNullOrWhiteSpace(InvoiceIdQS))
            {
                IsQueryString = true;
            }

            if (blIsValid)
            {
                if (ddlInvoiceType.Items.Count == 0)
                {
                    LoadInvoiceTypeDDL();
                }
                if (IsQueryString)
                {
                    ddlInvoiceType_SelectedIndexChanged(null, null);
                }
            }

            ClearQueryString();
        }
        catch (Exception ex)
        {
            LogException(ex, "Page_Load");
        }
    }
    #endregion

    private void ClearQueryString()
    {
        System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        isreadonly.SetValue(this.Request.QueryString, false, null);
        this.Request.QueryString.Remove("customer");
        this.Request.QueryString.Remove("invoiceId");
    }

    #region GetUserName
    /// <summary>
    /// Gets logged in user name
    /// </summary>
    /// <returns></returns>
    private string GetUserName()
    {
        string userName = string.Empty;
        try
        {
            //On the deployed server, to get current user, use HttpContext.
            userName = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                //On local machine, to get current user, use WindowsIdentity.
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Unable to retrieve current login user details.");
            }
        }
        catch (Exception ex)
        {
            LogException(ex, "GetUserName");
        }
        return userName;
    }
    #endregion

    #region LoadInvoiceTypeDDL
    /// <summary>
    /// Loads Invoice Type dropdown list
    /// </summary>
    private void LoadInvoiceTypeDDL()
    {
        try
        {
            List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
            invoiceTypeList = InvoiceTypeLists();

            var invoiceTypeappendedList = invoiceTypeList.Select(p => new { DisplayValue = p.ID, DisplayText = p.Prefix + "-" + p.Name });

            ddlInvoiceType.DataTextField = "DisplayText";
            ddlInvoiceType.DataValueField = "DisplayValue";
            ddlInvoiceType.DataSource = invoiceTypeappendedList;
            ddlInvoiceType.DataBind();

            //load new invoice type dropdownlist as both of the dropdownlist will have same list
            LoadNewInvoice(invoiceTypeList);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to fetch Customer data from DB");
            LogException(ex, "LoadInvoiceTypeDDL");
        }
    }
    #endregion

    #region InvoiceTypeLists
    /// <summary>
    /// Gets all the Invoice Types
    /// </summary>
    /// <returns></returns>
    private List<InvoiceType> InvoiceTypeLists()
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();

        try
        {
            Authenticate objAuth = new Authenticate();
            Users objUser = objAuth.AuthenticateUser(strConnectionString, GetUserName());
            _billing = new BillingEngine(strConnectionString);

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator))
                    {
                        invoiceTypeList = _billing.InvoiceBL.GetInvoiceTypesCustomerAdmin(objUser.UserId);
                    }
                    else
                    {
                        invoiceTypeList = _billing.InvoiceBL.GetInvoiceTypes();
                    }
                }
            }
           
           
        }
        catch (Exception ex)
        {
            LogException(ex, "InvoiceTypeLists");
        }

        return invoiceTypeList;
    }
    #endregion

    #region LoadNewInvoice
    /// <summary>
    /// Loads new Invoice creation popup
    /// </summary>
    /// <param name="invoiceTypeList"></param>
    private void LoadNewInvoice(List<InvoiceType> invoiceTypeList)
    {
        try
        {
            ddlInvoiceType.Items.Insert(0, new ListItem(DEFAULT_INVOICETYPE_TEXT, DEFAULT_INVOICE_VALUE));
            var invoiceTypeappendedList = invoiceTypeList.Select(p => new { DisplayValue = p.ID, DisplayText = p.Prefix + "-" + p.Name });

            ddlNewInvoiceType.DataTextField = "DisplayText";
            ddlNewInvoiceType.DataValueField = "DisplayValue";
            ddlNewInvoiceType.DataSource = invoiceTypeappendedList;
            ddlNewInvoiceType.DataBind();

            SetCurrencies(DEFAULT_IMPORT_CURRENCYID, DEFAULT_EXPORT_CURRENCYID);

            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < months.Length - 1; i++)
            {
                ddlNewInvoiceMonth.Items.Add(new ListItem(months[i], i.ToString()));
            }

            var currentYear = DateTime.Today.Year;
            for (int i = 2; i > 0; i--)
            {
                ddlNewInvoiceYear.Items.Add((currentYear - i).ToString());
            }

            for (int i = 0; i <= 2; i++)
            {
                ddlNewInvoiceYear.Items.Add((currentYear + i).ToString());
            }

            DateTime dt = DateTime.Now.AddMonths(-2);
            ddlNewInvoiceMonth.SelectedValue = dt.Month.ToString();
            ddlNewInvoiceYear.SelectedValue = dt.Year.ToString();
        }
        catch (Exception ex)
        {
            LogException(ex, "LoadNewInvoice");
        }
    }
    #endregion

    #region ddlInvoiceType_SelectedIndexChanged
    /// <summary>
    /// Invoice Type dropdown list index change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsQueryString)
        {
            ddlInvoiceType.SelectedValue = InvoiceTypeQS;
        }
        if (ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE)
        {
            List<Invoice> invoiceList = new List<Invoice>();
            _billing = new BillingEngine(strConnectionString);

            int InvoiceType = 0;
            if (IsQueryString)
            {
                InvoiceType = Convert.ToInt32(InvoiceTypeQS);
            }
            else
            {
                InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            }

            invoiceList = _billing.InvoiceBL.FetchInvoicesByType(InvoiceType);

            //1582
            List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);

            invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
            MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();

            objInvoiceType = invoiceTypeList.Where(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString())).FirstOrDefault();

            var billingSystem = string.Empty;
            //SERO-1582
            if (!string.IsNullOrEmpty(objInvoiceType.BillingSystem.ToString()))
            {
                billingSystem = (objInvoiceType.BillingSystem.ToString() == "CRM") ? "CRM" : "CSG Ascendon";
            }

            foreach (Invoice objInvoice in invoiceList.Where(s => s.BillingSystem == billingSystem))
            {
                string strMonthName = string.Empty;
                if (objInvoice.BillingMonth.ToString().Length == 1)
                {
                    strMonthName = "0" + objInvoice.BillingMonth.ToString();
                }
                else
                {
                    strMonthName = objInvoice.BillingMonth.ToString();
                }

                string strYear = objInvoice.BillingYear.ToString().Substring(2, 2);
                objInvoice.InvoiceNumber = objInvoice.InvoiceNumber + " - " + strMonthName + "/" + strYear;
            }

            ddlInvoiceNumber.DataTextField = DEFAULT_INVOICENUMBER_TEXTFIELD;
            ddlInvoiceNumber.DataValueField = DEFAULT_INVOICENUMBER_VALUEFIELD;

            ddlInvoiceNumber.Items.Clear();
            ddlInvoiceNumber.DataSource = null;

            //ER 6775 Start            
            if (!chkInvoiceNumber.Checked)
            {
                ddlInvoiceNumber.DataSource = invoiceList.Take(6);
            }
            else
            {
                ddlInvoiceNumber.DataSource = invoiceList;
            }
            //ER 6775 End

            ddlInvoiceNumber.DataBind();
            ddlInvoiceNumber.Items.Insert(0, new ListItem(DEFAULT_INVOICENUMBER_TEXT, DEFAULT_INVOICE_VALUE));

            ddlInvoiceNumber.Enabled = true;

            LoadUploadFileType();
            OnInvoiceTypeChangeEmptyGrid();
            ClearGrid();

            if (IsQueryString)
            {
                ddlInvoiceNumber_SelectedIndexChanged(null, null);
            }
        }
        else
        {
            ddlInvoiceNumber.Enabled = false;
            ddlInvoiceNumber.Items.Clear();
            OnInvoiceTypeChangeEmptyGrid();
        }
    }
    #endregion

    #region OnInvoiceTypeChangeEmptyGrid
    /// <summary>
    /// 
    /// </summary>
    private void OnInvoiceTypeChangeEmptyGrid()
    {
        lblInvoiceCreatedDate.Text = string.Empty;
        lblInvoiceStatus.Text = string.Empty;
        lblInvoiceLastAction.Text = string.Empty;
        lblInvoiceBillingDate.Text = string.Empty;
        lblInvoiceExportStatus.Text = string.Empty;
        lblRT1Status.Text = string.Empty;
        lblRT1TimeStamp.Text = string.Empty;
        lblRT2Status.Text = string.Empty;
        lblRT2TimeStamp.Text = string.Empty;
        lblRT3Status.Text = string.Empty;
        lblRT3TimeStamp.Text = string.Empty;
        lblRT4Status.Text = string.Empty;
        lblRT4TimeStamp.Text = string.Empty;
        lblRT5Status.Text = string.Empty;
        lblRT5TimeStamp.Text = string.Empty;
        grdUploadFile.DataSource = null;
        grdUploadFile.DataBind();
        btnUploadFile.Enabled = false;
        btnCompareToCRM.Enabled = false;

        //1582
        lblInvoiceBillingSystem.Text = string.Empty;
    }
    #endregion

    #region ClearGrid
    /// <summary>
    /// 
    /// </summary>
    private void ClearGrid()
    {
        grdExportFile.DataSource = null;
        grdExportFile.DataBind();
    }
    #endregion

    #region EnableDisableButtonBasedOnDateLogic
    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    private void EnableDisableButtonBasedOnDateLogic(ProcessWorkFlowStatus status)
    {
        //Logic to enable/disable the process workflow buttons based on the Bill Start/End date.
        DateTime dtCurrentDateTime = DateTime.Now;
        CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
        List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();

        Invoice invoiceDetail = GetInvoiceNumberDetails();

        //new
        List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
        InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);

        invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
        MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();

        objInvoiceType = invoiceTypeList.Where(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString())).FirstOrDefault();

        //SERO-1582
        if (!string.IsNullOrEmpty(objInvoiceType.BillingSystem.ToString()))
        {
            invoiceDetail.BillingSystem = (objInvoiceType.BillingSystem.ToString() == "CRM") ? "CRM" : "CSG Ascendon";
        }

        //end

        LoadInvoiceDetails(invoiceDetail);

        InvoiceBillStartDate = invoiceDetail.BillStartDate;
        InvoiceBillEndDate = invoiceDetail.BillEndDate;
        long lSnapshotId = 0;

        //int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
        //listMBMComparisonResult = crmAccount.GetMBMComparisonResult(invoiceId);

        //lSnapshotId = listMBMComparisonResult.FirstOrDefault().SnapShotId.Value;


        if (ConfigurationManager.AppSettings["OffsetDaysBefore"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["OffsetDaysBefore"].ToString())
            && ConfigurationManager.AppSettings["OffsetDaysAfter"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["OffsetDaysAfter"].ToString())
            && ConfigurationManager.AppSettings["CurrentDayOffset"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["CurrentDayOffset"].ToString()))
        {
            int intOffsetDaysBefore = Convert.ToInt32(ConfigurationManager.AppSettings["OffsetDaysBefore"].ToString());
            int intOffsetDaysAfter = Convert.ToInt32(ConfigurationManager.AppSettings["OffsetDaysAfter"].ToString());
            int intCurrentDayOffset = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentDayOffset"].ToString());

            //For testing purpose.
            dtCurrentDateTime = dtCurrentDateTime.AddDays(intCurrentDayOffset);

            if (InvoiceBillStartDate != null && InvoiceBillEndDate != null)
            {
                if (dtCurrentDateTime >= InvoiceBillStartDate.Value.AddDays(intOffsetDaysAfter) && dtCurrentDateTime < InvoiceBillEndDate.Value.AddDays(intOffsetDaysBefore))
                {
                    //disable file delete button
                    Button btnDelete = new Button();
                    if (grdUploadFile.Rows.Count > 0 && grdUploadFile.Rows[0].FindControl("btnDeleteUploadFile") != null)
                    {
                        btnDelete = (Button)grdUploadFile.Rows[0].FindControl("btnDeleteUploadFile");
                    }
                    if (isAutomationInvoiceOverriden())
                    {
                        btnDelete.Enabled = false;
                        btnDelete.ToolTip = "Manually Override Invoice To Perform This";
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }

                    if (isAutomationInvoiceOverriden())
                    {
                        btnUploadFile.Enabled = false;
                        btnUploadFile.ToolTip = "Manually Override Invoice To Perform This";
                    }
                    else
                    {
                        btnUploadFile.Enabled = true;
                    }
                    //btnUploadFile.Enabled = true;

                    if (isAutomationInvoiceOverriden())
                    {
                        btnCompareToCRM.Enabled = false;
                        btnCompareToCRM.ToolTip = "Manually Override Invoice To Perform This";
                    }
                    else
                    {
                        btnCompareToCRM.Enabled = true;
                    }
                    //btnCompareToCRM.Enabled = true;
                    if (status.CompareToCRM)
                    {
                        btnViewChanges.Enabled = true;
                    }
                    if (status.ViewChange)
                    {
                        if (isInvoiceApprovedByApi())
                        {
                            btnApproveChanges.Enabled = false;
                            btnApproveChanges.ToolTip = "Manually Override Invoice To Perform This";
                        }
                        else
                        {
                            btnApproveChanges.Enabled = true;
                        }
                    }
                    if (status.ApproveChange)
                    {
                        //sync will be done through automation only unless pre billing is overriden
                        if (isAutomationInvoiceOverriden())
                        {
                            btnSyncCRM.Enabled = false;
                            btnSyncCRM.ToolTip = "Manually Override Invoice To Perform This";
                        }
                        else
                        {
                            btnSyncCRM.Enabled = true;
                        }
                        //btnSyncCRM.Enabled = true;
                    }
                    if (status.SyncCRM)
                    {
                        btnCompareToCRM.Enabled = false;
                        btnCompareToCRM.ToolTip = "This button is disabled as the SyncToCRM process is not completed yet";

                        btnSyncCRM.Enabled = false;

                        bool IsSyncComplete = false;
                        bool IsSpecified = true;

                        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
                        listMBMComparisonResult = crmAccount.GetMBMComparisonResult(invoiceId);

                        lSnapshotId = listMBMComparisonResult.FirstOrDefault().SnapShotId.Value;

                        CBTSService.CBTSService cbtsService = new CBTSService.CBTSService();
                        cbtsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["QuorumCBTSServiceCallTimeout"]); //600000;

                        cbtsService.CheckSnapshotStatus(lSnapshotId, true, out IsSyncComplete, out IsSpecified);

                        if (IsSyncComplete)
                        {
                            btnCompareToCRM.Enabled = true;
                            btnCompareToCRM.ToolTip = string.Empty;

                            string strInvoiceNumber = string.Empty;

                            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

                            ProcessWorkFlowStatus objProcessStatus = new ProcessWorkFlowStatus();
                            objProcessStatus.InvoiceNumber = strInvoiceNumber;
                            objProcessStatus.CompareToCRM = false;
                            objProcessStatus.ViewChange = true;
                            objProcessStatus.ApproveChange = false;
                            objProcessStatus.SyncCRM = false;
                            objProcessStatus.ImportCRMData = false;
                            objProcessStatus.ProcessInvoice = false;
                            objProcessStatus.ExportInvoiceFile = false;

                            _billing = new BillingEngine(strConnectionString);
                            _billing.ProcessWorkFlowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(objProcessStatus);

                            // As we are making the pre-billing activities status as 0, update the same
                            _billing.ProcessWorkFlowStatusBL.ResetPreBillingActivityStatus(strInvoiceNumber);
                        }
                        else
                        {
                            btnCompareToCRM.Enabled = false;
                            btnCompareToCRM.ToolTip = "This button is disabled as the SyncToCRM process is not completed yet";
                        }
                    }
                }
                else
                {
                    btnUploadFile.Enabled = false;

                    foreach (GridViewRow gvrow in grdUploadFile.Rows)
                    {
                        Button btnDelete = (Button)gvrow.FindControl("btnDeleteUploadFile");
                        btnDelete.Enabled = false;

                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectedFile");

                        if (chk != null)
                        {
                            chk.Enabled = false;
                        }
                    }

                    btnCompareToCRM.Enabled = false;
                    //btnCompareToCRM.ToolTip = "This button is disabled as the enable offset period has exceeded";

                    if (status.ViewChange)
                    {
                        btnViewChanges.Enabled = true;
                    }
                    else
                    {
                        btnViewChanges.Enabled = false;
                    }

                    btnApproveChanges.Enabled = false;
                    btnSyncCRM.Enabled = false;
                }

                if (dtCurrentDateTime > InvoiceBillEndDate.Value.AddDays(intOffsetDaysAfter))
                {
                    btnImportCRMData.Enabled = true;
                    btnImportCRMData.ToolTip = "";
                    btnCompareToCRM.ToolTip = "This button is disabled as the enable offset period has exceeded";

                    if (status.ImportCRMData)
                    {
                        btnProcessInvoice.Enabled = true;
                    }
                    if (status.ProcessInvoice)
                    {
                        btnExportInvoiceFiles.Enabled = true;
                    }
                }
                else
                {
                    btnImportCRMData.Enabled = false;
                    btnImportCRMData.ToolTip = "This button will be enabled only after the bill cycle has run";

                    btnProcessInvoice.Enabled = false;
                    btnExportInvoiceFiles.Enabled = false;
                }
            }
            else
            {
                Controls_ErrorSuccessNotifier.AddErrorMessage("Invoice Bill Start or End date cannot be null");
            }
        }
        else
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("No offset days defined in the configuration");
        }
    }
    #endregion

    #region ddlInvoiceNumber_SelectedIndexChanged
    /// <summary>
    /// Invoice number dropdown list index changed logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsQueryString)
        {
            ddlInvoiceNumber.SelectedValue = InvoiceIdQS;
        }
        if (ddlInvoiceNumber.SelectedValue != DEFAULT_INVOICE_VALUE)
        {
            Invoice invoiceDetail = GetInvoiceNumberDetails();

            //new
            List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);

            invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
            MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();

            objInvoiceType = invoiceTypeList.Where(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString())).FirstOrDefault();

            //SERO-1582
            if (!string.IsNullOrEmpty(objInvoiceType.BillingSystem.ToString()))
            {
                invoiceDetail.BillingSystem = (objInvoiceType.BillingSystem.ToString() == "CRM") ? "CRM" : "CSG Ascendon";
            }

            //end

            //invoiceDetail.BillingSystem = "";

            LoadInvoiceDetails(invoiceDetail);

            // Assign values to properties InvoiceBillingMonth ,InvoiceBillingYear ,InvoiceBillEndDate
            InvoiceBillingMonth = invoiceDetail.BillingMonth;
            InvoiceBillingYear = invoiceDetail.BillingYear;
            InvoiceBillEndDate = invoiceDetail.BillEndDate;
            InvoiceBillStartDate = invoiceDetail.BillStartDate;

            //automation
            int invoiceId = 0;
            if (IsQueryString)
            {
                invoiceId = Convert.ToInt32(InvoiceIdQS);
            }
            else
            {
                invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
            }
            InvoiceBL invoiceBL = new InvoiceBL(strConnectionString);
            List<ExportedFile> exportedFileList = invoiceBL.GetInvoiceFileExports(invoiceId);

            //cbe_8967

            string OutputFileFormat = objInvoiceType.OutputFileFormat.ToString();
            ClearGrid();
            if (exportedFileList.Count > 0)
            {
                //cbe_8967               
                if (OutputFileFormat.ToString() == "1")
                {
                    List<ExportedFile> items = exportedFileList.Where(x => x.ExportedFileName.Contains("OneType")).ToList();
                    grdExportFile.DataSource = items;
                    grdExportFile.DataBind();
                }
                else
                {
                    exportedFileList.RemoveAll(x => x.ExportedFileName.Contains("OneType"));
                    grdExportFile.DataSource = exportedFileList;
                    grdExportFile.DataBind();
                }
            }

            //Automate Status
            MBMAutomateStatus mbmAutomateStatus = invoiceBL.GetMBMAutomateStatus(invoiceId);
            if (mbmAutomateStatus.InvoiceId != null)
            {
                divAutomationStatus.Visible = true;
                lblAutomationStatus.Text = mbmAutomateStatus.LastAction;
                chkPreBillOverride.Checked = mbmAutomateStatus.isPreBillingOverriden.GetValueOrDefault();
            }
            else
            {
                divAutomationStatus.Visible = false;
            }

            achUploadFile.Attributes["class"] = "accordion active";
            divUploadFile.Attributes["class"] = "panel show";
            btnUploadFile.Enabled = true;
            btnCompareToCRM.Enabled = true;

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            ProcessWorkFlowStatus objStatus = GetProcessWorkflowStatus(strInvoiceNumber);
            EnableDisableProcessWorkFlow(objStatus);

            EnableDisableButtonBasedOnDateLogic(objStatus);
            btnSyncCRM.Enabled = true; //testttttt
        }
        else
        {
            achUploadFile.Attributes["class"] = "accordion";
            divUploadFile.Attributes["class"] = "panel";
            //ddlInvoiceNumber.Enabled = false;
            //ddlInvoiceNumber.Items.Clear();
            divAutomationStatus.Visible = false;
            lblInvoiceCreatedDate.Text = string.Empty;
            lblInvoiceStatus.Text = string.Empty;
            lblInvoiceLastAction.Text = string.Empty;
            lblInvoiceBillingDate.Text = string.Empty;
            lblInvoiceExportStatus.Text = string.Empty;
            //1582
            lblInvoiceBillingSystem.Text = string.Empty;
            lblRT1Status.Text = string.Empty;
            lblRT1TimeStamp.Text = string.Empty;
            lblRT2Status.Text = string.Empty;
            lblRT2TimeStamp.Text = string.Empty;
            lblRT3Status.Text = string.Empty;
            lblRT3TimeStamp.Text = string.Empty;
            lblRT4Status.Text = string.Empty;
            lblRT4TimeStamp.Text = string.Empty;
            lblRT5Status.Text = string.Empty;
            lblRT5TimeStamp.Text = string.Empty;
            grdUploadFile.DataSource = null;
            grdUploadFile.DataBind();
            grdExportFile.DataSource = null;
            grdExportFile.DataBind();

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            if (!string.IsNullOrEmpty(strInvoiceNumber.Trim()))
            {
                ProcessWorkFlowStatus objStatus = GetProcessWorkflowStatus(strInvoiceNumber);
                EnableDisableProcessWorkFlow(objStatus);

                EnableDisableButtonBasedOnDateLogic(objStatus);

                btnUploadFile.Enabled = false;
                btnCompareToCRM.Enabled = false;
            }
        }
    }
    #endregion

    #region LoadInvoiceDetails
    /// <summary>
    /// Load detailed info of a selected invoice number
    /// </summary>
    /// <param name="invoice"></param>
    private void LoadInvoiceDetails(Invoice invoice)
    {
        try
        {
            lblInvoiceCreatedDate.Text = Convert.ToString(invoice.CreatedDate);
            lblInvoiceStatus.Text = Convert.ToString(invoice.Status);
            lblInvoiceLastAction.Text = Convert.ToString(invoice.LastAction);
            lblInvoiceBillingDate.Text = Convert.ToString(invoice.BillingMonth + "/" + invoice.BillingYear);
            lblInvoiceExportStatus.Text = invoice.BillingFileExportStatus.ToString();
            lblInvoiceBillingSystem.Text = invoice.BillingSystem;

            string strBillDate = string.Empty;
            if (invoice.BillEndDate != null)
            {
                strBillDate = invoice.BillEndDate.Value.AddDays(1).Date.ToString("MM/dd/yyyy");
            }
            lblBillDate.Text = " (BillCycle Run Date: " + strBillDate + ")";

            if (invoice.RecordType1Status != null)
            {
                lblRT1Status.Text = invoice.RecordType1Status.ToString();
                lblRT1TimeStamp.Text = Convert.ToString(invoice.RecordType1DateTime);
            }
            else
            {
                lblRT1Status.Text = "none";
                lblRT1TimeStamp.Text = string.Empty;
            }

            if (invoice.RecordType2Status != null)
            {
                lblRT2Status.Text = invoice.RecordType2Status.ToString();
                lblRT2TimeStamp.Text = Convert.ToString(invoice.RecordType2DateTime);
            }
            else
            {
                lblRT2Status.Text = "none";
                lblRT2TimeStamp.Text = string.Empty;
            }

            if (invoice.RecordType3Status != null)
            {
                lblRT3Status.Text = invoice.RecordType3Status.ToString();
                lblRT3TimeStamp.Text = Convert.ToString(invoice.RecordType3DateTime);
            }
            else
            {
                lblRT3Status.Text = "none";
                lblRT3TimeStamp.Text = string.Empty;
            }

            if (invoice.RecordType4Status != null)
            {
                lblRT4Status.Text = invoice.RecordType4Status.ToString();
                lblRT4TimeStamp.Text = Convert.ToString(invoice.RecordType4DateTime);
            }
            else
            {
                lblRT4Status.Text = "none";
                lblRT4TimeStamp.Text = string.Empty;
            }

            if (invoice.RecordType5Status != null)
            {
                lblRT5Status.Text = invoice.RecordType5Status.ToString();
                lblRT5TimeStamp.Text = Convert.ToString(invoice.RecordType5DateTime);
            }
            else
            {
                lblRT5Status.Text = "none";
                lblRT5TimeStamp.Text = string.Empty;
            }

            grdUploadFile.DataSource = invoice.UploadedFiles;
            grdUploadFile.DataBind();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to load Invoice details");
            LogException(ex, "LoadInvoiceDetails");
        }
    }
    #endregion

    #region ddlNewInvoiceType_SelectedIndexChanged
    /// <summary>
    /// New Invoice Type dropdown list index changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNewInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
        invoiceTypeList = InvoiceTypeLists();
        InvoiceType selectedInvoice = new InvoiceType();

        foreach (InvoiceType item in invoiceTypeList)
        {
            if (item.ID == Convert.ToInt32(ddlNewInvoiceType.SelectedValue.ToString()))
            {
                selectedInvoice = item;
                break;
            }
        }

        if (selectedInvoice != null)
        {
            SetCurrencies(selectedInvoice.ImportCurrencyDefault, selectedInvoice.ExportCurrencyDefault);
        }

        mdlNewInvoice.Show();
    }
    #endregion

    #region SetCurrencies
    /// <summary>
    /// Load Inport/Export currency details
    /// </summary>
    /// <param name="defaultImportCurrency"></param>
    /// <param name="defaultExportCurrency"></param>
    private void SetCurrencies(string defaultImportCurrency, string defaultExportCurrency)
    {
        try
        {
            List<CurrencyConversion> _importCurrencies = _billing.Configurations.Currencies(DateTime.Now);
            ddlNewImportCurrency.DataTextField = "Description";
            ddlNewImportCurrency.DataValueField = "ID";
            ddlNewImportCurrency.DataSource = _importCurrencies;
            ddlNewImportCurrency.DataBind();

            CurrencyConversion ccI = _importCurrencies.Find(
                  delegate(CurrencyConversion c)
                  {
                      return c.Code == defaultImportCurrency;
                  });

            if (ccI != null)
            {
                ddlNewImportCurrency.SelectedValue = ccI.ID.ToString();
            }

            List<CurrencyConversion> _exportCurrencies = _billing.Configurations.Currencies(DateTime.Now);
            ddlNewExportCurrency.DataTextField = "Description";
            ddlNewExportCurrency.DataValueField = "ID";
            ddlNewExportCurrency.DataSource = _exportCurrencies;
            ddlNewExportCurrency.DataBind();

            CurrencyConversion ccE = _exportCurrencies.Find(
                  delegate(CurrencyConversion c)
                  {
                      return c.Code == defaultExportCurrency;
                  });

            if (ccE != null)
            {
                ddlNewExportCurrency.SelectedValue = ccE.ID.ToString();
            }
        }
        catch (Exception ex)
        {
            LogException(ex, "SetCurrencies");
        }
    }
    #endregion

    #region grdUploadFile_RowDeleting
    /// <summary>
    /// Upload file grid row deleting event logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdUploadFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            _billing = new BillingEngine(strConnectionString);
            string id = grdUploadFile.DataKeys[e.RowIndex].Value.ToString();

            if (!string.IsNullOrWhiteSpace(id))
            {
                _billing.UploadedFileBL.DeleteFileuploadbyFileID(Convert.ToInt32(id));
                //reload grid data
                Invoice invoiceDetail = GetInvoiceNumberDetails();
                LoadInvoiceDetails(invoiceDetail);

                string strInvoiceNumber = string.Empty;
                strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

                ProcessWorkFlowStatus objStatus = GetProcessWorkflowStatus(strInvoiceNumber);
                EnableDisableProcessWorkFlow(objStatus);

                EnableDisableButtonBasedOnDateLogic(objStatus);
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occured while deleting the file");
            LogException(ex, "grdUploadFile_RowDeleting");
        }
    }
    #endregion

    #region GetInvoiceNumberDetails
    /// <summary>
    /// Gets the Invoice Number details
    /// </summary>
    /// <returns></returns>
    private Invoice GetInvoiceNumberDetails()
    {
        Invoice invoiceDetail = new Invoice();
        _billing = new BillingEngine(strConnectionString);

        //automation
        int invoiceId = 0;
        if (IsQueryString)
        {
            invoiceId = Convert.ToInt32(InvoiceIdQS);
        }
        else
        {
            invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
        }
        //int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
        invoiceDetail = _billing.InvoiceBL.FetchInvoiceById(invoiceId);
        return invoiceDetail;
    }
    #endregion

    #region CreateInvoice
    /// <summary>
    /// Create new Invoice logic
    /// </summary>
    /// <param name="invoiceNumber"></param>
    /// <returns></returns>
    private bool CreateInvoice(ref string invoiceNumber)
    {
        Invoice invoice = new Invoice();
        _billing = new BillingEngine(strConnectionString);
        List<InvoiceType> billTypes = _billing.InvoiceBL.GetInvoiceTypes();

        var invoiceBills = from item in billTypes
                           where item.ID == Convert.ToInt32(ddlNewInvoiceType.SelectedValue.ToString())
                           select item;

        if (billTypes == null)
        {
            return false;
        }

        CRMAccountBL objCRMAccountBL = new CRMAccountBL(strConnectionString);
        List<CRMAccount> lstCRMAccount = new List<CRMAccount>();
        lstCRMAccount = objCRMAccountBL.GetAssoicateAccountsByInvoiceTypeId(Convert.ToInt32(ddlNewInvoiceType.SelectedValue.ToString()));

        if (lstCRMAccount.Count == 0)
        {
            mdlNewInvoice.Show();
            lblNewInvoiceErrorMessage.Visible = true;
            lblNewInvoiceErrorMessage.Text = "Cannot create a Invoice as Customer doesn't have associated CRM account";

            return false;
        }

        InvoiceType billType = new InvoiceType();

        if (invoiceBills != null)
        {
            foreach (InvoiceType inv in invoiceBills)
            {
                billType = inv;
                break;
            }
        }

        string userName = GetUserName();

        invoice.TypeOfBill = Convert.ToInt32(ddlNewInvoiceType.SelectedValue.ToString());
        invoice.DefaultImportCurrencyID = Convert.ToInt32(ddlNewImportCurrency.SelectedValue.ToString());
        invoice.ExportCurrencyID = Convert.ToInt32(ddlNewExportCurrency.SelectedValue.ToString());

        //Month index starts from 0 so adding 1.
        invoice.BillingMonth = Convert.ToInt32(ddlNewInvoiceMonth.SelectedValue.ToString()) + 1;
        invoice.BillingYear = Convert.ToInt32(ddlNewInvoiceYear.SelectedValue.ToString());
        invoice.CustomerNum = string.Empty;

        invoice.RecordType1Status = Constants.UNKNOWN;
        invoice.RecordType2Status = Constants.UNKNOWN;
        invoice.RecordType3Status = Constants.UNKNOWN;
        invoice.RecordType4Status = Constants.UNKNOWN;
        invoice.RecordType5Status = Constants.UNKNOWN;

        invoice.InvoiceNumber = billType.Prefix + _billing.InvoiceBL.GetNextGeneratedInvoiceNumber(invoice.TypeOfBill, billType.Prefix);
        invoiceNumber = invoice.InvoiceNumber;
        invoice.BillingFileExportStatus = Constants.UNKNOWN;

        invoice.CreatedBy = userName;
        invoice.CreatedDate = DateTime.Now;
        invoice.Status = Constants.PROCESSING;
        invoice.LastAction = "Created";

        invoice.LastUpdatedBy = userName;
        invoice.LastUpdatedDate = DateTime.Now;

        //Restriction for duplicate Invoice creation for same billcycle
        List<Invoice> invoiceList = new List<Invoice>();
        int InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
        invoiceList = _billing.InvoiceBL.FetchInvoicesByType(InvoiceType);
        Invoice objinvoice = new Invoice();
        objinvoice = invoiceList.Where(m => m.BillingMonth == invoice.BillingMonth && m.BillingYear == invoice.BillingYear).FirstOrDefault();
        if (objinvoice != null)
        {
            if (objinvoice.BillingMonth != null && objinvoice.BillingYear != null)
            {
                mdlNewInvoice.Show();
                lblNewInvoiceErrorMessage.Visible = true;
                lblNewInvoiceErrorMessage.Text = "Invoice already exists for the Bill Cycle";
            }
            return false;
        }
        else
        {
            _billing.InvoiceBL.InsertInvoice(invoice, userName);
            return true;
        }
    }
    #endregion

    #region btnCreateInvoice_Click
    /// <summary>
    /// Create Invoice button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            btnCreateInvoice.Enabled = false;
            string invoiceNumber = string.Empty;

            //Automation
            //InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
            //List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
            //invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
            //MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();

            //objInvoiceType = invoiceTypeList.Where(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString())).FirstOrDefault();

            //if (objInvoiceType != null)
            //{
            //if (objInvoiceType.IsAutoPreBilling)
            //{
            //   Controls_ErrorSuccessNotifier.AddErrorMessage("Please disable Auto Pre Bill to create invoice");
            //}

            if (CreateInvoice(ref invoiceNumber))
            {
                //Reload dropdownlist again with new item
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    TypeCreated = Convert.ToInt32(ddlNewInvoiceType.SelectedValue.ToString());
                    //Month index starts from 0 so adding 1.
                    MonthCreated = Convert.ToInt32(ddlNewInvoiceMonth.SelectedValue.ToString()) + 1;
                    YearCreated = Convert.ToInt32(ddlNewInvoiceYear.SelectedValue.ToString());

                    string strNewInvoiceNumber = string.Empty;
                    string strNewMonth = string.Empty;
                    string strNewYear = string.Empty;

                    if (MonthCreated.ToString().Length == 1)
                    {
                        strNewMonth = "0" + MonthCreated.ToString();
                    }
                    else
                    {
                        strNewMonth = MonthCreated.ToString();
                    }

                    strNewYear = YearCreated.ToString().Substring(2, 2);
                    strNewInvoiceNumber = invoiceNumber + " - " + strNewMonth + "/" + strNewYear;

                    //gets invoicetype selected.
                    ddlInvoiceType.ClearSelection();
                    ddlInvoiceType.Items.FindByValue(ddlNewInvoiceType.SelectedValue.ToString()).Selected = true;
                    int InvoiceType = Convert.ToInt32(ddlNewInvoiceType.SelectedValue.ToString());
                    List<Invoice> invoiceList = new List<Invoice>();

                    invoiceList = _billing.InvoiceBL.FetchInvoicesByType(InvoiceType);

                    foreach (Invoice objInvoice in invoiceList)
                    {
                        string strMonthName = string.Empty;
                        if (objInvoice.BillingMonth.ToString().Length == 1)
                        {
                            strMonthName = "0" + objInvoice.BillingMonth.ToString();
                        }
                        else
                        {
                            strMonthName = objInvoice.BillingMonth.ToString();
                        }

                        string strYear = objInvoice.BillingYear.ToString().Substring(2, 2);
                        objInvoice.InvoiceNumber = objInvoice.InvoiceNumber + " - " + strMonthName + "/" + strYear;
                    }

                    ddlInvoiceNumber.DataTextField = DEFAULT_INVOICENUMBER_TEXTFIELD;
                    ddlInvoiceNumber.DataValueField = DEFAULT_INVOICENUMBER_VALUEFIELD;

                    //ER#6775
                    if (!chkInvoiceNumber.Checked)
                    {
                        ddlInvoiceNumber.DataSource = invoiceList.Take(6);
                    }
                    else
                    {
                        ddlInvoiceNumber.DataSource = invoiceList;
                    }

                    ddlInvoiceNumber.DataBind();

                    ddlInvoiceNumber.Items.Insert(0, new ListItem(DEFAULT_INVOICENUMBER_TEXT, DEFAULT_INVOICE_VALUE));
                    ddlInvoiceNumber.ClearSelection();

                    ddlInvoiceNumber.Items.FindByText(strNewInvoiceNumber).Selected = true;
                    ddlInvoiceNumber.Enabled = true;

                    Invoice invoiceDetail = GetInvoiceNumberDetails();
                    LoadInvoiceDetails(invoiceDetail);

                    // Assign values to properties InvoiceBillingMonth ,InvoiceBillingYear ,InvoiceBillEndDate
                    InvoiceBillingMonth = invoiceDetail.BillingMonth;
                    InvoiceBillingYear = invoiceDetail.BillingYear;
                    InvoiceBillEndDate = invoiceDetail.BillEndDate;
                    InvoiceBillStartDate = invoiceDetail.BillStartDate;

                    achUploadFile.Attributes["class"] = "accordion active";
                    divUploadFile.Attributes["class"] = "panel show";
                    btnUploadFile.Enabled = true;
                    btnCompareToCRM.Enabled = true;

                    string strInvoiceNumber = string.Empty;
                    strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(strNewInvoiceNumber);

                    ProcessWorkFlowStatus objStatus = GetProcessWorkflowStatus(strInvoiceNumber);
                    EnableDisableProcessWorkFlow(objStatus);

                    EnableDisableButtonBasedOnDateLogic(objStatus);

                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Invoice: " + invoiceNumber + " created successfully");
                }
                else
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("New Invoice number cannot be null or empty");
                }
                //}
            }


        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to create Invoice");
            LogException(ex, "btnCreateInvoice_Click");
        }
    }
    #endregion

    #region LoadUploadFileType
    /// <summary>
    /// 
    /// </summary>
    private void LoadUploadFileType()
    {
        try
        {
            List<FileTypes> fileTypes = new List<FileTypes>();
            _billing = new BillingEngine(strConnectionString);
            int InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            fileTypes = _billing.FileTypeBL.GetAssoicateFileTypesByInvoiceTypeId(InvoiceType);
            ddlNewFileTypeUpload.DataTextField = DEFAULT_FileType_TEXTFIELD;
            ddlNewFileTypeUpload.DataValueField = DEFAULT_FileType_VALUEFIELD;
            ddlNewFileTypeUpload.DataSource = fileTypes;
            ddlNewFileTypeUpload.DataBind();

            ddlNewFileTypeUpload.Items.Insert(0, new ListItem(FILETYPE_SELECT, FILETYPE_SELECT));
        }
        catch (Exception ex)
        {
            LogException(ex, "LoadUploadFileType");
        }
    }
    #endregion

    #region ddlNewFileTypeUpload_SelectedIndexChanged
    /// <summary>
    /// New file upload dropdown list index change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNewFileTypeUpload_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblNewFileUploadExeception.Text = string.Empty;

        if (ddlNewFileTypeUpload.SelectedItem.Text.Equals(FILETYPE_DB) || ddlNewFileTypeUpload.SelectedItem.Text.Equals(FILETYPE_SELECT))
        {
            lblNewFileuploadFilePath.Visible = false;
            fpNewFileuploadFilePath.Visible = false;
            mdlUploadFile.Show();
        }
        else
        {
            lblNewFileuploadFilePath.Visible = Visible;
            fpNewFileuploadFilePath.Visible = Visible;
            mdlUploadFile.Show();
        }
    }
    #endregion

    #region ValidateUser
    /// <summary>
    /// Validates the login user
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="strLoginUser"></param>
    /// <returns></returns>
    private bool ValidateUser(string connectionString, string strLoginUser)
    {
        try
        {
            Authenticate objAuth = new Authenticate();
            string strUserNameRole = string.Empty;
            Users objUser = new Users();

            objUser = objAuth.AuthenticateUser(connectionString, strLoginUser);

            LinkButton lbUserName = (LinkButton)Master.FindControl("linkUserName");

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    strUserNameRole = objUser.UserFirstName + " " + objUser.UserLastName + " (" + objUser.UserRole + ")";

                    if (objUser.IsActive)
                    {
                        if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator) || objUser.UserRole.Equals(UserRoleType.SystemAdministrator) || objUser.UserRole.Equals(UserRoleType.Processor) || objUser.UserRole.Equals(UserRoleType.Approver))
                        {
                            dvProcessInvoice.Visible = true;
                            dvAccessDenied.Visible = false;
                            lbUserName.Text = strUserNameRole;
                            return true;
                        }
                    }
                    else
                    {
                        dvProcessInvoice.Visible = false;
                        dvAccessDenied.Visible = true;
                        lblAccessDenied.Text = "Your status is inactive. Please contact the application administrator for activating the access.";
                        lbUserName.Text = strUserNameRole;
                        return false;
                    }
                }
                else
                {
                    dvProcessInvoice.Visible = false;
                    dvAccessDenied.Visible = true;
                    lblAccessDenied.Text = "You are not authorized to access this page. Please contact the application administrator for access.";
                    lbUserName.Text = "Unknown User";
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            LogException(ex, "ValidateUser");
        }
        return false;
    }
    #endregion

    #region chkSelectedFile
    /// <summary>
    /// Bind only the filetype which user selected in the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkSelectedFile(object sender, EventArgs e)
    {
        try
        {
            string chkSelectedUploadedFileId = string.Empty;
            Boolean chkSelected = false;
            foreach (GridViewRow gvrow in grdUploadFile.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectedFile");

                if (chk != null && chk.Checked)
                {
                    chkSelectedUploadedFileId = grdUploadFile.DataKeys[gvrow.RowIndex].Value.ToString();
                    chkSelected = true;
                    break;
                }
            }

            Invoice invoiceDetail = GetInvoiceNumberDetails();
            UploadedFileCollection uploadedFileCollection = invoiceDetail.UploadedFiles;
            List<FileTypes> fileTypesList = new List<FileTypes>();

            _billing = new BillingEngine(strConnectionString);
            int InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            fileTypesList = _billing.FileTypeBL.GetAssoicateFileTypesByInvoiceTypeId(InvoiceType);

            if (chkSelected)
            {
                UploadedFile uploadfile = new UploadedFile();
                if (uploadedFileCollection.Count > 0)
                {
                    foreach (UploadedFile item in uploadedFileCollection)
                    {
                        if (item.UploadedFileId == Convert.ToInt32(chkSelectedUploadedFileId))
                        {
                            uploadfile = item;
                            break;
                        }
                    }
                }

                List<FileTypes> fileTyp = new List<FileTypes>();

                if (fileTypesList.Count > 0 && uploadfile != null)
                {
                    foreach (FileTypes item in fileTypesList)
                    {
                        if (item.FileType == uploadfile.FileType)
                        {
                            fileTyp.Add(item);
                            break;
                        }
                    }
                }

                if (fileTyp.Count > 0 && fileTyp[0].FileType == FILETYPE_DB)
                {
                    lblNewFileuploadFilePath.Visible = false;
                    fpNewFileuploadFilePath.Visible = false;
                }
                else
                {
                    lblNewFileuploadFilePath.Visible = true;
                    fpNewFileuploadFilePath.Visible = true;
                }

                ddlNewFileTypeUpload.DataTextField = DEFAULT_FileType_TEXTFIELD;
                ddlNewFileTypeUpload.DataValueField = DEFAULT_FileType_VALUEFIELD;
                ddlNewFileTypeUpload.DataSource = fileTyp;
                ddlNewFileTypeUpload.DataBind();
            }
            else
            {
                lblNewFileuploadFilePath.Visible = true;
                fpNewFileuploadFilePath.Visible = true;
                ddlNewFileTypeUpload.DataTextField = DEFAULT_FileType_TEXTFIELD;
                ddlNewFileTypeUpload.DataValueField = DEFAULT_FileType_VALUEFIELD;
                ddlNewFileTypeUpload.DataSource = fileTypesList;
                ddlNewFileTypeUpload.DataBind();
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to process");
            LogException(ex, "chkSelectedFile");
        }
    }
    #endregion

    #region btnProceessUploadFile_Click
    /// <summary>
    /// Upload file button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProceessUploadFile_Click(object sender, EventArgs e) 
    {
        try
        {
            string fileType = FILETYPE_DB;
            FileTypeBL objFileTypes = new FileTypeBL(strConnectionString);
            MBMUploadFile uploadFile = new MBMUploadFile();
            string chkSelectedUploadedFileId = string.Empty;
            Boolean chkSelected = false;

            lblNewFileUploadExeception.Visible = false;

            if (!ddlNewFileTypeUpload.SelectedItem.Text.Equals(FILETYPE_SELECT))
            {
                //Logic if user select MBM db as input
                if (fileType == ddlNewFileTypeUpload.SelectedItem.ToString() && !fpNewFileuploadFilePath.HasFiles)
                {
                    GetCheckBoxSelectedRow(ref chkSelectedUploadedFileId, ref chkSelected);
                    uploadFile.InvoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
                    uploadFile.InvoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
                    uploadFile.FileTypeId = Convert.ToInt32(ddlNewFileTypeUpload.SelectedValue.ToString());
                    string FileType = ddlNewFileTypeUpload.SelectedItem.Text;
                    uploadFile.FileName = ddlNewFileTypeUpload.SelectedItem.Text.ToString();
                    uploadFile.UploadedBy = GetUserName();

                    if (chkSelected)
                    {
                        uploadFile.UploadedFileId = Convert.ToInt32(chkSelectedUploadedFileId);
                    }
                    else
                    {
                        uploadFile.UploadedFileId = 0;
                    }

                    bool isFileTypeUploaded = false;
                    if (grdUploadFile.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvrow in grdUploadFile.Rows)
                        {
                            Label lblFileType = (Label)gvrow.FindControl("lblFileType");
                            if (lblFileType.Text == FileType)
                            {
                                isFileTypeUploaded = true;
                            }
                        }
                    }

                    if (isFileTypeUploaded)
                    {
                        lblNewFileUploadExeception.Visible = true;
                        lblNewFileUploadExeception.Text = "FileType '" + FileType + "' already uploaded. Please delete the existing FileType and upload again.";
                        mdlUploadFile.Show();
                    }
                    else
                    {
                        //new 20220308
                        // string billingsystem =crmAccount.GetBillingSystem
                        List<MBM.Entities.InvoiceType> billingsystem = new List<MBM.Entities.InvoiceType>();
                        InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
                        billingsystem = objInvoiceBL.GetBillingSystem(Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString()));

                        if (billingsystem[0].BillingSystem.ToString() == "CRM")
                        {
                            objFileTypes.UpsertMBMdata_ByInvoiceIdInvoiceType(uploadFile);
                        }
                        else
                        {
                            objFileTypes.UploadMBMdata_ByInvoiceIdInvoiceType_CSG(uploadFile);
                        }

                        Invoice invoiceDetail = GetInvoiceNumberDetails();
                        LoadInvoiceDetails(invoiceDetail);

                        btnCompareToCRM.Enabled = true;
                    }
                }
                else if (fpNewFileuploadFilePath.HasFiles && fileType != ddlNewFileTypeUpload.SelectedItem.ToString())
                {
                    try
                    {
                        DataTable dt = ReadCSVData();
                        if (dt != null)
                        {
                            objFileTypes.BulkCopy_MBM_DataToSqlServer(dt, strConnectionString);
                        }
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to upload data to DB");
                        LogException(ex, "btnProceessUploadFile_Click");
                    }
                    GetCheckBoxSelectedRow(ref chkSelectedUploadedFileId, ref chkSelected);
                    uploadFile.InvoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
                    uploadFile.InvoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
                    uploadFile.FileTypeId = Convert.ToInt32(ddlNewFileTypeUpload.SelectedValue.ToString());
                    uploadFile.FileName = fpNewFileuploadFilePath.FileName.ToString();
                    uploadFile.UploadedBy = GetUserName();
                    if (chkSelected)
                    {
                        uploadFile.UploadedFileId = Convert.ToInt32(chkSelectedUploadedFileId);
                    }
                    else
                    {
                        uploadFile.UploadedFileId = 0;
                    }
                    objFileTypes.UpsertMBMDataFile_ByInvoiceIdInvoiceType(uploadFile);
                    Invoice invoiceDetail = GetInvoiceNumberDetails();
                    LoadInvoiceDetails(invoiceDetail);
                    btnCompareToCRM.Enabled = true;
                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Uploaded File data successfully");
                }
            }
            else
            {
                lblNewFileUploadExeception.Visible = true;
                lblNewFileUploadExeception.Text = "Please select a valid FileType.";
                mdlUploadFile.Show();
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to upload data to DB");
            LogException(ex, "btnProceessUploadFile_Click");
        }
    }
    #endregion

    #region GetCheckBoxSelectedRow
    /// <summary>
    /// 
    /// </summary>
    /// <param name="chkSelectedUploadedFileId"></param>
    /// <param name="chkSelected"></param>
    protected void GetCheckBoxSelectedRow(ref string chkSelectedUploadedFileId, ref Boolean chkSelected)
    {
        foreach (GridViewRow gvrow in grdUploadFile.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectedFile");

            if (chk != null && chk.Checked)
            {
                chkSelectedUploadedFileId = grdUploadFile.DataKeys[gvrow.RowIndex].Value.ToString();
                chkSelected = true;
                break;
            }
        }
    }
    #endregion

    #region ReadCSVData
    /// <summary>
    /// Read the data from the CSV input file
    /// </summary>
    /// <returns></returns>
    protected DataTable ReadCSVData()
    {
        bool fileHasHeaders = true;
        bool ignoreFirstLine = false;
        DataTable dt = new DataTable();
        Dictionary<string, System.Type> tableMapping = new Dictionary<string, Type>();
        using (CsvReader csv = new CsvReader(new StreamReader(fpNewFileuploadFilePath.PostedFile.InputStream), fileHasHeaders))
        {
            csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;
            //csv.ParseError += new EventHandler<ParseErrorEventArgs>(CSVParseErrorEventHandler);

            int fieldCount = csv.FieldCount;

            //-choose column headers--
            string[] fieldHeaders;
            if (fileHasHeaders)
            {
                fieldHeaders = csv.GetFieldHeaders();
                for (int k = 1; k <= fieldCount; k++)
                {
                    if (fieldHeaders[k - 1] == null || fieldHeaders[k - 1].Equals(""))
                    {
                        fieldHeaders[k - 1] = "Column " + k;
                    }
                }
            }
            else
            {
                fieldHeaders = new string[fieldCount];
                for (int k = 1; k <= fieldCount; k++)
                {
                    fieldHeaders[k - 1] = "Column " + k;                   //make up Column names if the file doesn't have headers
                }
            }

            //-assign column headers and data types--
            for (int i = 0; i < fieldCount; i++)
            {
                if (tableMapping.ContainsKey(fieldHeaders[i].ToUpper()))
                {
                    dt.Columns.Add(fieldHeaders[i], tableMapping[fieldHeaders[i].ToUpper()]);
                }
                else
                {
                    dt.Columns.Add(fieldHeaders[i], typeof(string));
                }
            }

            if (ignoreFirstLine)
            {
                csv.ReadNextRecord();
            }

            int lineCount = 0;
            //-fill table rows--
            while (csv.ReadNextRecord())
            {
                DataRow dr = dt.NewRow();

                for (int i = 0; i < fieldCount; i++)
                {
                    string currFieldText = csv[i] == null ? "" : csv[i];

                    if (i == 0 && currFieldText.Length > 0)             //skip lines that start w/ the 'Substitute' character (for Taxes)
                    {
                        char specChar = currFieldText.ToCharArray()[0];

                        int charNum = Convert.ToInt32(specChar);

                        if (charNum == 26)                   //ASCII character '26', aka 'SUBSTITUTE'
                        {
                            goto SkipLine;
                        }
                    }

                    AddFieldValueToRow(ref dr, i, currFieldText);
                }

                dt.Rows.Add(dr);

            SkipLine:
                lineCount++;
            }
        }
        return dt;
    }
    #endregion

    #region AddFieldValueToRow
    /// <summary>
    /// 
    /// </summary>
    /// <param name="currRow"></param>
    /// <param name="fieldIndx"></param>
    /// <param name="fieldText"></param>
    private static void AddFieldValueToRow(ref DataRow currRow, int fieldIndx, string fieldText)
    {
        try
        {
            currRow[fieldIndx] = fieldText;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region btnCompareToCRM_Click
    /// <summary>
    /// CompareToCRM button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCompareToCRM_Click(object sender, EventArgs e)
    {
        CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);

        try
        {
            int? invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
            int? invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            string strUserName = string.Empty;
            strUserName = GetUserName();

            //SERO-1582


            // string billingsystem =crmAccount.GetBillingSystem
            List<MBM.Entities.InvoiceType> billingsystem = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
            billingsystem = objInvoiceBL.GetBillingSystem(invoiceTypeId).ToList();
            var billingsystemvalue=billingsystem.Select(x=>x.BillingSystem).FirstOrDefault();
            if (billingsystemvalue.ToString() == "CRM")
            {

                crmAccount.GetCRMInputData(strInvoiceNumber, strUserName);

                if (grdUploadFile.Rows.Count > 0)
                {
                    int isAdded = crmAccount.InsertMBMComparisonResult(invoiceId, invoiceTypeId);

                    if (isAdded == 0)
                    {
                        ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
                        objStatus.CompareToCRM = true;
                        objStatus.ViewChange = false;
                        objStatus.ApproveChange = false;
                        objStatus.SyncCRM = false;
                        objStatus.ImportCRMData = false;
                        objStatus.ProcessInvoice = false;
                        objStatus.ExportInvoiceFile = false;

                        //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
                        UpdateProcessWorkFlowStatus(objStatus);

                        ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);
                        EnableDisableProcessWorkFlow(obj);

                        EnableDisableButtonBasedOnDateLogic(obj);

                        achProcessWorkFlow.Attributes["class"] = "accordion active";
                        divProcessWorkFlow.Attributes["class"] = "panel show";
                    }
                }
                else
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("No input file/data to compare.");
                }
            }
            else  //SERO-1582
            {
                crmAccount.GetCSGInputData(strInvoiceNumber, strUserName);//ToDo

                if (grdUploadFile.Rows.Count > 0)
                {
                    int isAdded = crmAccount.InsertMBMComparisonResultCSG(invoiceId, invoiceTypeId);

                    if (isAdded == 0)
                    {
                        ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
                        objStatus.CompareToCRM = true;
                        objStatus.ViewChange = false;
                        objStatus.ApproveChange = false;
                        objStatus.SyncCRM = false;
                        objStatus.ImportCRMData = false;
                        objStatus.ProcessInvoice = false;
                        objStatus.ExportInvoiceFile = false;

                        //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
                        UpdateProcessWorkFlowStatus(objStatus);

                        ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);
                        EnableDisableProcessWorkFlow(obj);

                        EnableDisableButtonBasedOnDateLogic(obj);

                        achProcessWorkFlow.Attributes["class"] = "accordion active";
                        divProcessWorkFlow.Attributes["class"] = "panel show";
                    }
                }

                else
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("No data to compare.");
                }
            }
        }

        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to compare data with Biller");
            LogException(ex, "btnCompareToCRM_Click");
        }
    }

    #endregion

    #region btnViewChanges_Click
    /// <summary>
    /// View Changes button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnViewChanges_Click(object sender, EventArgs e)
    {
        try
        {
            //SERO-1582
            int? invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            List<MBM.Entities.InvoiceType> billingsystem = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
            billingsystem = objInvoiceBL.GetBillingSystem(invoiceTypeId);

            if (billingsystem[0].BillingSystem == "CRM")
            {
                BindActiveTelephoneListGrid();
                mdlViewChanges.Show();
            }
            else
            {
                BindActiveTelephoneListGridCSG();
                mdlViewChangesCSG.Show();
            }
            //SERO-1582 CHANGES FROM mdlViewChange.Show();

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to view the comparison result");
            LogException(ex, "btnViewChanges_Click");
        }
    }
    #endregion

    #region BindActiveTelephoneListGrid
    /// <summary>
    /// Bind data to the active telephone list grid/
    /// </summary>
    private void BindActiveTelephoneListGrid()
    {
        CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
        List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();

        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

        listMBMComparisonResult = crmAccount.GetMBMComparisonResult(invoiceId);


        lblActiveTelephoneListCount.Text = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddBillItem
                                                                           || i.Action == (int)ActionTypes.AddCharge
                                                                           || i.Action == (int)ActionTypes.UpdateCharge
                                                                           || i.Action == (int)ActionTypes.AddTelephone
                                                                           || i.Action == (int)ActionTypes.ChangeTelephone
                                                                           ).Count().ToString() + " Inserts/Updates/Changes";

        grdActiveTelephoneList.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddBillItem
                                                                           || i.Action == (int)ActionTypes.AddCharge
                                                                           || i.Action == (int)ActionTypes.UpdateCharge
                                                                           || i.Action == (int)ActionTypes.AddTelephone
                                                                           || i.Action == (int)ActionTypes.ChangeTelephone).ToList();
        grdActiveTelephoneList.DataBind();

        btnInsertsExportToExcel.Visible = false;
        if (grdActiveTelephoneList.Rows.Count > 0)
        {
            btnInsertsExportToExcel.Visible = true;
        }

        lblDeactiveTelephoneListCount.Text = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteBillItem
                                                                             || i.Action == (int)ActionTypes.DeleteCharge
                                                                             || i.Action == (int)ActionTypes.DeleteTelephone).Count().ToString() + " Cancels";

        grdDeactiveTelephoneList.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteBillItem
                                                                             || i.Action == (int)ActionTypes.DeleteCharge
                                                                             || i.Action == (int)ActionTypes.DeleteTelephone).ToList();
        grdDeactiveTelephoneList.DataBind();

        btnCancelsExportToExcel.Visible = false;
        if (grdDeactiveTelephoneList.Rows.Count > 0)
        {
            btnCancelsExportToExcel.Visible = true;
        }

        if (listMBMComparisonResult.Count() > 0)
        {
            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.ViewChange);

            ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
            objStatus.CompareToCRM = true;
            objStatus.ViewChange = true;
            objStatus.ApproveChange = false;
            objStatus.SyncCRM = false;
            objStatus.ImportCRMData = false;
            objStatus.ProcessInvoice = false;
            objStatus.ExportInvoiceFile = false;

            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
            UpdateProcessWorkFlowStatus(objStatus);

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);
            EnableDisableProcessWorkFlow(obj);

            InvoiceBL invoiceBl = new InvoiceBL(strConnectionString);
            MBMAutomateStatus mbmAutomateStatus = invoiceBl.GetMBMAutomateStatus(invoiceId);
            if (mbmAutomateStatus.InvoiceId != null)
            {
                UpdateAutomationWorkFlowStatus(invoiceId, true, "ViewChanges");
            }

            EnableDisableButtonBasedOnDateLogic(obj);
        }
    }
    #endregion
    //SERO-1582
    #region BindActiveTelephoneListGridCSG
    /// <summary>
    /// Bind data to the active telephone list grid
    /// </summary>
    private void BindActiveTelephoneListGridCSG()
    {
        CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
        List<MBMComparisonResultCSG> listMBMComparisonResult = new List<MBMComparisonResultCSG>();

        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

        listMBMComparisonResult = crmAccount.GetMBMComparisonResultCSG(invoiceId);


        lblActiveTelephoneListCountCSG.Text = listMBMComparisonResult.Where(i => i.Action == (int)CSGActionTypes.AddService
                                                                           || i.Action == (int)CSGActionTypes.AddServiceFeature
                                                                           ).Count().ToString() + " Inserts/Updates/Changes";

        grdActiveTelephoneListCSG.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)CSGActionTypes.AddService
                                                                           || i.Action == (int)CSGActionTypes.AddServiceFeature
                                                                           ).ToList();
        grdActiveTelephoneListCSG.DataBind();



        lblDeactiveTelephoneListCountCSG.Text = listMBMComparisonResult.Where(i => i.Action == (int)CSGActionTypes.RemoveService
                                                                             || i.Action == (int)CSGActionTypes.RemoveServiceFeature
                                                                             ).Count().ToString() + " Cancels";

        grdDeactiveTelephoneListCSG.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)CSGActionTypes.RemoveService
                                                                             || i.Action == (int)CSGActionTypes.RemoveServiceFeature
                                                                             ).ToList();
        grdDeactiveTelephoneListCSG.DataBind();


        if (listMBMComparisonResult.Count() > 0)
        {
            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.ViewChange);

            ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
            objStatus.CompareToCRM = true;
            objStatus.ViewChange = true;
            objStatus.ApproveChange = false;
            objStatus.SyncCRM = false;
            objStatus.ImportCRMData = false;
            objStatus.ProcessInvoice = false;
            objStatus.ExportInvoiceFile = false;

            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
            UpdateProcessWorkFlowStatus(objStatus);

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);
            EnableDisableProcessWorkFlow(obj);

            InvoiceBL invoiceBl = new InvoiceBL(strConnectionString);
            MBMAutomateStatus mbmAutomateStatus = invoiceBl.GetMBMAutomateStatus(invoiceId);
            if (mbmAutomateStatus.InvoiceId != null)
            {
                UpdateAutomationWorkFlowStatus(invoiceId, true, "ViewChanges");
            }

            EnableDisableButtonBasedOnDateLogic(obj);
        }
    }
    #endregion

    #region btnSyncCRM_Click
    /// <summary>
    /// SyncCRM button click logic.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSyncCRM_Click(object sender, EventArgs e)
    {
        try
        {
            btnSyncCRM.Enabled = false;
            int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
            string submittedBy = GetUserName();

            string invoiceNumber = string.Empty;
            invoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            //new 20220308
            // string billingsystem =crmAccount.GetBillingSystem
            List<MBM.Entities.InvoiceType> billingsystem = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
            billingsystem = objInvoiceBL.GetBillingSystem(Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString()));

            //int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

            // 
            SyncCRMLogic syncCRM = new SyncCRMLogic();
            string SyncCRMStatus = "";

            if (billingsystem[0].BillingSystem.ToString() == "CRM")
            {
                // SyncCRMLogic syncCRM = new SyncCRMLogic();
                SyncCRMStatus = syncCRM.SyncCRM(invoiceId, submittedBy, invoiceNumber);
            }
            else
            {
                SyncCSGLogic syncCSG = new SyncCSGLogic();
                string SyncCSGStatus = syncCSG.SyncCSG(invoiceId, submittedBy, invoiceNumber);
            }

            ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(invoiceNumber);
            EnableDisableProcessWorkFlow(obj);

            EnableDisableButtonBasedOnDateLogic(obj);

            if (SyncCRMStatus == "NoRecords")
            {
                btnSyncCRM.Enabled = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('There are no unprocessed records to Sync to CRM')", true);
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to Sync Biller Data");
            LogException(ex, "btnSyncCRM_Click");
        }
    }
    #endregion

    #region DeactivateTCPAuthData
    /// <summary>
    /// This method deactivates the TCPAUTH items in CRM
    /// </summary>
    /// <param name="crmAccount"></param>
    /// <param name="listMBMComparisonResult"></param>
    /// <returns></returns>
    private bool DeactivateTCPAuthData(long snapshotId, string crmAccount, List<CRMDeactivateTCPAUTH> deactivateTCPAuthList)
    {
        //Add TCPAuth Items to be deleted in CRM.
        CRMAccountConfiguration accountConfiguration = new CRMAccountConfiguration();
        List<CRMAccountConfiguration> accountConfigurationList = new List<CRMAccountConfiguration>();

        CRMAccountConfigurationRequest accountConfigRequest = new CRMAccountConfigurationRequest();

        accountConfiguration.DeactivateTCPAUTHs = deactivateTCPAuthList.ToArray();
        accountConfigurationList.Add(accountConfiguration);

        accountConfigRequest.CRMAccountNumber = Convert.ToInt32(crmAccount);
        accountConfigRequest.SnapshotID = snapshotId;

        accountConfigRequest.BillAndSubBillItems = accountConfigurationList.ToArray();

        //CBTService Call.
        CBTSService.CBTSService cbtsService = new CBTSService.CBTSService();
        cbtsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["QuorumCBTSServiceCallTimeout"]);//600000;
        cbtsService.CRMAccountConfiguration(accountConfigRequest);

        return true;
    }
    #endregion

    #region grdActiveTelephoneList_PageIndexChanging
    /// <summary>
    /// Grid active Telephone list page index changing logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdActiveTelephoneList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActiveTelephoneList.PageIndex = e.NewPageIndex;
        BindActiveTelephoneListGrid();
        mdlViewChanges.Show();
    }
    #endregion

    #region grdActiveTelephoneList_PageIndexChanging
    /// <summary>
    /// Grid deactive Telephone list page index changing logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDeactiveTelephoneList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDeactiveTelephoneList.PageIndex = e.NewPageIndex;
        BindActiveTelephoneListGrid();
        mdlViewChanges.Show();
    }
    #endregion

    //SERO-1582

    #region grdActiveTelephoneList_PageIndexChanging
    /// <summary>
    /// Grid active Telephone list page index changing logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdActiveTelephoneListCSG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActiveTelephoneListCSG.PageIndex = e.NewPageIndex;
        BindActiveTelephoneListGridCSG();
        mdlViewChangesCSG.Show();
    }
    #endregion

    #region grdActiveTelephoneListCSG_PageIndexChanging
    /// <summary>
    /// Grid deactive Telephone list page index changing logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDeactiveTelephoneListCSG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDeactiveTelephoneListCSG.PageIndex = e.NewPageIndex;
        BindActiveTelephoneListGridCSG();
        mdlViewChangesCSG.Show();
    }
    #endregion

    #region LogException
    /// <summary>
    /// This method logs the exception into the DB
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="methodName"></param>
    public void LogException(Exception exception, string methodName)
    {
        AppExceptionBL objAppExceptionBL = new AppExceptionBL(strConnectionString);
        AppException objAppException = new AppException();

        objAppException.ErrorMessage = exception.Message;
        objAppException.LoggedInUser = GetUserName();
        objAppException.MethodName = methodName;
        objAppException.StackTrace = exception.StackTrace;

        objAppExceptionBL.InsertApplicationException(objAppException);
    }
    #endregion

    #region GetCurrencyConversion
    /// <summary>
    /// Gets the Currency conversion from the DB.
    /// </summary>
    /// <returns></returns>
    private CurrencyConversion GetCurrencyConversion()
    {
        CurrencyConversion currencyConversion = new CurrencyConversion();

        try
        {
            Invoice invoiceDetails = GetInvoiceNumberDetails();
            int exportCurrencyId = invoiceDetails.ExportCurrencyID;
            List<CurrencyConversion> _importCurrencies = _billing.Configurations.Currencies(DateTime.Now);
            currencyConversion = _importCurrencies.Where(m => m.ID == exportCurrencyId).FirstOrDefault();
            return currencyConversion;
        }
        catch (Exception ex)
        {
            LogException(ex, "GetCurrencyConversion");
            return currencyConversion;
        }
    }
    #endregion

    #region btnExportInvoiceFiles_Click
    /// <summary>
    /// ExportInvoice button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportInvoiceFiles_Click(object sender, EventArgs e)
    {
        try
        {
            int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
            string invoiceNumber = string.Empty;
            invoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);
            int invoiceTypeID = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            string userName = GetUserName();
            bool isByLegalEntity = chkExportProcess.Checked;

            ExportInvoiceLogic exportFile = new ExportInvoiceLogic();
            bool isExported = exportFile.ExportInvoice(invoiceId, invoiceNumber, invoiceTypeID, userName, isByLegalEntity);

            if (isExported)
            {
                ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(invoiceNumber);
                InvoiceBL invoiceBL = new InvoiceBL(strConnectionString);
                List<ExportedFile> exportedFileList = invoiceBL.GetInvoiceFileExports(invoiceId);

                //cbe_8967
                List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
                InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);

                invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
                MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();

                objInvoiceType = invoiceTypeList.Where(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString())).FirstOrDefault();
                string OutputFileFormat = objInvoiceType.OutputFileFormat.ToString();
                ClearGrid();
                if (exportedFileList.Count > 0)
                {
                    //cbe_8967               
                    if (OutputFileFormat.ToString() == "1")
                    {
                        List<ExportedFile> items = exportedFileList.Where(x => x.ExportedFileName.Contains("OneType")).ToList();
                        grdExportFile.DataSource = items;
                        grdExportFile.DataBind();
                    }
                    else
                    {
                        exportedFileList.RemoveAll(x => x.ExportedFileName.Contains("OneType"));
                        grdExportFile.DataSource = exportedFileList;
                        grdExportFile.DataBind();
                    }
                }

                EnableDisableProcessWorkFlow(obj);

                EnableDisableButtonBasedOnDateLogic(obj);
                if (isByLegalEntity)
                {
                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Exported Invoice files by Legal Entity to FTP sucessfully");
                }
                else
                {
                    Controls_ErrorSuccessNotifier.AddSuccessMessage(invoiceNumber + " File exported to FTP sucessfully");
                }
            }
            else
            {
                Controls_ErrorSuccessNotifier.AddErrorMessage("There is no FTP path defined for " + ddlInvoiceType.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occured while Exporting Invoice");
            LogException(ex, "btnExportInvoiceFiles_Click");
        }
    }
    #endregion

    #region btnApproveChanges_Click
    /// <summary>
    /// ApproveChanges button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApproveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.ApproveChange);
            ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
            objStatus.CompareToCRM = true;
            objStatus.ViewChange = true;
            CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
            List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();
            List<MBMComparisonResultCSG> listMBMComparisonResultCSG = new List<MBMComparisonResultCSG>();

            //new 20220308
            // string billingsystem =crmAccount.GetBillingSystem
            List<MBM.Entities.InvoiceType> billingsystem = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
            billingsystem = objInvoiceBL.GetBillingSystem(Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString()));

            int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

            // 
            if (billingsystem[0].BillingSystem.ToString() == "CRM")
            {
                listMBMComparisonResult = crmAccount.GetUnprocessedMBMComparisonResult(invoiceId);
            }
            else
            {
                listMBMComparisonResultCSG = crmAccount.GetUnprocessedMBMComparisonResultCSG(invoiceId);
            }


            if (listMBMComparisonResult.Count > 0 || listMBMComparisonResultCSG.Count > 0)
            {
                objStatus.ApproveChange = true;
            }
            else
            {
                objStatus.ApproveChange = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('There are no unprocessed records to Approve')", true);
            }
            objStatus.SyncCRM = false;
            objStatus.ImportCRMData = false;
            objStatus.ProcessInvoice = false;
            objStatus.ExportInvoiceFile = false;

            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
            UpdateProcessWorkFlowStatus(objStatus);

            InvoiceBL invoiceBl = new InvoiceBL(strConnectionString);
            MBMAutomateStatus mbmAutomateStatus = invoiceBl.GetMBMAutomateStatus(invoiceId);
            if (mbmAutomateStatus.InvoiceId != null)
            {
                UpdateAutomationWorkFlowStatus(invoiceId, true, "ApproveChanges");
            }

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);
            EnableDisableProcessWorkFlow(obj);

            EnableDisableButtonBasedOnDateLogic(obj);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to Approve Changes");
            LogException(ex, "btnApproveChanges_Click");
        }
    }
    #endregion

    #region btnProcessInvoice_Click
    /// <summary>
    /// ProcessInvoice button click logic 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProcessInvoice_Click(object sender, EventArgs e)
    {
        RecordTypes objRecordTypes = new RecordTypes(strConnectionString);
        Invoice invoicedetail = new Invoice();
        string recordType1Processing = string.Empty;
        string recordType2Processing = string.Empty;
        string recordType3Processing = string.Empty;
        string recordType4Processing = string.Empty;
        string recordType5Processing = string.Empty;
        string RT1SummaryProcessing = string.Empty;
        string RT2SummaryProcessing = string.Empty;
        string RT4SummaryProcessing = string.Empty;

        int invoiceID = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

        try
        {
            invoicedetail = GetInvoiceNumberDetails();
            LoadInvoiceDetails(invoicedetail);

            if (invoicedetail != null)
            {
                if (invoicedetail.ExportCurrencyID != null && invoicedetail.ExportCurrencyID > 0)
                {
                    //RecordType1 Process
                    try
                    {
                        recordType1Processing = objRecordTypes.ProcessRecordType1(invoiceID, GetUserName(), invoicedetail.ExportCurrencyID);
                        if (!String.IsNullOrEmpty(recordType1Processing))
                        {
                            if (recordType1Processing == "Completed")
                            {
                                //validate RecordType1
                                bool recordType1Validate = objRecordTypes.ValidateRecordType1(invoiceID);
                                if (recordType1Validate)
                                {
                                    int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, null, null, null, null, GetUserName());
                                }
                            }
                            else if (recordType1Processing == "Failed")
                            {
                                int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, null, null, null, null, GetUserName());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RecordType1");
                        LogException(ex, "ProcessRecordType1");
                    }

                    //RecordType2 Process
                    try
                    {
                        recordType2Processing = objRecordTypes.ProcessRecordType2(invoiceID, GetUserName(), invoicedetail.ExportCurrencyID);
                        if (!String.IsNullOrEmpty(recordType2Processing))
                        {
                            if (recordType2Processing == "Completed")
                            {
                                //validate RecordType2
                                bool recordType2Validate = objRecordTypes.ValidateRecordType2(invoiceID);
                                if (recordType2Validate)
                                {
                                    int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, null, null, null, GetUserName());
                                }
                            }
                            else if (recordType2Processing == "Failed")
                            {
                                int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, null, null, null, GetUserName());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RecordType2");
                        LogException(ex, "ProcessRecordType2");
                    }

                    //RT1Summary Process
                    try
                    {
                        RT1SummaryProcessing = objRecordTypes.ProcessRT1Summary(invoiceID, GetUserName());
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RT1Summary");
                        LogException(ex, "ProcessRT1Summary");
                    }

                    //RT2Summary Process
                    try
                    {
                        RT2SummaryProcessing = objRecordTypes.ProcessRT2Summary(invoiceID, GetUserName());
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RT2Summary");
                        LogException(ex, "ProcessRT2Summary");
                    }

                    //RecordType4 Process
                    try
                    {
                        recordType4Processing = objRecordTypes.ProcessRecordType4(invoiceID, GetUserName(), invoicedetail.ExportCurrencyID);
                        if (!String.IsNullOrEmpty(recordType4Processing))
                        {
                            if (recordType4Processing == "Completed")
                            {
                                //validate RecordType4
                                bool recordType4Validate = objRecordTypes.ValidateRecordType4(invoiceID);
                                if (recordType4Validate)
                                {
                                    int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, recordType4Processing, null, GetUserName());
                                }
                            }
                            else if (recordType4Processing == "Failed")
                            {
                                int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, recordType4Processing, null, GetUserName());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RecordType4");
                        LogException(ex, "ProcessRecordType4");
                    }

                    //RT4Summary Process
                    try
                    {
                        RT4SummaryProcessing = objRecordTypes.ProcessRT4Summary(invoiceID, GetUserName());
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RT2Summary");
                        LogException(ex, "ProcessRT4Summary");
                    }

                    //RecordType3 Process
                    try
                    {
                        recordType3Processing = objRecordTypes.ProcessRecordType3(invoiceID, GetUserName(), invoicedetail.ExportCurrencyID);
                        if (!String.IsNullOrEmpty(recordType3Processing))
                        {
                            if (recordType3Processing == "Completed")
                            {
                                //validate RecordType3
                                bool recordType3Validate = objRecordTypes.ValidateRecordType3(invoiceID);
                                if (recordType3Validate)
                                {
                                    int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, null, null, GetUserName());
                                }
                            }
                            else if (recordType3Processing == "Failed")
                            {
                                int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, null, null, GetUserName());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RecordType3");
                        LogException(ex, "ProcessRecordType3");
                    }

                    //RecordType5 Process
                    try
                    {
                        recordType5Processing = objRecordTypes.ProcessRecordType5(invoiceID, GetUserName(), invoicedetail.ExportCurrencyID);
                        if (!String.IsNullOrEmpty(recordType5Processing))
                        {
                            if (recordType5Processing == "Completed")
                            {
                                //validate RecordType5
                                bool recordType5Validate = objRecordTypes.ValidateRecordType5(invoiceID);
                                if (recordType5Validate)
                                {
                                    int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, recordType4Processing, recordType5Processing, GetUserName());
                                }
                            }
                            else if (recordType5Processing == "Failed")
                            {
                                int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, recordType4Processing, recordType5Processing, GetUserName());
                            }
                        }

                        //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.ProcessInvoice);
                        ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
                        objStatus.CompareToCRM = true;
                        objStatus.ViewChange = true;
                        objStatus.ApproveChange = true;
                        objStatus.SyncCRM = true;
                        objStatus.ImportCRMData = true;
                        objStatus.ProcessInvoice = true;
                        objStatus.ExportInvoiceFile = false;

                        UpdateProcessWorkFlowStatus(objStatus);

                        string strInvoiceNumber = string.Empty;
                        strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

                        ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);
                        EnableDisableProcessWorkFlow(obj);

                        EnableDisableButtonBasedOnDateLogic(obj);
                    }
                    catch (Exception ex)
                    {
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Processing the RecordType5");
                        LogException(ex, "ProcessRecordType5");
                    }
                }

                ////updating RecordTypes Status for the Invoice Processed
                //if (!String.IsNullOrEmpty(recordType1Processing) && !String.IsNullOrEmpty(recordType2Processing) && !String.IsNullOrEmpty(recordType3Processing)
                //    && !String.IsNullOrEmpty(recordType4Processing) && !String.IsNullOrEmpty(recordType5Processing))
                //{
                //    //validate RecordType1
                //    bool recordType1Validate = objRecordTypes.ValidateRecordType1(invoiceID);
                //    //validate RecordType2
                //    bool recordType2Validate = objRecordTypes.ValidateRecordType2(invoiceID);
                //    //validate RecordType3
                //    bool recordType3Validate = objRecordTypes.ValidateRecordType3(invoiceID);
                //    //validate RecordType4
                //    bool recordType4Validate = objRecordTypes.ValidateRecordType4(invoiceID);
                //    //validate RecordType5
                //    bool recordType5Validate = objRecordTypes.ValidateRecordType5(invoiceID);

                //    if (recordType1Validate && recordType2Validate && recordType3Validate && recordType4Validate && recordType5Validate)
                //    {
                //        int result = objRecordTypes.UpdateInvoiceRecordTypes(invoiceID, recordType1Processing, recordType2Processing, recordType3Processing, recordType4Processing, recordType5Processing, GetUserName());
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while processing the Invoice");
            LogException(ex, "btnProcessInvoice_Click");
        }
    }
    #endregion

    #region btnImportCRMData_Click
    /// <summary>
    /// ImportCRM button click logic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImportCRMData_Click(object sender, EventArgs e)
    {
        CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
        try
        {
            string strInvoiceNumber = string.Empty;
            string strUserName = string.Empty;

            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);
            strUserName = GetUserName();

            int? invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            // string billingsystem =crmAccount.GetBillingSystem
            List<MBM.Entities.InvoiceType> billingsystem = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
            billingsystem = objInvoiceBL.GetBillingSystem(invoiceTypeId);

            if (billingsystem[0].BillingSystem.ToString() == "CRM")
            {
                crmAccount.GetSyncedCRMData(strInvoiceNumber, strUserName);

            }
            else
            {
                crmAccount.GetSyncedCSGData(strInvoiceNumber, strUserName);

            }
            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.ImportCRMData);

            ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
            objStatus.CompareToCRM = true;
            objStatus.ViewChange = true;
            objStatus.ApproveChange = true;
            objStatus.SyncCRM = true;
            objStatus.ImportCRMData = true;
            objStatus.ProcessInvoice = false;
            objStatus.ExportInvoiceFile = false;

            UpdateProcessWorkFlowStatus(objStatus);

            ProcessWorkFlowStatus obj = GetProcessWorkflowStatus(strInvoiceNumber);

            EnableDisableProcessWorkFlow(obj);

            EnableDisableButtonBasedOnDateLogic(obj);


        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while retrieving the latest CRMData");
            LogException(ex, "btnImportCRMData_Click");
        }
    }
    #endregion

    #region GetProcessWorkflowStatus
    /// <summary>
    /// Gets the process workflow status to show on top of workflow buttons
    /// </summary>
    /// <param name="strInvoiceNumber"></param>
    /// <returns></returns>
    private ProcessWorkFlowStatus GetProcessWorkflowStatus(string strInvoiceNumber)
    {
        ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
        try
        {
            _billing = new BillingEngine(strConnectionString);
            objStatus = _billing.ProcessWorkFlowStatusBL.GetProcessWorkFlowStatusByInvoiceNumber(strInvoiceNumber);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to get process workflow status");
            LogException(ex, "GetProcessWorkflowStatus");
        }
        return objStatus;
    }
    #endregion

    #region EnableDisableProcessWorkFlow
    /// <summary>
    /// Enables or Disables the workflow buttons based on the workflow status
    /// </summary>
    /// <param name="status"></param>
    private void EnableDisableProcessWorkFlow(ProcessWorkFlowStatus status)
    {
        if (status.ExportInvoiceFile)
        {
            btnExportInvoiceFiles.Enabled = true;
            liExportInvoiceFiles.Attributes["class"] = "completed";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "completed";
            btnImportCRMData.Enabled = false;
            //btnImportCRMData.ToolTip = "This button is disabled as the CRM data is already imported";
            btnImportCRMData.ToolTip = "";
            liImportCRMData.Attributes["class"] = "completed";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "completed";
            btnApproveChanges.Enabled = false;
            liApproveChanges.Attributes["class"] = "completed";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "completed";
            btnCompareToCRM.Enabled = false;
            liCompareToCRM.Attributes["class"] = "completed";

            achExportFile.Attributes["class"] = "accordion active";
            divExportFile.Attributes["class"] = "panel show";

            chkExportProcess.Visible = true;
            //chkExportProcess.Enabled = true;
            spnExportText.Visible = true;

        }
        else if (status.ProcessInvoice)
        {
            btnExportInvoiceFiles.Enabled = true;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "completed";
            btnImportCRMData.Enabled = false;
            //btnImportCRMData.ToolTip = "This button is disabled as the CRM data is already imported";
            btnImportCRMData.ToolTip = "";
            liImportCRMData.Attributes["class"] = "completed";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "completed";
            btnApproveChanges.Enabled = false;
            liApproveChanges.Attributes["class"] = "completed";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "completed";
            btnCompareToCRM.Enabled = false;
            liCompareToCRM.Attributes["class"] = "completed";

            chkExportProcess.Visible = true;
            chkExportProcess.Enabled = true;
            //chkExportProcess.Checked = true;
            spnExportText.Visible = true;
        }
        else if (status.ImportCRMData)
        {
            btnExportInvoiceFiles.Enabled = false;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = true;
            liProcessInvoice.Attributes["class"] = "";
            btnImportCRMData.Enabled = false;
            //btnImportCRMData.ToolTip = "This button is disabled as the CRM data is already imported";
            btnImportCRMData.ToolTip = "";
            liImportCRMData.Attributes["class"] = "completed";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "completed";
            btnApproveChanges.Enabled = false;
            liApproveChanges.Attributes["class"] = "completed";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "completed";
            btnCompareToCRM.Enabled = false;
            liCompareToCRM.Attributes["class"] = "completed";

            chkExportProcess.Visible = false;
            chkExportProcess.Enabled = false;
            spnExportText.Visible = false;
        }
        else if (status.SyncCRM)
        {
            btnExportInvoiceFiles.Enabled = false;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "";

            btnImportCRMData.Enabled = true;
            liImportCRMData.Attributes["class"] = "";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "completed";
            btnApproveChanges.Enabled = false;
            liApproveChanges.Attributes["class"] = "completed";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "completed";
            btnCompareToCRM.Enabled = false;
            liCompareToCRM.Attributes["class"] = "completed";

            chkExportProcess.Visible = false;
            chkExportProcess.Enabled = false;
            spnExportText.Visible = false;
        }
        else if (status.ApproveChange)
        {
            btnExportInvoiceFiles.Enabled = false;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "";
            btnImportCRMData.Enabled = false;
            liImportCRMData.Attributes["class"] = "";

            //Sync will be done through automation only unless pre billing overriden                        
            if (isAutomationInvoiceOverriden())
            {
                btnSyncCRM.Enabled = false;
            }
            else
            {
                btnSyncCRM.Enabled = true;
            }

            liSyncCRM.Attributes["class"] = "";
            btnApproveChanges.Enabled = true;
            liApproveChanges.Attributes["class"] = "completed";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "completed";
            btnCompareToCRM.Enabled = true;
            liCompareToCRM.Attributes["class"] = "completed";

            chkExportProcess.Visible = false;
            chkExportProcess.Enabled = false;
            spnExportText.Visible = false;
        }
        else if (status.ViewChange)
        {
            btnExportInvoiceFiles.Enabled = false;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "";
            btnImportCRMData.Enabled = false;
            liImportCRMData.Attributes["class"] = "";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "";
            btnApproveChanges.Enabled = true;
            liApproveChanges.Attributes["class"] = "";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "completed";
            btnCompareToCRM.Enabled = true;
            liCompareToCRM.Attributes["class"] = "completed";

            chkExportProcess.Visible = false;
            chkExportProcess.Enabled = false;
            spnExportText.Visible = false;
        }
        else if (status.CompareToCRM)
        {
            btnExportInvoiceFiles.Enabled = false;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "";
            btnImportCRMData.Enabled = false;
            liImportCRMData.Attributes["class"] = "";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "";
            btnApproveChanges.Enabled = false;
            liApproveChanges.Attributes["class"] = "";
            btnViewChanges.Enabled = true;
            liViewChanges.Attributes["class"] = "";
            btnCompareToCRM.Enabled = true;
            liCompareToCRM.Attributes["class"] = "completed";

            chkExportProcess.Visible = false;
            chkExportProcess.Enabled = false;
            spnExportText.Visible = false;
        }
        else
        {
            btnExportInvoiceFiles.Enabled = false;
            liExportInvoiceFiles.Attributes["class"] = "";
            btnProcessInvoice.Enabled = false;
            liProcessInvoice.Attributes["class"] = "";
            btnImportCRMData.Enabled = false;
            liImportCRMData.Attributes["class"] = "";
            btnSyncCRM.Enabled = false;
            liSyncCRM.Attributes["class"] = "";
            btnApproveChanges.Enabled = false;
            liApproveChanges.Attributes["class"] = "";
            btnViewChanges.Enabled = false;
            liViewChanges.Attributes["class"] = "";
            btnCompareToCRM.Enabled = true;
            liCompareToCRM.Attributes["class"] = "";

            chkExportProcess.Visible = false;
            chkExportProcess.Enabled = false;
            spnExportText.Visible = false;
        }
    }
    #endregion

    #region UpdateProcessWorkFlowStatus
    /// <summary>
    /// This method updates the Process workflow status after every workflow button click.
    /// </summary>
    /// <param name="buttonStatus"></param>
    //private void UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons buttonStatus)
    private void UpdateProcessWorkFlowStatus(ProcessWorkFlowStatus buttonStatus)
    {
        string strInvoiceNumber = string.Empty;
        strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

        ProcessWorkFlowStatus obj = new ProcessWorkFlowStatus();

        obj.InvoiceNumber = strInvoiceNumber;
        //obj.CompareToCRM = buttonStatus == ProcessWorkFlowButtons.CompareToCRM ? true : false;
        //obj.ViewChange = buttonStatus == ProcessWorkFlowButtons.ViewChange ? true : false;
        //obj.ApproveChange = buttonStatus == ProcessWorkFlowButtons.ApproveChange ? true : false;
        //obj.SyncCRM = buttonStatus == ProcessWorkFlowButtons.SyncCRM ? true : false;
        //obj.ImportCRMData = buttonStatus == ProcessWorkFlowButtons.ImportCRMData ? true : false;
        //obj.ProcessInvoice = buttonStatus == ProcessWorkFlowButtons.ProcessInvoice ? true : false;
        //obj.ExportInvoiceFile = buttonStatus == ProcessWorkFlowButtons.ExportInvoiceFile ? true : false;
        obj.CompareToCRM = buttonStatus.CompareToCRM;
        obj.ViewChange = buttonStatus.ViewChange;
        obj.ApproveChange = buttonStatus.ApproveChange;
        obj.SyncCRM = buttonStatus.SyncCRM;
        obj.ImportCRMData = buttonStatus.ImportCRMData;
        obj.ProcessInvoice = buttonStatus.ProcessInvoice;
        obj.ExportInvoiceFile = buttonStatus.ExportInvoiceFile;

        try
        {
            _billing = new BillingEngine(strConnectionString);
            _billing.ProcessWorkFlowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(obj);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to get process workflow status");
            LogException(ex, "GetProcessWorkflowStatus");
        }
    }
    #endregion

    #region UpdateAutomationWorkFlowStatus
    /// <summary>
    /// This method updates the Automation workflow status after every workflow button click.
    /// </summary>
    /// <param name="buttonStatus"></param>    
    private void UpdateAutomationWorkFlowStatus(int invoiceId, bool value, string statusOf)
    {
        try
        {
            _billing = new BillingEngine(strConnectionString);
            _billing.ProcessWorkFlowStatusBL.UpdateAutomationWorkFlowStatusByInvoiceId(invoiceId, value, statusOf);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to get process workflow status");
            LogException(ex, "GetProcessWorkflowStatus");
        }
    }
    #endregion

    #region ExportComparisonDataToExcel
    /// <summary>
    /// This method exports the gridview data to MS Excel
    /// </summary>
    /// <param name="strInsertsOrCancels"></param>
    private void ExportComparisonDataToExcel(string strInsertsOrCancels)
    {
        try
        {
            CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
            List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();

            List<MBMComparisonResultCSG> listMBMComparisonResultCSG = new List<MBMComparisonResultCSG>();
            int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

            if (strInsertsOrCancels == "MBMComparisonInsertsCSG" || strInsertsOrCancels == "MBMComparisonCancelsCSG")
            {
                listMBMComparisonResultCSG = crmAccount.GetMBMComparisonResultCSG(invoiceId);
            }
            else
            {
                listMBMComparisonResult = crmAccount.GetMBMComparisonResult(invoiceId);
            }

            GridView grdExport = new GridView();
            grdExport.AllowPaging = false;

            switch (strInsertsOrCancels)
            {
                case "MBMComparisonInserts":
                    grdExport.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddBillItem
                                                                           || i.Action == (int)ActionTypes.AddCharge
                                                                           || i.Action == (int)ActionTypes.UpdateCharge
                                                                           || i.Action == (int)ActionTypes.AddTelephone
                                                                           || i.Action == (int)ActionTypes.ChangeTelephone).ToList();
                    break;

                case "MBMComparisonCancels":
                    grdExport.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteBillItem
                                                                             || i.Action == (int)ActionTypes.DeleteCharge
                                                                             || i.Action == (int)ActionTypes.DeleteTelephone).ToList();
                    break;

                case "MBMComparisonInsertsCSG":
                    grdExport.DataSource = listMBMComparisonResultCSG.Where(i => i.Action == (int)CSGActionTypes.AddService
                                                                           || i.Action == (int)CSGActionTypes.AddServiceFeature
                                                                        ).ToList();
                    break;

                case "MBMComparisonCancelsCSG":
                    grdExport.DataSource = listMBMComparisonResultCSG.Where(i => i.Action == (int)CSGActionTypes.RemoveService
                                                                             || i.Action == (int)CSGActionTypes.RemoveServiceFeature
                                                                             ).ToList();
                    break;

            }

            grdExport.DataBind();

            //grdExport.Columns[11].Visible = false;

            //Apply style to Individual Cells
            for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
            {
                grdExport.HeaderRow.Cells[j].Style.Add("background-color", "#E48D0F");

                if (grdExport.HeaderRow.Cells[j].Text.Equals("Action"))
                {
                    grdExport.HeaderRow.Cells[j].Visible = false;
                }
            }

            for (int i = 0; i < grdExport.Rows.Count; i++)
            {
                GridViewRow row = grdExport.Rows[i];

                //Hide the column "Action"
                row.Cells[11].Visible = false;

                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
                    {
                        row.Cells[j].Style.Add("background-color", "#C2D69B");
                    }
                }
            }

            //BindActiveTelephoneListGrid();
            mdlViewChanges.Show();

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strInsertsOrCancels + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xls");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);

            grdExport.RenderControl(htmlTextWriter);
            System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            //Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while exporting the data");
            LogException(ex, "ExportToExcel");
        }
    }
    #endregion

    #region ExportComparisonDataToExcel_Updated(cbe_11609)


    public DataTable GetMBMComparisonResultDataTable(List<MBMComparisonResult> listMBMComparisonResult)
    {
        string tableName1 = "MBM Comparison Result Data";
        DataTable dt1 = new DataTable(tableName1);

        try
        {
            dt1.Columns.Add("Snapshot Id", typeof(string));
            dt1.Columns.Add("Invoice Id", typeof(string));
            dt1.Columns.Add("CRMAccountNumber", typeof(string));
            dt1.Columns.Add("AssetSearchCode1", typeof(string));
            dt1.Columns.Add("AssetSearchCode2", typeof(string));
            dt1.Columns.Add("Profile Name", typeof(string));
            dt1.Columns.Add("Feature Code", typeof(string));
            dt1.Columns.Add("GL Code", typeof(string));
            dt1.Columns.Add("Charge", typeof(decimal));
            dt1.Columns.Add("Action Type", typeof(string));
            dt1.Columns.Add("Action", typeof(string));
            dt1.Columns.Add("Start Date", typeof(string));
            dt1.Columns.Add("End Date", typeof(string));
            dt1.Columns.Add("ItemType", typeof(string));
            dt1.Columns.Add("SubType", typeof(string));
            dt1.Columns.Add("SwitchId", typeof(string));
            dt1.Columns.Add("ItemId", typeof(string));
            dt1.Columns.Add("SequenceId", typeof(string));
            dt1.Columns.Add("LoadId", typeof(string));
            dt1.Columns.Add("Processed", typeof(string));

            foreach (var items in listMBMComparisonResult)
            {
                var row = dt1.NewRow();
                row["Snapshot Id"] = items.SnapShotId;
                row["Invoice Id"] = items.InvoiceId;
                row["CRMAccountNumber"] = items.CRMAccountNumber;
                row["AssetSearchCode1"] = items.AssetSearchCode1;
                row["AssetSearchCode2"] = items.AssetSearchCode2;
                row["Profile Name"] = items.ProfileName;
                row["Feature Code"] = items.FeatureCode;
                row["GL Code"] = items.GLCode;
                row["Charge"] = items.Charge;
                row["Action Type"] = items.ActionType;
                row["Action"] = items.Action;
                row["Start Date"] = items.StartDate;
                row["End Date"] = items.EndDate;
                row["ItemType"] = items.ItemType;
                row["SubType"] = items.SubType;
                row["SwitchId"] = items.SwitchId;
                row["ItemId"] = items.ItemId;
                row["SequenceId"] = items.SequenceId;
                row["LoadId"] = items.LoadId;
                row["Processed"] = items.Processed;

                dt1.Rows.Add(row);
            }
            return dt1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// This method exports the gridview data to MS Excel, cbe_11609
    /// </summary>
    /// <param name="strInsertsOrCancels"></param>
    private void ExportComparisonDataToExcel_Updated(string strInsertsOrCancels)
    {
        try
        {
            CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
            List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();

            int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
            listMBMComparisonResult = crmAccount.GetMBMComparisonResult(invoiceId);

            GridView grdExport = new GridView();
            grdExport.AllowPaging = false;

            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();

            switch (strInsertsOrCancels)
            {
                case "MBMComparisonInserts":
                    listMBMComparisonResult = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddBillItem
                                                                           || i.Action == (int)ActionTypes.AddCharge
                                                                           || i.Action == (int)ActionTypes.UpdateCharge
                                                                           || i.Action == (int)ActionTypes.AddTelephone
                                                                           || i.Action == (int)ActionTypes.ChangeTelephone).ToList();
                    dt1 = GetMBMComparisonResultDataTable(listMBMComparisonResult);
                    ds.Tables.Add(dt1);
                    break;

                case "MBMComparisonCancels":
                    grdExport.DataSource = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteBillItem
                                                                             || i.Action == (int)ActionTypes.DeleteCharge
                                                                             || i.Action == (int)ActionTypes.DeleteTelephone).ToList();
                    dt1 = GetMBMComparisonResultDataTable(listMBMComparisonResult);
                    ds.Tables.Add(dt1);
                    break;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                using (ExcelPackage xp = new ExcelPackage())
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);

                        int rowstart = 2;
                        int colstart = 2;
                        int rowend = rowstart;
                        int colend = colstart + dt.Columns.Count;

                        ws.Cells[rowstart, colstart, rowend, colend].Merge = true;
                        ws.Cells[rowstart, colstart, rowend, colend].Value = dt.TableName;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Bold = true;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                        rowstart += 2;
                        rowend = rowstart + dt.Rows.Count;
                        ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                        int i = 1;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            i++;
                            if (dc.DataType == typeof(decimal))
                                ws.Column(i).Style.Numberformat.Format = "#0.00";
                        }
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();



                        ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                           ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                           ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                           ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                    Response.AddHeader("content-disposition", "attachment;filename=" + strInsertsOrCancels + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(xp.GetAsByteArray());
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            //Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while exporting the data");
            LogException(ex, "ExportToExcel");
        }
    }
    #endregion

    #region btnInsertsExportToExcel_Click
    /// <summary>
    /// Export to Excel button logic for CRM Inserts.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsertsExportToExcel_Click(object sender, EventArgs e)
    {
        //ExportComparisonDataToExcel("MBMComparisonInserts");
        ExportComparisonDataToExcel_Updated("MBMComparisonInserts");//cbe_11609
    }
    #endregion

    #region btnCancelsExportToExcel_Click
    /// <summary>
    /// Export to Excel button logic for CRM Cancels.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelsExportToExcel_Click(object sender, EventArgs e)
    {
        ExportComparisonDataToExcel("MBMComparisonCancels");
    }
    #endregion


    #region btnInsertsExportToExcelCSG_Click
    /// <summary>
    /// Export to Excel button logic for CRM Inserts.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsertsExportToExcelCSG_Click(object sender, EventArgs e)
    {
        ExportComparisonDataToExcel("MBMComparisonInsertsCSG");
    }
    #endregion

    #region btnCancelsExportToExcelCSG_Click
    /// <summary>
    /// Export to Excel button logic for CRM Cancels.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelsExportToExcelCSG_Click(object sender, EventArgs e)
    {
        ExportComparisonDataToExcel("MBMComparisonCancelsCSG");
    }
    #endregion


    #region GetInvoiceNumberFromInvoiceNumberDropDownList
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strInvoiceNumberFromDropDownList"></param>
    /// <returns></returns>
    private string GetInvoiceNumberFromInvoiceNumberDropDownList(string strInvoiceNumberFromDropDownList)
    {
        string strInvoiceNumber = string.Empty;

        string[] strArr = strInvoiceNumberFromDropDownList.Split(new Char[] { '-' }, StringSplitOptions.None);
        strInvoiceNumber = strArr[0].Trim();

        return strInvoiceNumber;
    }
    #endregion

    #region btnNewInvoice_Click
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewInvoice_Click(object sender, EventArgs e)
    {
        lblNewInvoiceErrorMessage.Visible = false;
        lblNewInvoiceErrorMessage.Text = string.Empty;

        if (!ddlInvoiceType.SelectedItem.Text.Equals(DEFAULT_INVOICETYPE_TEXT))
        {
            ddlNewInvoiceType.SelectedIndex = ddlInvoiceType.SelectedIndex - 1;
        }
        else
        {
            ddlNewInvoiceType.SelectedIndex = 0;
        }

        mdlNewInvoice.Show();
    }
    #endregion

    protected void RestrictInvoiceDisplay(object sender, EventArgs e)
    {

        ddlInvoiceType_SelectedIndexChanged(sender, e);
    }

    private bool isAutomationInvoiceOverriden()
    {
        bool hideSync = false;
        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
        InvoiceBL invoiceBL = new InvoiceBL(strConnectionString);
        MBMAutomateStatus mbmAutomateStatus = invoiceBL.GetMBMAutomateStatus(invoiceId);
        if (mbmAutomateStatus.InvoiceId != null && !mbmAutomateStatus.isPreBillingOverriden.GetValueOrDefault())
        {
            hideSync = true;
        }
        return hideSync;
    }

    private bool isInvoiceApprovedByApi()
    {
        bool hideApproveBtn = false;
        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
        InvoiceBL invoiceBL = new InvoiceBL(strConnectionString);
        MBMAutomateStatus mbmAutomateStatus = invoiceBL.GetMBMAutomateStatus(invoiceId);
        if (mbmAutomateStatus.InvoiceId != null && mbmAutomateStatus.IsApprovedChange == true && !mbmAutomateStatus.isPreBillingOverriden.GetValueOrDefault())
        {
            hideApproveBtn = true;
        }
        return hideApproveBtn;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {


        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

        InvoiceBL invoiceBL = new InvoiceBL(strConnectionString);

        MBMAutomateStatus mbmAutomateStatus = invoiceBL.GetMBMAutomateStatus(invoiceId);
        if (mbmAutomateStatus.InvoiceId != null)
        {
            divAutomationStatus.Visible = true;
            lblAutomationStatus.Text = mbmAutomateStatus.LastAction;
            chkPreBillOverrideEditable.Checked = mbmAutomateStatus.isPreBillingOverriden.GetValueOrDefault();
        }
        mdlPreBillOverride.Show();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());

        try
        {
            LinkButton lbUserName = (LinkButton)Master.FindControl("linkUserName");
            string userName = lbUserName.Text.Split('(')[0].TrimEnd().ToString();

            string strInvoiceNumber = string.Empty;
            strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

            int InvoiceType = 0;
            if (IsQueryString)
            {
                InvoiceType = Convert.ToInt32(InvoiceTypeQS);
            }
            else
            {
                InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
            }

            string customer = InvoiceTypeLists().Where(c => c.ID == InvoiceType).Select(x => x.Name).FirstOrDefault();

            string month = lblInvoiceBillingDate.Text;

            string subject = "";
            string messageBody = "";
            if (chkPreBillOverrideEditable.Checked)
            {
                subject = string.Format("{0} ({1}) - Pre-Billing Manual Override Enabled", customer, month);
                messageBody = string.Format("Pre-Billing Manual Override is Enabled for Invoice number {0} by {1}.", strInvoiceNumber, userName);
            }
            else
            {
                subject = string.Format("{0} ({1}) - Pre-Billing Manual Override Disabled", customer, month);
                messageBody = string.Format("Pre-Billing Manual Override is Disabled for Invoice number {0} by {1}.", strInvoiceNumber, userName);
            }

            OverrideMailSend(subject, messageBody);
            UpdateAutomationWorkFlowStatus(invoiceId, chkPreBillOverrideEditable.Checked, "PreBillingOverride");

            chkPreBillOverride.Checked = chkPreBillOverrideEditable.Checked;

            ddlInvoiceNumber_SelectedIndexChanged(null, null);
            Controls_ErrorSuccessNotifier.AddSuccessMessage(string.Format("Pre-Billing Manual Override is {0} now", (chkPreBillOverrideEditable.Checked ? "Enabled" : "Disabled")));
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to change Pre-Billing Manual Override status");
            LogException(ex, "OverridePrebilling");
        }
    }

    private void OverrideMailSend(string subject, string messageBody)
    {
        try
        {
            var newline = "<br/>";
            string EmailSignature = newline + "Thanks and Regards" + newline + "    " + "     " + "<b>Application Admin </b>" + "</br>";
            string body = "<b>Dear User,</b>" + "<br/>" + newline + newline + string.Format("{0}", messageBody) + newline + newline + EmailSignature;

            MBM.BillingEngine.SendMail billingEngine = new MBM.BillingEngine.SendMail();
            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"].ToString());

            System.Collections.Specialized.StringCollection toAddress = new System.Collections.Specialized.StringCollection();
            toAddress.Add(ConfigurationManager.AppSettings["EmailToAddress"].ToString());

            billingEngine.SendMailMessage(fromAddress, toAddress, subject, body);
        }
        catch (Exception ex)
        {
            LogException(ex, "OverrideMailSend");
        }
    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {

    }
}

