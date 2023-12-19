using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MBM.Authenticate;
using MBM.Entities;
using MBM.BillingEngine;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data;
using OfficeOpenXml;

public partial class Reports_Report : System.Web.UI.Page
{
    //ER 6775
    #region Constants
    /// <summary>
    /// 
    /// </summary>
    public const string DEFAULT_INVOICETYPE_TEXT = "- Select Customer -";
    public const string DEFAULT_INVOICENUMBER_TEXT = "- Invoice Number -";
    public const string DEFAULT_INVOICE_VALUE = "-1";
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

    private BillingEngine _billing;
    string connectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        Authenticate objAuth = new Authenticate();
        bool blIsValid = false;
        string strLoginUser = string.Empty;
        strLoginUser = GetUserName();

        blIsValid = ValidateUser(connectionString, strLoginUser);

        //ER 6775
        if (blIsValid)
        {
            if (ddlInvoiceType.Items.Count == 0 && !ddlDataType.SelectedValue.Equals("EDI"))
            {
                LoadInvoiceTypeDDL();
                ddlInvoiceType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("All", "0"));
            }

            if (ddlDataType.SelectedValue.Equals("EDI"))
            {
                ddlInvoiceType.Items.Clear();
                ddlInvoiceType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("SOO", "0"));
            }
        }
    }

    //ER 6775 Start
    #region LoadInvoiceTypeDDL
    /// <summary>
    /// Loads Invoice Type dropdown list
    /// </summary>
    private void LoadInvoiceTypeDDL()
    {
        try
        {
            Authenticate objAuth = new Authenticate();
            Users objUser = objAuth.AuthenticateUser(connectionString, GetUserName());

            List<InvoiceType> invoiceTypeList = new List<InvoiceType>();

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator))
                    {
                        invoiceTypeList = InvoiceTypeListsCustomerAdmin(objUser.UserId);
                    }
                    else
                    {
                        invoiceTypeList = InvoiceTypeLists();
                    }
                }
            }

            ddlInvoiceType.Items.Clear();
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

    //ER 9179 Start
    #region LoadInvoiceTypeDDLSOO
    /// <summary>
    /// Loads Invoice Type dropdown list
    /// </summary>
    private void LoadInvoiceTypeDDLSOO()
    {
        try
        {
            Authenticate objAuth = new Authenticate();
            Users objUser = objAuth.AuthenticateUser(connectionString, GetUserName());

            List<InvoiceType> invoiceTypeList = new List<InvoiceType>();

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator))
                    {
                        invoiceTypeList = InvoiceTypeListsSOOCustomerAdmin(objUser.UserId);
                    }
                    else
                    {
                        invoiceTypeList = InvoiceTypeListsSOO();
                    }
                }
            }

            ddlInvoiceType.Items.Clear();
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
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to fetch SOO Customer data from DB");
            LogException(ex, "LoadInvoiceTypeDDLSOO");
        }
    }
    #endregion

    //ER 11609 Start
    #region LoadInvoiceTypeDDLSOOAccsReport
    /// <summary>
    /// Loads Invoice Type dropdown list
    /// </summary>
    private void LoadInvoiceTypeDDLSOOAccReport()
    {
        try
        {
            Authenticate objAuth = new Authenticate();
            Users objUser = objAuth.AuthenticateUser(connectionString, GetUserName());

            List<InvoiceType> invoiceTypeList = new List<InvoiceType>();

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator))
                    {
                        invoiceTypeList = SOOAcctsListCustomerAdmin(objUser.UserId);
                    }
                    else
                    {
                        invoiceTypeList = SOOAcctsList();
                    }
                }
            }

            ddlInvoiceType.Items.Clear();
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
            Controls_ErrorSuccessNotifier.AddErrorMessage("Unable to fetch SOO Customer data from DB");
            LogException(ex, "LoadInvoiceTypeDDLSOOAccReport");
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
            _billing = new BillingEngine(connectionString);
            invoiceTypeList = _billing.InvoiceBL.GetInvoiceTypes();
        }
        catch (Exception ex)
        {
            LogException(ex, "InvoiceTypeLists");
        }

        return invoiceTypeList;
    }
    #endregion

    //cbe_9179
    #region InvoiceTypeListsSOO
    /// <summary>
    /// Gets all the Invoice Types
    /// </summary>
    /// <returns></returns>
    private List<InvoiceType> InvoiceTypeListsSOO()
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
        try
        {
            _billing = new BillingEngine(connectionString);
            invoiceTypeList = _billing.InvoiceBL.GetInvoiceTypesSOO();
        }
        catch (Exception ex)
        {
            LogException(ex, "InvoiceTypeLists");
        }

        return invoiceTypeList;
    }
    #endregion

    //cbe_11609
    #region SOOAcctsList
    /// <summary>
    /// Gets all the Invoice Types
    /// </summary>
    /// <returns></returns>
    private List<InvoiceType> SOOAcctsList()
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
        try
        {
            _billing = new BillingEngine(connectionString);
            invoiceTypeList = _billing.InvoiceBL.GetAccListsSOO();
        }
        catch (Exception ex)
        {
            LogException(ex, "SOOAcctsList");
        }

        return invoiceTypeList;
    }
    #endregion

    #region InvoiceTypeListsCustomerAdmin
    /// <summary>
    /// Gets all the Invoice Types the customer administrator is associated with
    /// </summary>
    /// <returns></returns>
    private List<InvoiceType> InvoiceTypeListsCustomerAdmin(string userId)
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
        try
        {
            _billing = new BillingEngine(connectionString);
            invoiceTypeList = _billing.InvoiceBL.GetInvoiceTypesCustomerAdmin(userId);
        }
        catch (Exception ex)
        {
            LogException(ex, "InvoiceTypeListsCustomerAdmin");
        }

        return invoiceTypeList;
    }
    #endregion

    #region InvoiceTypeListsSOOCustomerAdmin
    /// <summary>
    /// Gets all the Invoice Types the customer administrator is associated with
    /// </summary>
    /// <returns></returns>
    private List<InvoiceType> InvoiceTypeListsSOOCustomerAdmin(string userId)
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
        try
        {
            _billing = new BillingEngine(connectionString);
            invoiceTypeList = _billing.InvoiceBL.GetInvoiceTypesSOOCustomerAdmin(userId);
        }
        catch (Exception ex)
        {
            LogException(ex, "InvoiceTypeListsSOOCustomerAdmin");
        }

        return invoiceTypeList;
    }
    #endregion

    //cbe_11609
    #region SOOAcctsListCustomerAdmin
    /// <summary>
    /// Gets all the Invoice Types
    /// </summary>
    /// <returns></returns>
    private List<InvoiceType> SOOAcctsListCustomerAdmin(string userId)
    {
        List<InvoiceType> invoiceTypeList = new List<InvoiceType>();
        try
        {
            _billing = new BillingEngine(connectionString);
            invoiceTypeList = _billing.InvoiceBL.GetAccListsSOOCustomerAdmin(userId);
        }
        catch (Exception ex)
        {
            LogException(ex, "SOOAcctsList");
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
            ddlInvoiceType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(DEFAULT_INVOICETYPE_TEXT, DEFAULT_INVOICE_VALUE));

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
        if (ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE)
        {

            List<Invoice> invoiceList = new List<Invoice>();
            _billing = new BillingEngine(connectionString);
            int InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());

            if(InvoiceType == 0 && ddlDataType.SelectedValue.Equals("SOOAccsReport"))
                invoiceList = _billing.InvoiceBL.FetchInvoicesByType_All(InvoiceType);
            else
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

            ddlInvoiceNumber.Items.Clear();
            ddlInvoiceNumber.DataSource = null;

            if (!chkInvoiceNumber.Checked)
            {
                ddlInvoiceNumber.DataSource = invoiceList.Take(6);
            }
            else
            {
                ddlInvoiceNumber.DataSource = invoiceList;
            }

            ddlInvoiceNumber.DataBind();
            ddlInvoiceNumber.Items.Insert(0, new System.Web.UI.WebControls.ListItem(DEFAULT_INVOICENUMBER_TEXT, DEFAULT_INVOICE_VALUE));

            ddlInvoiceNumber.Enabled = true;
        }
        else if (ddlInvoiceType.SelectedValue == "0" && ddlDataType.SelectedValue.Equals("SOOAccsReport"))
        {
            List<Invoice> invoiceList = new List<Invoice>();
            _billing = new BillingEngine(connectionString);
            int InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
        }
        else
        {
            ddlInvoiceNumber.Enabled = false;
            ddlInvoiceNumber.Items.Clear();
        }
    }
    #endregion

    //ER 6775 End

    /// <summary>
    /// Get logged in user name
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
                            dvReports.Visible = true;
                            dvReportTable.Visible = true;
                            divUnextractedData.Visible = true;
                            dvAccessDenied.Visible = false;
                            lbUserName.Text = strUserNameRole;
                            return true;
                        }
                    }
                    else
                    {
                        dvReports.Visible = false;
                        dvReportTable.Visible = false;
                        divUnextractedData.Visible = false;
                        dvAccessDenied.Visible = true;
                        lblAccessDenied.Text = "Your status is inactive. Please contact the application administrator for activating the access.";
                        lbUserName.Text = strUserNameRole;
                        return false;
                    }
                }
                else
                {
                    dvReports.Visible = false;
                    dvReportTable.Visible = false;
                    divUnextractedData.Visible = false;
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

    private void LogException(Exception exception, string methodName)
    {
        AppExceptionBL objAppExceptionBL = new AppExceptionBL(connectionString);
        AppException objAppException = new AppException();
        objAppException.ErrorMessage = exception.Message;
        objAppException.LoggedInUser = GetUserName();
        objAppException.MethodName = methodName;
        objAppException.StackTrace = exception.StackTrace;
        objAppExceptionBL.InsertApplicationException(objAppException);
    }

    protected void grdUnextractedData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<CUCDMData> lstCUCDMData = new List<CUCDMData>();
        lstCUCDMData = GetUnextractedCUCDMData();

        grdUnextractedData.DataSource = lstCUCDMData;
        grdUnextractedData.PageIndex = e.NewPageIndex;
        grdUnextractedData.DataBind();
    }

    private List<CUCDMData> GetUnextractedCUCDMData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        List<CUCDMData> lstCUCDMData = new List<CUCDMData>();

        lstCUCDMData = objCUCDMDataBL.GetUnextractedCUCDMData();
        return lstCUCDMData;
    }

    protected void btnUnextractedData_Click(object sender, EventArgs e)
    {
        try
        {
            List<CUCDMData> lstCUCDMData = new List<CUCDMData>();
            lstCUCDMData = GetUnextractedCUCDMData();
            if (lstCUCDMData.Count > 0)
            {
                divUnExtractedGrid.Visible = true;
                grdUnextractedData.Visible = true;
                grdUnextractedData.DataSource = lstCUCDMData;
                grdUnextractedData.DataBind();
            }
            else
            {
                divUnExtractedGrid.Visible = false;
                grdUnextractedData.Visible = false;
                lblUnextractedData.Text = "No Unextracted Data Found";
                lblUnextractedData.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while Binding Unextracted Data");
            LogException(ex, "btnUnextractedData_Click");
        }
    }

    protected void grdEnhancedData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEnhancedData.PageIndex = e.NewPageIndex;
        BindEnhancedData();
    }

    protected void grdUnEnhanceddata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUnEnhanceddata.PageIndex = e.NewPageIndex;
        BindUnEnhancedData();
    }

    protected void grdRawdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRawdata.PageIndex = e.NewPageIndex;
        BindRawData();
    }

    protected void grdSOOReportData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEnhancedData.PageIndex = e.NewPageIndex;
        BindSOOReportData();
    }

    //cbe_11609 grdSOOAccountdata_PageIndexChanging
    protected void grdSOOAccountdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEnhancedData.PageIndex = e.NewPageIndex;
        BindSOOAccountData();
    }
    protected void grdEDIReportData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEDIReportData.PageIndex = e.NewPageIndex;
        BindEDIReportData();
    }

    private void FetchSearchResult()
    {
        if (ddlDataType.SelectedValue.Equals("Enhanced"))
        {
            lbldata.Visible = false;
            divUnEnhanceddata.Visible = false;
            divEnhanceddata.Visible = true;
            divRawData.Visible = false;
            divSOOARReportData.Visible = false;
            divSOOAccListData.Visible = false;
            divEDIReportData.Visible = false;
            BindEnhancedData();
        }
        else if (ddlDataType.SelectedValue.Equals("UnEnhanced"))
        {
            lbldata.Visible = false;
            divUnEnhanceddata.Visible = true;
            divEnhanceddata.Visible = false;
            divRawData.Visible = false;
            divSOOARReportData.Visible = false;
            divSOOAccListData.Visible = false;
            divEDIReportData.Visible = false;
            BindUnEnhancedData();
        }
        else if (ddlDataType.SelectedValue.Equals("RawData"))
        {
            lbldata.Visible = false;
            divUnEnhanceddata.Visible = false;
            divEnhanceddata.Visible = false;
            divRawData.Visible = true;
            divSOOARReportData.Visible = false;
            divSOOAccListData.Visible = false;
            divEDIReportData.Visible = false;
            BindRawData();
        }
        else if (ddlDataType.SelectedValue.Equals("SOOARReport"))
        {
            lbldata.Visible = false;
            divUnEnhanceddata.Visible = false;
            divEnhanceddata.Visible = false;
            divRawData.Visible = false;
            divSOOARReportData.Visible = true;
            divSOOAccListData.Visible = false;
            divEDIReportData.Visible = false;

            BindSOOReportData();
        }
        else if (ddlDataType.SelectedValue.Equals("SOOAccsReport"))
        {
            lbldata.Visible = false;
            divUnEnhanceddata.Visible = false;
            divEnhanceddata.Visible = false;
            divRawData.Visible = false;
            divSOOARReportData.Visible = false;
            divSOOAccListData.Visible = true;
            BindSOOAccountData();
        }
        else if (ddlDataType.SelectedValue.Equals("EDI"))
        {
            lbldata.Visible = false;
            divUnEnhanceddata.Visible = false;
            divEnhanceddata.Visible = false;
            divRawData.Visible = false;
            divSOOARReportData.Visible = false;
            divSOOAccListData.Visible = false;
            divEDIReportData.Visible = true;
            BindEDIReportData();
        }
    }

    private void BindEDIReportData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        List<EDIReportData> EDIReportData = new List<EDIReportData>();
        try
        {
            EDIReportData = objCUCDMDataBL.GetEDIReportData();

            if (EDIReportData.Count > 0)
            {
                divEDIReportData.Visible = true;
                grdEDIReportData.DataSource = EDIReportData;
                grdEDIReportData.DataBind();
            }
            else
            {
                divEnhanceddata.Visible = false;
                lbldata.Visible = true;
                lbldata.Text = "No EDI Report Data Found";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while loading EDI Report Data");
            LogException(ex, "BindEDIReportData");
        }
    }

    //ER# 6775 modified logic
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlInvoiceType.SelectedValue == DEFAULT_INVOICE_VALUE)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Please select Customer");
            return;
        }
        if (ddlDataType.SelectedValue.Equals("Enhanced"))
        {
            if (ddlInvoiceNumber.SelectedValue == DEFAULT_INVOICE_VALUE)
            {
                Controls_ErrorSuccessNotifier.AddErrorMessage("Please select Invoice Number");
                return;
            }
            if (ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE && ddlInvoiceNumber.SelectedValue != DEFAULT_INVOICE_VALUE)
            {
                FetchSearchResult();
            }
        }
        else if (ddlDataType.SelectedValue.Equals("UnEnhanced"))
        {
            if (ddlInvoiceNumber.SelectedValue == DEFAULT_INVOICE_VALUE)
            {
                Controls_ErrorSuccessNotifier.AddErrorMessage("Please select Invoice Number");
                return;
            }
            if (ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE && ddlInvoiceNumber.SelectedValue != DEFAULT_INVOICE_VALUE)
            {
                FetchSearchResult();
            }
        }
        else if (ddlDataType.SelectedValue.Equals("RawData"))
        {
            if ((ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE) && (!string.IsNullOrEmpty(txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim())))
            {
                DateTime dtStartDate = DateTime.Parse(txtStartDate.Text.Trim());
                DateTime dtEndDate = DateTime.Parse(txtEndDate.Text.Trim());

                if (dtStartDate > dtEndDate)
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("Start date cannot be greater than the End date");
                    divEnhanceddata.Visible = false;
                    divUnEnhanceddata.Visible = false;
                    divRawData.Visible = false;
                }
                else
                {
                    FetchSearchResult();
                }
            }
            else if ((ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE) && (!string.IsNullOrEmpty(txtStartDate.Text.Trim()) || !string.IsNullOrEmpty(txtEndDate.Text.Trim())))
            {
                FetchSearchResult();
            }
            else
            {
                FetchSearchResult();
                //Controls_ErrorSuccessNotifier.AddErrorMessage("Please specify a desired data range or Start/End date");
                //divEnhanceddata.Visible = false;
                //divUnEnhanceddata.Visible = false;
                //divRawData.Visible = false;
            }
        }
        else if (ddlDataType.SelectedValue.Equals("SOOARReport"))//cbe_9941
        {
            if ((ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE) && (!string.IsNullOrEmpty(txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim())))
            {
                DateTime dtStartDate = DateTime.Parse(txtStartDate.Text.Trim());
                DateTime dtEndDate = DateTime.Parse(txtEndDate.Text.Trim());

                if (dtStartDate.Month != dtEndDate.Month)
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("Month should be same for both Start and End dates");
                    divEnhanceddata.Visible = false;
                    divSOOARReportData.Visible = false;
                    divUnEnhanceddata.Visible = false;
                    divRawData.Visible = false;
                }
                else
                {
                    FetchSearchResult();
                }
            }
        }
        else if (ddlDataType.SelectedValue.Equals("SOOAccsReport"))//cbe_11609
        {
            if (ddlInvoiceNumber.SelectedValue == DEFAULT_INVOICE_VALUE)
            {
                Controls_ErrorSuccessNotifier.AddErrorMessage("Please select Invoice Number");
                return;
            }
            if (ddlInvoiceType.SelectedValue != DEFAULT_INVOICE_VALUE && ddlInvoiceNumber.SelectedValue != DEFAULT_INVOICE_VALUE)
            {
                FetchSearchResult();
            }
        }
        else if (ddlDataType.SelectedValue.Equals("EDI"))
        {
            divEnhanceddata.Visible = false;
            divSOOARReportData.Visible = false;
            divUnEnhanceddata.Visible = false;
            divRawData.Visible = false;
            divSOOARReportData.Visible = false;

            FetchSearchResult();
        }
    }

    private void BindEnhancedData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        List<EnhancedData> lstEnhancedData = new List<EnhancedData>();
        try
        {
            lstEnhancedData = objCUCDMDataBL.GetEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
            if (lstEnhancedData.Count > 0)
            {
                divEnhanceddata.Visible = true;
                grdEnhancedData.DataSource = lstEnhancedData;
                grdEnhancedData.DataBind();
            }
            else
            {
                divEnhanceddata.Visible = false;
                lbldata.Visible = true;
                lbldata.Text = "No MBM Enhanced Data with Charges Found";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while loading MBM Enhanced Data with Charges");
            LogException(ex, "BindEnhancedData");
        }
    }

    private void BindUnEnhancedData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        List<UnEnhancedData> lstUnEnhancedData = new List<UnEnhancedData>();
        try
        {
            lstUnEnhancedData = objCUCDMDataBL.GetUnEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
            if (lstUnEnhancedData.Count > 0)
            {
                divUnEnhanceddata.Visible = true;
                grdUnEnhanceddata.DataSource = lstUnEnhancedData;
                grdUnEnhanceddata.DataBind();
            }
            else
            {
                divUnEnhanceddata.Visible = false;
                lbldata.Visible = true;
                lbldata.Text = "No MBM Enhanced Data Found";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while loading MBM Enhanced Data");
            LogException(ex, "BindUnEnhancedData");
        }
    }

    private void BindRawData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        List<EHCSRawData> lstEHCSRawData = new List<EHCSRawData>();
        try
        {
            lstEHCSRawData = objCUCDMDataBL.GetEHCSRawData(ddlInvoiceType.SelectedItem.Text, txtStartDate.Text, txtEndDate.Text);
            if (lstEHCSRawData.Count > 0)
            {
                divRawData.Visible = true;
                grdRawdata.DataSource = lstEHCSRawData;
                grdRawdata.DataBind();
            }
            else
            {
                divRawData.Visible = false;
                lbldata.Visible = true;
                lbldata.Text = "No MBM Input Data Found";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while loading MBM Input Data");
            LogException(ex, "BindEnhancedData");
        }
    }

    //cbe_9941
    private void BindSOOReportData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        List<SOOReportData> SOOReportData = new List<SOOReportData>();
        try
        {
            SOOReportData = objCUCDMDataBL.GetSOOReportData(ddlInvoiceType.SelectedItem.Text, txtStartDate.Text, txtEndDate.Text);
            if (SOOReportData.Count > 0)
            {
                divSOOARReportData.Visible = true;
                grdSOOReportData.DataSource = SOOReportData;
                grdSOOReportData.DataBind();
            }
            else
            {
                divEnhanceddata.Visible = false;
                lbldata.Visible = true;
                lbldata.Text = "No SOO Accural Revenue Report Data Found";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while loading SOO Accural Revenue Report Data");
            LogException(ex, "BindSOOReportData");
        }
    }

    //cbe_11609
    private void BindSOOAccountData()
    {
        CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
        DataSet ds = new DataSet();
        try
        {
            ds = objCUCDMDataBL.GetSOOAccountData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows)
            {
                divSOOAccListData.Visible = true;
                grdSOOAccountdata.DataSource = ds.Tables[3];
                grdSOOAccountdata.DataBind();
            }
            else
            {
                divEnhanceddata.Visible = false;
                lbldata.Visible = true;
                lbldata.Text = "No SOO Account Data Found";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while loading SOO Account Data");
            LogException(ex, "BindSOOAccountData");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    private void ExportGridDataToExcel(string strFileName)
    {
        try
        {
            CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);

            GridView grdExport = new GridView();
            grdExport.AllowPaging = false;

            switch (strFileName)
            {
                case "MBMEnhancedDataWithCharges":
                    List<EnhancedData> lstEnhancedData = new List<EnhancedData>();
                    lstEnhancedData = objCUCDMDataBL.GetEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    grdExport.DataSource = lstEnhancedData;
                    break;

                case "MBMEnhancedData":
                    List<UnEnhancedData> lstUnEnhancedData = new List<UnEnhancedData>();
                    lstUnEnhancedData = objCUCDMDataBL.GetUnEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    grdExport.DataSource = lstUnEnhancedData;
                    break;

                case "MBMUnextractedData":
                    List<CUCDMData> lstUnextractedData = new List<CUCDMData>();
                    lstUnextractedData = GetUnextractedCUCDMData();
                    grdExport.DataSource = lstUnextractedData;
                    break;

                case "MBMInputData":
                    List<EHCSRawData> lstRawData = new List<EHCSRawData>();
                    lstRawData = objCUCDMDataBL.GetEHCSRawData(ddlInvoiceType.SelectedItem.Text, txtStartDate.Text, txtEndDate.Text);
                    grdExport.DataSource = lstRawData;
                    break;

                case "MBMSOOAccountsData":
                    DataSet ds = new DataSet();
                    ds = objCUCDMDataBL.GetSOOAccountData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    grdExport.DataSource = ds;
                    break;

                case "MBMEDIReportData":
                    List<EDIReportData> ediReportData = new List<EDIReportData>();
                    ediReportData = objCUCDMDataBL.GetEDIReportData();
                    grdExport.DataSource = ediReportData;
                    break;
            }

            grdExport.DataBind();

            //Apply style to Individual Cells
            for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
            {
                grdExport.HeaderRow.Cells[j].Style.Add("background-color", "#E48D0F");

                if (strFileName.Equals("MBMUnextractedData"))
                {
                    if (grdExport.HeaderRow.Cells[j].Text.Equals("LegalEntity"))
                    {
                        grdExport.HeaderRow.Cells[j].Text = "ProvisioningIdentifier";
                    }
                }
            }

            for (int i = 0; i < grdExport.Rows.Count; i++)
            {
                GridViewRow row = grdExport.Rows[i];

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

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xls");
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);

            grdExport.RenderControl(htmlTextWriter);
            System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.SuppressContent = true;
        }
        catch (Exception ex)
        {
            //Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while exporting the data");
            LogException(ex, "ExportToExcel");
        }
    }

    private void ExportGridDataToPDF(string strFileName)
    {
        try
        {
            CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);
            List<EnhancedData> lstEnhancedData = new List<EnhancedData>();
            List<UnEnhancedData> lstUnEnhancedData = new List<UnEnhancedData>();
            List<CUCDMData> lstUnextractedData = new List<CUCDMData>();

            GridView grdExport = new GridView();
            grdExport.AllowPaging = false;

            switch (strFileName)
            {
                case "MBMEnhancedData":
                    lstEnhancedData = objCUCDMDataBL.GetEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    grdExport.DataSource = lstEnhancedData;
                    break;

                case "MBMUnEnhancedData":
                    lstUnEnhancedData = objCUCDMDataBL.GetUnEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    grdExport.DataSource = lstUnEnhancedData;
                    break;

                case "MBMUnextractedData":
                    lstUnextractedData = GetUnextractedCUCDMData();
                    grdExport.DataSource = lstUnextractedData;
                    break;
            }

            grdExport.DataBind();

            //Apply style to Individual Cells
            for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
            {
                grdExport.HeaderRow.Cells[j].Style.Add("background-color", "#E48D0F");
            }

            for (int i = 0; i < grdExport.Rows.Count; i++)
            {
                GridViewRow row = grdExport.Rows[i];

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

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".pdf");
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.ContentType = "application/pdf";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);

            grdExport.RenderControl(htmlTextWriter);

            StringReader sr = new StringReader(stringWriter.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            //Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while exporting the data");
            LogException(ex, "ExportToExcel");
        }
    }

    protected void btnEnhancedDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMEnhancedDataWithCharges";
        //ExportGridDataToExcel(strFileName);
        ExportExcel(strFileName);

    }

    protected void btnEnhancedDataExportToPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMEnhancedData";
        ExportGridDataToPDF(strFileName);
    }

    protected void btnUnEnhancedDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMEnhancedData";
        //ExportGridDataToExcel(strFileName);
        ExportExcel(strFileName);
    }

    protected void btnUnEnhancedDataExportToPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMUnEnhancedData";
        ExportGridDataToPDF(strFileName);
    }

    protected void btnUnextractedDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMUnextractedData";
        //ExportGridDataToExcel(strFileName);
        ExportExcel(strFileName);
    }

    protected void btnUnextractedDataExportToPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMUnextractedData";
        ExportGridDataToPDF(strFileName);
    }

    protected void btnRawDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMInputData";
        //ExportGridDataToExcel(strFileName);
        ExportExcel(strFileName);
    }

    protected void btnSOOReportDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMSOOReportData";
        //ExportGridDataToExcel(strFileName);
        ExportExcel(strFileName);
    }

    //cbe_11609 btnSOOAccountDataExportToExcel_Click
    protected void btnSOOAccountDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMSOOAccountsData";
        //ExportGridDataToExcel(strFileName);
        ExportExcel(strFileName);
    }

    #region Convert DataSource to DatsSet

    public DataTable GetEnhancedDataTable(List<EnhancedData> lstEnhancedData)
    {
        string tableName1 = "MBM EnhancedData With Charges";
        DataTable dt1 = new DataTable(tableName1);

        try
        {
            dt1.Columns.Add("Customer", typeof(string));
            dt1.Columns.Add("Invoice Number", typeof(string));
            dt1.Columns.Add("Snapshot Id", typeof(string));
            dt1.Columns.Add("Sub-Identifier", typeof(string));
            dt1.Columns.Add("First Name", typeof(string));
            dt1.Columns.Add("Last Name", typeof(string));
            dt1.Columns.Add("Primary Unique Identifier", typeof(string));
            dt1.Columns.Add("Secondary Unique Identifier", typeof(string));
            dt1.Columns.Add("Service Profile", typeof(string));
            dt1.Columns.Add("Legal Entity", typeof(string));
            dt1.Columns.Add("Charge Code", typeof(string));
            dt1.Columns.Add("Charge", typeof(string));
            dt1.Columns.Add("Start Date", typeof(string));
            dt1.Columns.Add("End Date", typeof(string));
            dt1.Columns.Add("Feature Code", typeof(string));
            dt1.Columns.Add("e164_mask", typeof(string));
            dt1.Columns.Add("Active Charge", typeof(string));
            dt1.Columns.Add("Effective Billing Date", typeof(string));

            foreach (var items in lstEnhancedData)
            {
                var row = dt1.NewRow();
                row["Customer"] = items.Customer;
                row["Invoice Number"] = items.InvoiceNumber;
                row["Snapshot Id"] = items.SnapshotId;
                row["Sub-Identifier"] = items.SubIdentifier;
                row["First Name"] = items.FirstName;
                row["Last Name"] = items.LastName;
                row["Primary Unique Identifier"] = items.PrimaryUniqueIdentifier;
                row["Secondary Unique Identifier"] = items.SecondaryUniqueIdentifier;
                row["Service Profile"] = items.ServiceProfile;
                row["Legal Entity"] = items.LegalEntity;
                row["Charge Code"] = items.ChargeCode;
                row["Charge"] = items.Charge;
                row["Start Date"] = items.StartDate;
                row["End Date"] = items.EndDate;
                row["Feature Code"] = items.FeatureCode;
                row["e164_mask"] = items.e164_mask;
                row["Active Charge"] = items.ActiveCharge;
                row["Effective Billing Date"] = items.EffectiveBillingDate;

                dt1.Rows.Add(row);
            }
            return dt1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUnEnhancedDataTable(List<UnEnhancedData> lstUnEnhancedData)
    {
        string tableName1 = "MBM UnEnhancedData With Charges";
        DataTable dt1 = new DataTable(tableName1);

        try
        {
            dt1.Columns.Add("Customer", typeof(string));
            dt1.Columns.Add("Invoice Number", typeof(string));
            dt1.Columns.Add("Snapshot Id", typeof(string));
            dt1.Columns.Add("Sub-Identifier", typeof(string));
            dt1.Columns.Add("First Name", typeof(string));
            dt1.Columns.Add("Last Name", typeof(string));
            dt1.Columns.Add("Primary Unique Identifier", typeof(string));
            dt1.Columns.Add("Secondary Unique Identifier", typeof(string));
            dt1.Columns.Add("Service Profile", typeof(string));
            dt1.Columns.Add("Legal Entity", typeof(string));
            dt1.Columns.Add("Service Start Date", typeof(string));
            dt1.Columns.Add("Service End Date", typeof(string));
            dt1.Columns.Add("State", typeof(string));
            dt1.Columns.Add("Postal Code", typeof(string));
            dt1.Columns.Add("Country", typeof(string));
            dt1.Columns.Add("e164_mask", typeof(string));
            dt1.Columns.Add("Active Charge", typeof(string));
            dt1.Columns.Add("Effective Billing Date", typeof(string));

            foreach (var items in lstUnEnhancedData)
            {
                var row = dt1.NewRow();
                row["Customer"] = items.Customer;
                row["Invoice Number"] = items.InvoiceNumber;
                row["Snapshot Id"] = items.SnapshotId;
                row["Sub-Identifier"] = items.SubIdentifier;
                row["First Name"] = items.FirstName;
                row["Last Name"] = items.LastName;
                row["Primary Unique Identifier"] = items.PrimaryUniqueIdentifier;
                row["Secondary Unique Identifier"] = items.SecondaryUniqueIdentifier;
                row["Service Profile"] = items.ServiceProfile;
                row["Legal Entity"] = items.LegalEntity;
                row["Service Start Date"] = items.ServiceStartDate;
                row["Service End Date"] = items.ServiceEndDate;
                row["State"] = items.State;
                row["Postal Code"] = items.PostalCode;
                row["Country"] = items.Country;
                row["e164_mask"] = items.e164_mask;
                row["Active Charge"] = items.ActiveCharge;
                row["Effective Billing Date"] = items.EffectiveBillingDate;

                dt1.Rows.Add(row);
            }
            return dt1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetUnextractedCUCDMDataTable(List<CUCDMData> lstUnextractedData)
    {
        string tableName1 = "MBM Unextracted CUCDM Data";
        DataTable dt1 = new DataTable(tableName1);

        try
        {
            dt1.Columns.Add("Id", typeof(string));
            dt1.Columns.Add("Snapshot Id", typeof(string));
            dt1.Columns.Add("Sub-Identifier", typeof(string));
            dt1.Columns.Add("First Name", typeof(string));
            dt1.Columns.Add("Last Name", typeof(string));
            dt1.Columns.Add("Asset Search", typeof(string));
            dt1.Columns.Add("Service Profile ID", typeof(string));
            dt1.Columns.Add("Legal Entity", typeof(string));
            dt1.Columns.Add("Service Start Date", typeof(string));
            dt1.Columns.Add("State", typeof(string));
            dt1.Columns.Add("Postal Code", typeof(string));
            dt1.Columns.Add("Service End Date", typeof(string));
            dt1.Columns.Add("Country", typeof(string));
            dt1.Columns.Add("Directory Number", typeof(string));
            dt1.Columns.Add("MacAddress", typeof(string));

            foreach (var items in lstUnextractedData)
            {
                var row = dt1.NewRow();
                row["Id"] = items.Id;
                row["Snapshot Id"] = items.SnapshotId;
                row["Sub-Identifier"] = items.SubIdentifier;
                row["First Name"] = items.FirstName;
                row["Last Name"] = items.LastName;
                row["Asset Search"] = items.AssetSearch;
                row["Service Profile ID"] = items.ServiceProfileID;
                row["Legal Entity"] = items.LegalEntity;
                row["Service Start Date"] = items.ServiceStartDate;
                row["State"] = items.State;
                row["Postal Code"] = items.PostalCode;
                row["Service End Date"] = items.ServiceStartDate;
                row["Country"] = items.Country;
                row["Directory Number"] = items.DirectoryNumber;
                row["MacAddress"] = items.MacAddress;

                dt1.Rows.Add(row);
            }
            return dt1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetEHCSRawDataTable(List<EHCSRawData> lstRawData)
    {
        string tableName1 = "MBM EHCS Raw Data";
        DataTable dt1 = new DataTable(tableName1);

        try
        {
            dt1.Columns.Add("Snapshot Id", typeof(string));
            dt1.Columns.Add("Sub-Identifier", typeof(string));
            dt1.Columns.Add("First Name", typeof(string));
            dt1.Columns.Add("Last Name", typeof(string));
            dt1.Columns.Add("Primary Unique Identifier", typeof(string));
            dt1.Columns.Add("Service Profile ID", typeof(string));
            dt1.Columns.Add("Service Start Date", typeof(string));
            dt1.Columns.Add("Service End Date", typeof(string));
            dt1.Columns.Add("Legal Entity", typeof(string));
            dt1.Columns.Add("State", typeof(string));
            dt1.Columns.Add("Postal Code", typeof(string));
            dt1.Columns.Add("Country", typeof(string));
            dt1.Columns.Add("Directory Number", typeof(string));
            dt1.Columns.Add("Service Profile Uid", typeof(string));
            dt1.Columns.Add("e164_mask", typeof(string));
            dt1.Columns.Add("Active Charge", typeof(string));
            dt1.Columns.Add("Effective Billing Date", typeof(string));

            foreach (var items in lstRawData)
            {
                var row = dt1.NewRow();
                row["Snapshot Id"] = items.SnapshotId;
                row["Sub-Identifier"] = items.SubIdentifier;
                row["First Name"] = items.FirstName;
                row["Last Name"] = items.LastName;
                row["Primary Unique Identifier"] = items.PrimaryUniqueIdentifier;
                row["Service Profile ID"] = items.ServiceProfileId;
                row["Service Start Date"] = items.ServiceStartDate;
                row["Service End Date"] = items.ServiceEndDate;
                row["Legal Entity"] = items.LegalEntity;
                row["State"] = items.State;
                row["Postal Code"] = items.PostalCode;
                row["Country"] = items.Country;
                row["Directory Number"] = items.DirectoryNumber;
                row["Service Profile Uid"] = items.ServiceProfileUid;
                row["e164_mask"] = items.e164_mask;
                row["Active Charge"] = items.ActiveCharge;
                row["Effective Billing Date"] = items.EffectiveBillingDate;

                dt1.Rows.Add(row);
            }
            return dt1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetSOOReportDataTable(List<SOOReportData> SOOReportData)
    {
        string tableName1 = "MBM SOO Report Data";
        DataTable dt1 = new DataTable(tableName1);

        try
        {
            dt1.Columns.Add("CRM_ACCOUNT", typeof(string));
            dt1.Columns.Add("TN", typeof(string));
            dt1.Columns.Add("PROFILE_TYPE", typeof(string));
            dt1.Columns.Add("AMOUNT", typeof(decimal));
            dt1.Columns.Add("START_DTT", typeof(string));
            dt1.Columns.Add("END_DTT", typeof(string));
            dt1.Columns.Add("CUSTOMER_DEPARTMENT", typeof(string));
            dt1.Columns.Add("CUSTOMER_COST_CENTER", typeof(string));
            dt1.Columns.Add("CUSTOMER_ALI_CODE", typeof(string));
            dt1.Columns.Add("SUPERVISOR_NAME", typeof(string));
            dt1.Columns.Add("FIRST_NAME", typeof(string));
            dt1.Columns.Add("LAST_NAME", typeof(string));
            dt1.Columns.Add("EMAIL", typeof(string));
            dt1.Columns.Add("QUANTITY", typeof(string));
            dt1.Columns.Add("UNIT_PRICE", typeof(decimal));
            dt1.Columns.Add("DESCRIPTION", typeof(string));

            foreach (var items in SOOReportData)
            {
                var row = dt1.NewRow();
                row["CRM_ACCOUNT"] = items.CRM_ACCOUNT;
                row["TN"] = items.TN;
                row["PROFILE_TYPE"] = items.PROFILE_TYPE;
                row["AMOUNT"] = items.AMOUNT;
                row["START_DTT"] = items.START_DTT;
                row["END_DTT"] = items.END_DTT;
                row["CUSTOMER_DEPARTMENT"] = items.CUSTOMER_DEPARTMENT;
                row["CUSTOMER_COST_CENTER"] = items.CUSTOMER_COST_CENTER;
                row["CUSTOMER_ALI_CODE"] = items.CUSTOMER_ALI_CODE;
                row["SUPERVISOR_NAME"] = items.SUPERVISOR_NAME;
                row["FIRST_NAME"] = items.FIRST_NAME;
                row["LAST_NAME"] = items.LAST_NAME;
                row["EMAIL"] = items.EMAIL;
                row["QUANTITY"] = items.QUANTITY;
                row["UNIT_PRICE"] = items.UNIT_PRICE;
                row["DESCRIPTION"] = items.DESCRIPTION;

                dt1.Rows.Add(row);
            }
            return dt1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion Convert DataSource to DatsSet

    public void ExportExcel(string filename)
    {
        try
        {
            CUCDMDataBL objCUCDMDataBL = new CUCDMDataBL(connectionString);

            GridView grdExport = new GridView();
            grdExport.AllowPaging = false;

            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();

            switch (filename)
            {
                case "MBMEnhancedDataWithCharges":
                    List<EnhancedData> lstEnhancedData = new List<EnhancedData>();
                    lstEnhancedData = objCUCDMDataBL.GetEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    dt1 = GetEnhancedDataTable(lstEnhancedData);
                    ds.Tables.Add(dt1);
                    break;

                case "MBMEnhancedData":
                    List<UnEnhancedData> lstUnEnhancedData = new List<UnEnhancedData>();
                    lstUnEnhancedData = objCUCDMDataBL.GetUnEnhancedData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    dt1 = GetUnEnhancedDataTable(lstUnEnhancedData);
                    ds.Tables.Add(dt1);
                    break;

                case "MBMUnextractedData":
                    List<CUCDMData> lstUnextractedData = new List<CUCDMData>();
                    lstUnextractedData = GetUnextractedCUCDMData();
                    dt1 = GetUnextractedCUCDMDataTable(lstUnextractedData);
                    ds.Tables.Add(dt1);
                    break;

                case "MBMInputData":
                    List<EHCSRawData> lstRawData = new List<EHCSRawData>();
                    lstRawData = objCUCDMDataBL.GetEHCSRawData(ddlInvoiceType.SelectedItem.Text, txtStartDate.Text, txtEndDate.Text);
                    dt1 = GetEHCSRawDataTable(lstRawData);
                    ds.Tables.Add(dt1);
                    break;

                case "MBMSOOReportData":
                    List<SOOReportData> SOOReportData = new List<SOOReportData>();
                    SOOReportData = objCUCDMDataBL.GetSOOReportData(ddlInvoiceType.SelectedItem.Text, txtStartDate.Text, txtEndDate.Text);
                    dt1 = GetSOOReportDataTable(SOOReportData);
                    ds.Tables.Add(dt1);
                    break;

                case "MBMSOOAccountsData":
                    ds = objCUCDMDataBL.GetSOOAccountData(Convert.ToInt32(ddlInvoiceNumber.SelectedValue));
                    grdExport.DataSource = ds;
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
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(xp.GetAsByteArray());
                    Response.End();
                }
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void btnEDIReportDataExportToExcel_Click(object sender, EventArgs e)
    {
        string strFileName = "MBMEDIReportData";
        ExportGridDataToExcel(strFileName);
    }

    //ER 6775 Changes
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDataType.SelectedValue.Equals("RawData"))
        {
            LoadInvoiceTypeDDL();
            ddlInvoiceType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("All", "0"));
            chkInvoiceNumber.Visible = false;
            ddlInvoiceNumber.Visible = false;
            lblCol4Up.Visible = true;
            lblCol4Down.Visible = true;
            lblInvoices.Visible = false;
            lblStartDate.Visible = true;
            lblEndDate.Visible = true;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtStartDate.Visible = true;
            txtEndDate.Visible = true;
        }
        else if (ddlDataType.SelectedValue.Equals("Enhanced"))
        {
            LoadInvoiceTypeDDL();
            ddlInvoiceNumber.Items.Clear();
            chkInvoiceNumber.Visible = true;
            ddlInvoiceNumber.Visible = true;
            lblCol4Up.Visible = false;
            lblCol4Down.Visible = false;
            lblInvoices.Visible = true;
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            txtStartDate.Visible = false;
            txtEndDate.Visible = false;
        }
        else if (ddlDataType.SelectedValue.Equals("UnEnhanced"))
        {
            LoadInvoiceTypeDDL();
            ddlInvoiceNumber.Items.Clear();
            chkInvoiceNumber.Visible = true;
            ddlInvoiceNumber.Visible = true;
            lblCol4Up.Visible = false;
            lblCol4Down.Visible = false;
            lblInvoices.Visible = true;
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            txtStartDate.Visible = false;
            txtEndDate.Visible = false;
        }
        else if (ddlDataType.SelectedValue.Equals("SOOARReport"))
        {
            LoadInvoiceTypeDDLSOO();
            ddlInvoiceType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("All", "0"));
            chkInvoiceNumber.Visible = false;
            ddlInvoiceNumber.Visible = false;
            lblCol4Up.Visible = true;
            lblCol4Down.Visible = true;
            lblInvoices.Visible = false;
            lblStartDate.Visible = true;
            lblEndDate.Visible = true;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtStartDate.Visible = true;
            txtEndDate.Visible = true;
        }
        else if (ddlDataType.SelectedValue.Equals("SOOAccsReport"))
        {
            LoadInvoiceTypeDDLSOOAccReport();
            ddlInvoiceType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("All", "0"));
            chkInvoiceNumber.Visible = true;
            ddlInvoiceNumber.Visible = true;
            lblCol4Up.Visible = false;
            lblCol4Down.Visible = false;
            lblInvoices.Visible = true;
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtStartDate.Visible = false;
            txtEndDate.Visible = false;
        }
        else if (ddlDataType.SelectedValue.Equals("EDI"))
        {
            //LoadInvoiceTypeDDLSOO();
            //ddlInvoiceType.Items.Insert(1, new System.Web.UI.WebControls.ListItem("All", "0"));
            chkInvoiceNumber.Visible = false;
            ddlInvoiceNumber.Visible = false;
            lblCol4Up.Visible = false;
            lblCol4Down.Visible = false;
            lblInvoices.Visible = false;
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtStartDate.Visible = false;
            txtEndDate.Visible = false;
        }
    }

    //ER 6775
    protected void RestrictInvoiceDisplay(object sender, EventArgs e)
    {

        ddlInvoiceType_SelectedIndexChanged(sender, e);
    }
}