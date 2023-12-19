#region NameSpaces

using MBM.Authenticate;
using MBM.BillingEngine;
using MBM.Entities;
using MBM.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.DirectoryServices;
using Renci.SshNet;
using System.Net.Http;
using Newtonsoft.Json;
using Renci.SshNet;

#endregion


public partial class InvoiceType : Page
{
    #region Constants

    public const string DEFAULT_INVOICETYPE_TEXT = "------------------------------------- Select Customer -------------------------------------";
   
    public const string DEFAULT_INVOICE_VALUE = "0";
    public const string DEFAULT_INVOICETYPE_TEXTFIELD = "Prefix";
    public const string DEFAULT_INVOICETYPE_VALUEFIELD = "ID";
    public const string DEFAULT_FILETYPE_TEXTFIELD = "FileType";
    public const string DEFAULT_FILETYPE_VALUEFIELD = "FileTypeId";
    private const string DEFAULT_IMPORT_CURRENCYID = "USD";
    private const string DEFAULT_EXPORT_CURRENCYID = "USD";
    //sero-3511 Start
    private const string GLDepartmentCode_Indirect_Channel = "999100";
    public const string DEFAULT_GL_Department_Code_TEXT = "Select GL Department Code";
    public const string DEFAULT_Indirect_Agent_Region_TEXT = "Select Indirect Agent Region";
    
    //sero-3511 End


    #endregion

    #region PrivateVariables

    private BillingEngine _billing;
    string connectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();

    #endregion

    #region PageLifeCycleEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Authenticate objAuth = new Authenticate();
            bool blIsValid = false;
            string strLoginUser = string.Empty;
            strLoginUser = GetUserName();
            blIsValid = ValidateUser(connectionString, strLoginUser);

            if (blIsValid)
            {
                if (!Page.IsPostBack)
                {
                    if (ddlInvoiceType.Items.Count == 0)
                    {
                        PopulateInvoiceTypes();
                        divAssociates.Visible = false;
                        divInvoiceType.Visible = false;
                        btnEdit.Visible = false;//change
                    }
                    if (ddlImportCurrency.Items.Count == 0 && ddlExportCurrency.Items.Count == 0)
                    {
                        SetCurrencies(DEFAULT_IMPORT_CURRENCYID, DEFAULT_EXPORT_CURRENCYID);
                    }
                }
                else
                {
                    if (ddlInvoiceType.Items.Count > 0)
                    {
                        if (Convert.ToInt32(ddlInvoiceType.SelectedItem.Value) == 0)
                        {
                            divAssociates.Visible = false;
                            divInvoiceType.Visible = false;
                        }
                    }
                }
            }
            if (!Page.IsPostBack)
            {
                //hdnShowAccordin.Value = "0";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while loading the page");
            LogException(ex, "Page_Load");
        }
    }

    #endregion

    #region PrivateMethods
    /// <summary>
    /// Gets the UserName of logined user
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
                throw new Exception("Unable to retrieve current login user details");
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage(ex.Message);
            LogException(ex, "GetUserName");
        }
        return userName;
    }

    /// <summary>
    /// Sets the Currencies to Currency Dropdowns 
    /// </summary>
    /// <param name="defaultImportCurrency"></param>
    /// <param name="defaultExportCurrency"></param>
    private void SetCurrencies(string defaultImportCurrency, string defaultExportCurrency)
    {
        _billing = new BillingEngine(connectionString);
        try
        {
            List<CurrencyConversion> _importCurrencies = _billing.Configurations.Currencies(DateTime.Now);
            ddlImportCurrency.DataTextField = "Description";
            ddlImportCurrency.DataValueField = "ID";
            ddlImportCurrency.DataSource = _importCurrencies;
            ddlImportCurrency.DataBind();

            CurrencyConversion ccI = _importCurrencies.Find(
                  delegate(CurrencyConversion c)
                  {
                      return c.Code == defaultImportCurrency;
                  });

            if (ccI != null)
            {
                ddlImportCurrency.SelectedValue = ccI.ID.ToString();
            }

            List<CurrencyConversion> _exportCurrencies = _billing.Configurations.Currencies(DateTime.Now);
            ddlExportCurrency.DataTextField = "Description";
            ddlExportCurrency.DataValueField = "ID";
            ddlExportCurrency.DataSource = _exportCurrencies;
            ddlExportCurrency.DataBind();

            CurrencyConversion ccE = _exportCurrencies.Find(
                  delegate(CurrencyConversion c)
                  {
                      return c.Code == defaultExportCurrency;
                  });

            if (ccE != null)
            {
                ddlExportCurrency.SelectedValue = ccE.ID.ToString();
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while loading Currencies");
            LogException(ex, "SetCurrencies");
        }
    }
    
    /// <summary>
    /// Validates the logined user
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="strLoginUser"></param>
    /// <returns></returns>
    private bool ValidateUser(string connectionString, string strLoginUser)
    {
        Authenticate objAuth = new Authenticate();
        string strUserNameRole = string.Empty;
        Users objUser = new Users();
        try
        {
            objUser = objAuth.AuthenticateUser(connectionString, strLoginUser);

            LinkButton lbUserName = (LinkButton)Master.FindControl("linkUserName");

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    strUserNameRole = objUser.UserFirstName + " " + objUser.UserLastName + " (" + objUser.UserRole + ")";

                    if (objUser.IsActive)
                    {
                        if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator) || objUser.UserRole.Equals(UserRoleType.SystemAdministrator))
                        {
                            divInvoiceSelection.Visible = true;
                            divInvoiceType.Visible = true;
                            divAssociates.Visible = true;
                            dvAccessDenied.Visible = false;
                            lbUserName.Text = strUserNameRole;
                            return true;

                        }
                        else if (objUser.UserRole.Equals(UserRoleType.Processor) || objUser.UserRole.Equals(UserRoleType.Approver))
                        {
                            divInvoiceSelection.Visible = false;
                            divInvoiceType.Visible = false;
                            divAssociates.Visible = false;
                            dvAccessDenied.Visible = true;
                            lblAccessDenied.Text = "This page is only for Administrators. Please contact the application administrator for admin access.";
                            lbUserName.Text = strUserNameRole;
                            return false;
                        }
                    }
                    else
                    {
                        divInvoiceSelection.Visible = false;
                        divInvoiceType.Visible = false;
                        divAssociates.Visible = false;
                        dvAccessDenied.Visible = true;
                        lblAccessDenied.Text = "Your status is inactive. Please contact the application administrator for activating the access.";
                        lbUserName.Text = strUserNameRole;
                        return false;
                    }
                }
                else
                {
                    divInvoiceSelection.Visible = false;
                    divInvoiceType.Visible = false;
                    divAssociates.Visible = false;
                    dvAccessDenied.Visible = true;
                    lblAccessDenied.Text = "You are not authorized to access this page. Please contact the application administrator for access.";
                    lbUserName.Text = "Unknown User";
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validating User");
            LogException(ex, "ValidateUser");
        }
        return false;
    }

    /// <summary>
    /// Populates InvoiceTypes Created
    /// </summary>
    private void PopulateInvoiceTypes()
    {
        InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
        try
        {
            List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();

            Authenticate objAuth = new Authenticate();
            Users objUser = objAuth.AuthenticateUser(connectionString, GetUserName());

            if (objUser != null)
            {
                if (!string.IsNullOrEmpty(objUser.UserId))
                {
                    if (objUser.UserRole.Equals(UserRoleType.CustomerAdministrator))
                    {
                        invoiceTypeList = objInvoiceBL.GetInvoiceTypesCustomerAdmin(objUser.UserId);
                    }
                    else
                    {
                        invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
                    }
                }
            }

            var invoiceTypeappendedList = invoiceTypeList.Select(p => new { DisplayValue = p.ID, DisplayText = p.Prefix + "-" + p.Name });

            ddlInvoiceType.DataTextField = "DisplayText";
            ddlInvoiceType.DataValueField = "DisplayValue";
            ddlInvoiceType.DataSource = invoiceTypeappendedList;
            ddlInvoiceType.DataBind();
            ddlInvoiceType.Items.Insert(0, new ListItem(DEFAULT_INVOICETYPE_TEXT, DEFAULT_INVOICE_VALUE));
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Customer info");
            LogException(ex, "PopulateInvoiceTypes");
        }
    }

    /// <summary>
    /// Populates FileTypes based on InvoiceType
    /// </summary>
    /// <param name="InvoiceTypeID"></param>
    private void PopulateFileTypes(int InvoiceTypeID)
    {
        FileTypeBL objFileTypes = new FileTypeBL(connectionString);
        try
        {
            List<FileTypes> lstAllFileTypes = new List<FileTypes>();

            lstAllFileTypes = objFileTypes.GetAllFileTypesByInvoiceTypeId(InvoiceTypeID);
            ddlFileType.DataTextField = DEFAULT_FILETYPE_TEXTFIELD;
            ddlFileType.DataValueField = DEFAULT_FILETYPE_VALUEFIELD;
            ddlFileType.DataSource = lstAllFileTypes;
            ddlFileType.DataBind();
            if (ddlFileType.Items.Count == 0)
            {
                btnAddFileType.Enabled = false;
                ddlFileType.Enabled = false;
            }
            else
            {
                btnAddFileType.Enabled = true;
                ddlFileType.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating FileTypes");
            LogException(ex, "PopulateFileTypes");
        }
    }

    /// <summary>
    /// Populates Associated FileTypes based on InvoiceType
    /// </summary>
    /// <param name="InvoiceTypeID"></param>
    private void PopulateAssoicateFileTypes(int InvoiceTypeID)
    {
        FileTypeBL objFileTypes = new FileTypeBL(connectionString);
        try
        {
            List<FileTypes> lstFileTypes = new List<FileTypes>();
            lstFileTypes = objFileTypes.GetAssoicateFileTypesByInvoiceTypeId(InvoiceTypeID);
            grdFileType.DataSource = lstFileTypes;
            grdFileType.DataBind();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated FileTypes");
            LogException(ex, "PopulateAssoicateFileTypes");
        }
    }

    /// <summary>
    /// Populates Associated Users based on InvoiceType
    /// </summary>
    /// <param name="InvoiceTypeId"></param>
    private void PopulateAssociatedUsers(int InvoiceTypeId)
    {
        UserBL objAssociatedUsers = new UserBL(connectionString);
        try
        {
            List<Users> lstAssociatedUsers = new List<Users>();
            lstAssociatedUsers = objAssociatedUsers.GetUserDetailsByInvoiceType(InvoiceTypeId);

            grdAssociateUsers.DataSource = lstAssociatedUsers;
            grdAssociateUsers.DataBind();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated Users");
            LogException(ex, "PopulateAssociatedUsers");
        }
    }

    /// <summary>
    /// Populates Assocaite Accounts based on InvoiceType
    /// </summary>
    /// <param name="InvoiceTypeID"></param>
    private void PopulateAssociateAccounts(int InvoiceTypeID)
    {
        CRMAccountBL objCRMAccountBL = new CRMAccountBL(connectionString);
        try
        {
            List<CRMAccount> lstCRMAccount = new List<CRMAccount>();
            lstCRMAccount = objCRMAccountBL.GetAssoicateAccountsByInvoiceTypeId(InvoiceTypeID);
            grdAssociateAccounts.DataSource = lstCRMAccount;
            grdAssociateAccounts.DataBind();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated Accounts");
            LogException(ex, "PopulateAssociateAccounts");
        }
    }

    /// <summary>
    /// Inserts or Updates the Invoice based on Event Type
    /// </summary>
    /// <param name="InvoiceType"></param>
    /// <param name="Event"></param>
    private void InsertUpdateInvoice(MBM.Entities.InvoiceType InvoiceType, string Event)
    {
        InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
        int result = 0;
        string userName = GetUserName();
        try
        {
            if (Page.IsValid)
            {
                if (InvoiceType != null)
                {
                    if (InvoiceType.Name != null && InvoiceType.BAN != null && InvoiceType.Prefix != null && InvoiceType.VendorName != null)
                    {
                        if (Event.ToLower().Equals("create"))
                        {
                            InvoiceType.CreatedBy = userName;
                            result = objInvoiceBL.InsertInvoiceType(InvoiceType);
                        }
                        else if (Event.ToLower().Equals("update"))
                        {
                            if (Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString()) > 0)
                            {
                                InvoiceType.ID = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
                            }
                            InvoiceType.UpdatedBy = userName;
                            result = objInvoiceBL.UpdateInvoiceType(InvoiceType);
                        }

                        if (result >= 0)
                        {
                            List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
                            invoiceTypeList = objInvoiceBL.GetInvoiceTypes();

                            Authenticate objAuth = new Authenticate();
                            Users objUser = objAuth.AuthenticateUser(connectionString, GetUserName());

                            if (Event.ToLower().Equals("create"))
                            {
                                UserBL objUserBL = new UserBL(connectionString);
                                objUserBL.InsertAssociatedUser(invoiceTypeList.Where(x => x.Prefix == InvoiceType.Prefix && x.VendorName == InvoiceType.VendorName && x.BAN == InvoiceType.BAN && x.Name == InvoiceType.Name && x.EmailAddress == InvoiceType.EmailAddress
                                                                                       && x.EDI == InvoiceType.EDI && x.IsSOO == InvoiceType.IsSOO && x.FTPUserName == InvoiceType.FTPUserName && x.FTPPassword == InvoiceType.FTPPassword
                                                                                       && x.DefaultFTP == InvoiceType.DefaultFTP).Single().ID, objUser.UserId, userName);
                            }

                            invoiceTypeList = (objUser.UserRole == UserRoleType.CustomerAdministrator) ? objInvoiceBL.GetInvoiceTypesCustomerAdmin(objUser.UserId) : invoiceTypeList;

                            var invoiceTypeappendedList = invoiceTypeList.Select(p => new { DisplayValue = p.ID, DisplayText = p.Prefix + "-" + p.Name });

                            ddlInvoiceType.DataTextField = "DisplayText";
                            ddlInvoiceType.DataValueField = "DisplayValue";
                            ddlInvoiceType.DataSource = invoiceTypeappendedList;
                            ddlInvoiceType.DataBind();

                            ddlInvoiceType.Items.Insert(0, new ListItem(DEFAULT_INVOICETYPE_TEXT, DEFAULT_INVOICE_VALUE));
                            ddlInvoiceType.ClearSelection();

                            ddlInvoiceType.Items.FindByText(InvoiceType.Prefix + "-" + InvoiceType.Name).Selected = true;

                            MBM.Entities.InvoiceType grdobjInvoiceType = new MBM.Entities.InvoiceType();
                            grdobjInvoiceType = invoiceTypeList.Find(x => x.Prefix == ddlInvoiceType.SelectedItem.Text);

                            List<MBM.Entities.InvoiceType> grdinvoiceTypeList = new List<MBM.Entities.InvoiceType>();
                            if (Event.ToLower().Equals("create"))
                            {
                                Controls_ErrorSuccessNotifier.AddSuccessMessage("Create Customer successfully");
                                //  Controls_ErrorSuccessNotifier.AddSuccessMessage("Customer: " + grdobjInvoiceType.Name + " created successfully");
                            }
                            else if (Event.ToLower().Equals("update"))
                            {
                                Controls_ErrorSuccessNotifier.AddSuccessMessage("updated Customer successfully");
                                //   Controls_ErrorSuccessNotifier.AddSuccessMessage("Customer: " + grdobjInvoiceType.Name + " updated successfully");
                            }

                            if (grdobjInvoiceType != null)
                            {
                                //grdinvoiceTypeList.Add(grdobjInvoiceType);
                                //grdInvoiceType.DataSource = grdinvoiceTypeList;
                                //grdInvoiceType.DataBind();

                                if (Event.ToLower().Equals("create"))
                                {
                                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Customer: " + grdobjInvoiceType.Name + " created successfully");
                                }
                                else if (Event.ToLower().Equals("update"))
                                {
                                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Customer: " + grdobjInvoiceType.Name + " updated successfully");
                                }

                                btnEdit.Visible = true;

                                PopulateAssociatedUsers(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                PopulateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                PopulateAssoicateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));

                                divInvoiceType.Visible = true;
                                divAssociates.Visible = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Inserting or Updating a Customer");
            LogException(exception, "InsertUpdateInvoice");
        }
    }

    /// <summary>
    /// Logs Exception whenever Exception raises
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="methodName"></param>
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

    /// <summary>
    /// Gets the CurrencyCode based on provided currencies
    /// </summary>
    /// <param name="ImportCurrencyID"></param>
    /// <param name="ExportCurrencyID"></param>
    /// <returns></returns>
    private Dictionary<string, string> GetCurrenciesCode(int ImportCurrencyID, int ExportCurrencyID)
    {
        _billing = new BillingEngine(connectionString);
        Dictionary<string, string> dicCurrenciesCode = new Dictionary<string, string>();
        try
        {
            List<CurrencyConversion> _importCurrencies = _billing.Configurations.Currencies(DateTime.Now);

            CurrencyConversion ccI = _importCurrencies.Find(
                  delegate(CurrencyConversion c)
                  {
                      return c.ID == ImportCurrencyID;
                  });

            if (ccI != null)
            {
                dicCurrenciesCode.Add("ImportCurrency", ccI.Code.ToString());
            }

            List<CurrencyConversion> _exportCurrencies = _billing.Configurations.Currencies(DateTime.Now);

            CurrencyConversion ccE = _exportCurrencies.Find(
                  delegate(CurrencyConversion c)
                  {
                      return c.ID == ExportCurrencyID;
                  });

            if (ccE != null)
            {
                dicCurrenciesCode.Add("ExportCurrency", ccE.Code.ToString());
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while retrieving Currencies data");
            LogException(ex, "GetCurrenciesCode");
        }
        return dicCurrenciesCode;
    }

    /// <summary>
    /// Validates Ftp Path and Credentails Supplied
    /// </summary>
    /// <param name="strFTPUrl"></param>
    /// <param name="strFTPUsername"></param>
    /// <param name="strFTPPassword"></param>
    /// <returns></returns>
    private bool ValidateFTPPath(string strFTPUrl, string strFTPUsername, string strFTPPassword)
    {
            bool isFTPValid = false;
            Stream ftpStream = null;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(strFTPUrl);
            request.Credentials = new NetworkCredential(strFTPUsername, strFTPPassword);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                ftpStream = response.GetResponseStream();
                isFTPValid = true;
                ftpStream.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                isFTPValid = false;
                LogException(ex, "ValidateFTPPath");
            }
        
        return isFTPValid;
    }


    private bool ValidateSFTPPath(string host, string strFTPUsername, string strFTPPassword)
    {
        bool isSFTPValid = false;
        try
        {
            using (var client = new SftpClient(host, strFTPUsername, strFTPPassword))
            {
                client.Connect();
                client.Disconnect();
                isSFTPValid = true;
            }
        }
        catch (Exception ex)
        {
            isSFTPValid = false;
            LogException(ex, "ValidateSFTPPath");
        }

        return isSFTPValid;
    }

    private void CheckInvoiceType(string PrefixCode)
    {
        mpeNewInvoiceType.Show();
        lblNewInvoiceTypeExeception.Text = "Invoice Type with Prefix " + PrefixCode + " already exists";
        lblNewInvoiceTypeExeception.Visible = true;
        //chkFTPCheckBox.Checked = false;
        //txtDefaultftp.Text = string.Empty;
        //txtFTPUsername.Text = string.Empty;
    }

    //ERO 4002 start
    #region Profile
    /// <summary>
    /// Populates Profiles based on InvoiceType
    /// </summary>
    /// <param name="InvoiceTypeId"></param>
    private void PopulateProfiles(int InvoiceTypeId)
    {
        List<Profile> lstProfiles = GetProfiles(InvoiceTypeId);
        //if (lstProfiles != null && lstProfiles.Count > 0)
        //{
        grdProfiles.DataSource = lstProfiles;
        grdProfiles.DataBind();
        //}

    }

    private List<Profile> GetProfiles(int InvoiceTypeId)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        List<Profile> lstProfiles = new List<Profile>();
        try
        {
            lstProfiles = objProfiles.FetchProfilesByInvoiceId(InvoiceTypeId);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Profiles");
            LogException(ex, "PopulateAssociatedUsers");
        }
        return lstProfiles;
    }


    //SERO-1582
    private List<Profile> GetStringIdentifier(int InvoiceTypeId)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        List<Profile> lstStringIdentofier = new List<Profile>();
        try
        {
            lstStringIdentofier = objProfiles.FetchChargeStringIdentifierByInvoiceId(InvoiceTypeId);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Profiles");
            LogException(ex, "PopulateAssociatedUsers");
        }
        return lstStringIdentofier;
    }

    private void UpdateProfile(Profile profile)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        try
        {
            objProfiles.EditProfile(profile);
            Controls_ErrorSuccessNotifier.AddSuccessMessage("Profile updated successfully.");
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Updating Profiles");
            LogException(ex, "PopulateAssociatedUsers");
        }
    }

    private void ClearAddProfile()
    {
        hAddProfile.Visible = true;
        hEditProfile.Visible = false;
        lblAddProfileErrorMessage.Text = "";
        lblAddProfileErrorMessage.Visible = false;
        txtProfileName.Text = "";
        txtProfileDescription.Text = "";
    }

    private void ClearUpdateProfile()
    {
        hAddProfile.Visible = false;
        hEditProfile.Visible = true;
        lblAddProfileErrorMessage.Text = "";
        lblAddProfileErrorMessage.Visible = false;
        txtProfileName.Text = "";
        txtProfileDescription.Text = "";
    }

    private void showProfileAccoridion()
    {
        accProfiles.Attributes["class"] = "accordion active";
        divProfiles.Attributes["class"] = "panel show";
    }
    #endregion

    #region ProfileCharges
    private void ClearUpdateProfileCharge()
    {
        hAddProfileCharge.Visible = false;
        hEditProfileCharge.Visible = true;
        hAddProfileChargeCSG.Visible = false;
        hEditProfileChargeCSG.Visible = true;
        lblprofileChargesErrorMessage.Text = "";
        lblprofileChargesErrorMessage.Visible = false;
        txtprofileChargeAmount.Text = "";
        txtProfileChargeDesc.Text = "";
        txtprofileChargeFeature.Text = "";
        txtProfileChargeGLCode.Text = "";
        BtnSaveProfileCharge.Visible = false;
        BtnUpdateProfileCharge.Visible = true;
        txtprofileChargeAmount.Enabled = true;
        BtnSaveProfileChargeCSG.Visible = false;
        BtnUpdateProfileChargeCSG.Visible = true;
        lblCSGprofileChargesErrorMessage.Text = "";
        lblCSGprofileChargesErrorMessage.Visible = false;

    }

    private void ClearAddProfileChargePopup()
    {
        hAddProfileCharge.Visible = true;
        hEditProfileCharge.Visible = false;
        hAddProfileChargeCSG.Visible = true;
        hEditProfileChargeCSG.Visible = false;
        txtProfileChargeGLCode.ReadOnly = false;
        txtProfileChargeGLCode.Text = "";
        txtprofileChargeFeature.Text = "";
        txtProfileChargeDesc.Text = "";
        txtprofileChargeAmount.Text = "";
        lblprofileChargesErrorMessage.Text = "";
        lblprofileChargesErrorMessage.Visible = false;
        lblCSGprofileChargesErrorMessage.Text = "";
        lblCSGprofileChargesErrorMessage.Visible = false;
        BtnSaveProfileCharge.CommandName = "Validate";
        BtnSaveProfileCharge.Text = "Validate";
        BtnSaveProfileCharge.Visible = true;
        BtnUpdateProfileCharge.Visible = false;
        ddlProfileName.Enabled = true;
        txtProfileChargeGLCode.Enabled = true;
        BtnSaveProfileCharge.ToolTip = "Prvide GLCode";
        txtCSGprofileChargeAmount.Text = "";
       // txtCSGprofileChargeEffectiveStartDate.Text = "";
        BtnSaveProfileChargeCSG.Visible = true;
        BtnUpdateProfileChargeCSG.Visible = false;
    }


    private void ClearUpdateCSGProfileCharge()
    {
        hAddProfileChargeCSG.Visible = false;
        hEditProfileChargeCSG.Visible = true;
        hAddProfileChargeCSG.Visible = false;
        hEditProfileChargeCSG.Visible = true;
        lblprofileChargesErrorMessage.Text = "";
        lblprofileChargesErrorMessage.Visible = false;
        lblCSGprofileChargesErrorMessage.Text = "";
        lblCSGprofileChargesErrorMessage.Visible = false;
        txtprofileChargeAmount.Text = "";
        txtProfileChargeDesc.Text = "";
        BtnSaveProfileCharge.Visible = false;
        BtnUpdateProfileCharge.Visible = true;
        txtprofileChargeAmount.Enabled = true;
        
    }

    private void showProfileChargeAccoridion()
    {
        accProfileCharges.Attributes["class"] = "accordion active";
        divprofileCharges.Attributes["class"] = "panel show";
    }

    private void ValidateGLCodeWithCRM()
    {
        ProfileCharge profileCharge = new ProfileCharge();
        if (!string.IsNullOrEmpty(txtProfileChargeGLCode.Text))
        {
            profileCharge = validateProfileChargeFromCRM(txtProfileChargeGLCode.Text);
            if (!string.IsNullOrEmpty(profileCharge.ChargeDescription) && !string.IsNullOrEmpty(profileCharge.Feature))
            {
                txtProfileChargeDesc.Text = profileCharge.ChargeDescription;
                txtprofileChargeFeature.Text = profileCharge.Feature;
                txtprofileChargeAmount.Enabled = true;
                BtnSaveProfileCharge.CommandName = "AddCharges";
                BtnSaveProfileCharge.Text = "Save";
            }
            else
            {
                lblprofileChargesErrorMessage.Text = "Invalid GLCode";
                lblprofileChargesErrorMessage.Visible = true;
            }
        }
        else
        {
            lblprofileChargesErrorMessage.Text = "Invalid GLCode";
            lblprofileChargesErrorMessage.Visible = true;
        }
        mpeCreateProfileCharge.Show();
    }

    /// <summary>
    /// Populates Profile charges based on InvoiceType
    /// </summary>
    /// <param name="InvoiceTypeId"></param>
    private void PopulateProfileCharges(int invoiceTypeId, string invoiceTypeName)
    {

        if (lblBillingSystem.Text == "CRM") //SERO-1582
        {
        List<ProfileCharge> lstProfileCharges = GetProfileCharges(invoiceTypeId, invoiceTypeName);

        grdProfileCharges.DataSource = lstProfileCharges;
        grdProfileCharges.DataBind();
            grdProfileCharges.Visible = true;
            accProfileCharges.Visible = true;
            grdProfileChargesCSG.Visible = false;
            accProfileChargesCSG.Visible = false;
    }
        else
        {
            List<ProfileCharge> lstProfileCharges = GetProfileChargesCSG(invoiceTypeId, invoiceTypeName);

            grdProfileChargesCSG.DataSource = lstProfileCharges;
            grdProfileChargesCSG.DataBind();
            grdProfileChargesCSG.Visible = true;
            accProfileChargesCSG.Visible = true;
            grdProfileCharges.Visible = false;
            accProfileCharges.Visible = false;
            int index = 0;
            foreach (var item in lstProfileCharges)
            {

                var _bIsQuorumSynced = item.bIsQuorumSynced;
                // var _bIsQuorumSynced = false;
                if (_bIsQuorumSynced == false)
                {
                    //var data = Convert.ToInt32(Session["grdEditedRow"].ToString());
                    grdProfileChargesCSG.SelectedIndex = index;
                    //grdProfileChargesCSG.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#39c");
                    if (grdProfileChargesCSG.SelectedRow.RowType == DataControlRowType.DataRow)
                    {
                        if (grdProfileChargesCSG.SelectedRow.RowIndex == index)
                        {
                            grdProfileChargesCSG.SelectedRow.Cells[4].Enabled = false;
                            // grdProfileChargesCSG.SelectedRow.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                        }
                        else
                        {
                            grdProfileChargesCSG.SelectedRow.Cells[4].Enabled = true;
                            grdProfileChargesCSG.SelectedRow.Cells[4].BorderStyle = BorderStyle.Solid;
                        }
                    }

                }

                index++;

            }

        }
        //    else
        //    {

        //        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //        //try
        //        //{

        //        //    // string requestUrl = ConfigurationManager.AppSettings["RetrieveSubscriberRateSheet"];
        //        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://services.sbx1.cdops.net/subscribermanagement/RetrieveSubscriberRateSheet ");
        //        //    // HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
        //        //    request.Method = "POST";
        //        //    request.ContentType = "application/json";
        //        //    request.Headers["CD-SystemID"] = ConfigurationManager.AppSettings["CSGAscendonSystemID"];
        //        //    request.Headers["CD-User"] = ConfigurationManager.AppSettings["CSGAscendonUserName"];
        //        //    request.Headers["CD-Password"] = ConfigurationManager.AppSettings["CSGAscendonPassword"];
        //        //    request.Headers["CD-SubscriberId"] = "4985024"; //lblEditParentAccountNameDisplay.Text;
        //        //    request.ContentLength = 0;

        //        //    var response = (HttpWebResponse)request.GetResponse();
        //        //    var encoding = ASCIIEncoding.ASCII;
        //        //    string responseText = string.Empty;

        //        //    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
        //        //    {
        //        //        responseText = reader.ReadToEnd();
        //        //        dynamic response1 = JsonConvert.DeserializeObject(responseText);

        //        //        List<Items> newresult = JsonConvert.DeserializeObject<List<Items>>(response1.Items.ToString());

        //        //        List<ProfileCharge> entities = new List<ProfileCharge>();

        //        //        int i;
        //        //        foreach (Items r in newresult)
        //        //        {
        //        //            List<ProfileCharge> planId = GetCSGPricingPlanId(invoiceTypeId, invoiceTypeName);
        //        //            foreach (ProfileCharge t in planId)
        //        //            {
        //        //                if (r.PricingPlanIdentifier.Value == t.PricingPlanId)
        //        //                {
        //        //                    for (i = 0; i < newresult.Count - 1; i++)
        //        //                    {

        //        //                        ProfileCharge entity = new ProfileCharge()
        //        //                        {
        //        //                            ProductId = r.ProductId,
        //        //                            BillerRuleConfigurationId = r.BillerRuleConfigurationId,
        //        //                            PricingPlanId = r.PricingPlanIdentifier.Value,
        //        //                            Id = r.Id,
        //        //                            Price = r.RateSheetItemAmounts[i].NegotiatedRate
        //        //                        };
        //        //                        if (entity != null)
        //        //                        {
        //        //                            entities.Add(entity);
        //        //                        }
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //grdProfileChargesCSG.DataSource = entities;
        //        //grdProfileChargesCSG.DataBind();
        //        //grdProfileChargesCSG.Visible = true;
        //        //accProfileChargesCSG.Visible = true;
        //        //grdProfileCharges.Visible = false;
        //        //accProfileCharges.Visible = false;
        //        List<ProfileCharge> lstProfileCharges = GetProfileCharges(invoiceTypeId, invoiceTypeName);
        //        grdProfileCharges.DataSource = lstProfileCharges;
        //        grdProfileCharges.DataBind();
        //        grdProfileChargesCSG.Visible = true;
        //        accProfileChargesCSG.Visible = true;
        //        grdProfileCharges.Visible = false;
        //        accProfileCharges.Visible = false;
        //        //}
        //        // int test2 = 29;
        //        // List<ProfileCharge> lstProfileCharges1 = GetProfileCharges(test2, invoiceTypeName);
        //        //}
        //        //catch
        //        //{
        //        //}

        //    } //SERO-1582
        //}       
    }

    private List<ProfileCharge> GetProfileCharges(int invoiceTypeId, string invoiceTypeName)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        List<ProfileCharge> lstProfileCharges = new List<ProfileCharge>();
        try
        {

            lstProfileCharges = objProfiles.FetchProfileChargesByInvoiceId(invoiceTypeId, invoiceTypeName);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated Users");
            LogException(ex, "PopulateAssociatedUsers");
        }
        return lstProfileCharges;
    }

    private List<ProfileCharge> GetProfileChargesCSG(int invoiceTypeId, string invoiceTypeName)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        List<ProfileCharge> lstProfileCharges = new List<ProfileCharge>();
        try
        {

            lstProfileCharges = objProfiles.FetchCSGProfileChargesByInvoiceId(invoiceTypeId, invoiceTypeName);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated Users");
            LogException(ex, "PopulateAssociatedUsers");
        }
        return lstProfileCharges;
    }
    //SERO-1582

    private List<ProfileCharge> GetCSGPricingPlanId(int invoiceTypeId, string invoiceTypeName)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        List<ProfileCharge> lstProfileCharges = new List<ProfileCharge>();
        try
        {

            lstProfileCharges = objProfiles.FetchCSGPricingPlanIdByInvoiceId(invoiceTypeId, invoiceTypeName);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated Users");
            LogException(ex, "PopulateAssociatedUsers");
        }
        return lstProfileCharges;
    }

    private ProfileCharge validateProfileChargeFromCRM(string glCode)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        ProfileCharge profileCharge = new ProfileCharge();

        try
        {
            profileCharge = objProfiles.ValidateProfileChargeFromCRM(glCode);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Associated Users");
            LogException(ex, "PopulateAssociatedUsers");
        }
        return profileCharge;
    }
    #endregion

    #region ManualCharges
    private List<ManualChargeFileDetails> GetManualChargeFileInfoByInvoiceId(int invoiceTypeId)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        List<ManualChargeFileDetails> lstMaualChangeFiles = new List<ManualChargeFileDetails>();
        try
        {
            lstMaualChangeFiles = objProfiles.getMaualChargeFilesInfoByInvoiceType(invoiceTypeId);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while populating Manual charges file info");
            LogException(ex, "GetManualChargeFileInfoByInvoiceId");
        }
        return lstMaualChangeFiles;
    }

    private void populateManualChangeFileInfo(int invoiceTypeId)
    {
        List<ManualChargeFileDetails> lstManualChargeFileDetails = GetManualChargeFileInfoByInvoiceId(invoiceTypeId);
        grdManualChargeFileInfo.DataSource = lstManualChargeFileDetails;
        grdManualChargeFileInfo.DataBind();
    }

    private void ExportGridDataToExcel(int iFileId, string fileName, DownloadFileType datamodel)
    {
        try
        {
            string filenamewithext = fileName;
            string onlyfilename = filenamewithext.Split('.')[0];
            ProfileBL objProfileBL = new ProfileBL(connectionString);

            GridView grdExport = new GridView();
            grdExport.AllowPaging = false;

            switch (datamodel)
            {
                case DownloadFileType.RecordsByFileid:
                    List<ManualCharge_Stage> lstManualCharges = new List<ManualCharge_Stage>();
                    lstManualCharges = objProfileBL.getMaualChargesByFileId(iFileId);
                    if (lstManualCharges.Count == 0)
                        lstManualCharges.Add(new ManualCharge_Stage());
                    grdExport.DataSource = lstManualCharges;
                    break;
                case DownloadFileType.SuccessRecords:
                    List<ManualCharge_Success> lstSuccessManualCharges = new List<ManualCharge_Success>();
                    lstSuccessManualCharges = objProfileBL.getSuccessRecordsByInvoiceId(iFileId);
                    if (lstSuccessManualCharges.Count == 0)
                        lstSuccessManualCharges.Add(new ManualCharge_Success());
                    grdExport.DataSource = lstSuccessManualCharges;
                    break;

            }
            grdExport.DataBind();

            //Apply style to Individual Cells
            for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
            {
                if (DownloadFileType.RecordsByFileid == datamodel)
                {
                    if (grdExport.HeaderRow.Cells[j].Text.Equals("Status") || grdExport.HeaderRow.Cells[j].Text.Equals("Comments"))
                    {
                        grdExport.HeaderRow.Cells[j].Style.Add("background-color", "#FF0000");
                        continue;
                    }
                }
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
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + onlyfilename + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xls");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            //System.Web.HttpContext.Current.Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; 
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
        finally
        {
            //showManualChargeAccoridion();
        }
    }

    private void ExportDataTableToExcel(int iFileId, string fileName, DownloadFileType datamodel)
    {
        string filenamewithext = fileName;
        string onlyfilename = Path.GetFileNameWithoutExtension(fileName);//filenamewithext.Split('.')[0];
        ProfileBL objProfileBL = new ProfileBL(connectionString);

        try
        {
            string newfile = onlyfilename + "_" + DateTime.Now.ToString("yyyyMMddHHMMss") + ".xlsx";

            switch (datamodel)
            {
                case DownloadFileType.RecordsByFileid:
                    List<ManualCharge_Stage> lstManualCharges = new List<ManualCharge_Stage>();
                    lstManualCharges = objProfileBL.getMaualChargesByFileId(iFileId);
                    //if (lstManualCharges.Count == 0)
                    //    lstManualCharges.Add(new ManualCharge_Stage());
                    CreateExcelFile.CreateExcelDocument(lstManualCharges, newfile, System.Web.HttpContext.Current.Response);
                    break;
                case DownloadFileType.SuccessRecords:
                    List<ManualCharge_Success> lstSuccessManualCharges = new List<ManualCharge_Success>();
                    lstSuccessManualCharges = objProfileBL.getSuccessRecordsByInvoiceId(iFileId);
                    //if (lstSuccessManualCharges.Count == 0)
                    //    lstSuccessManualCharges.Add(new ManualCharge_Success());
                    CreateExcelFile.CreateExcelDocument(lstSuccessManualCharges, newfile, System.Web.HttpContext.Current.Response);
                    break;

            }
        }
        catch (System.Threading.ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {
            //Controls_ErrorSuccessNotifier.AddErrorMessage("Problem Occurred while exporting the data");
            LogException(ex, "ExportToExcel");
        }
    }

    private void SendMailNotification(string userName, int fileId, string fileName, MailType mailType, string errorMsg = "")
    {
        try
        {
            string body = string.Empty;
            System.Collections.Specialized.StringCollection toAddress = new System.Collections.Specialized.StringCollection();
            ADUserInfo user = new ADUserInfo();
            ProfileBL objProfileBL = new ProfileBL(connectionString);
            string subject = string.Empty;

            user = GetADUserInfo(userName);

            if (mailType == MailType.ManualChargeTemplate)
            {
                ManualChargeFileSummary chargeSummary = objProfileBL.getManualChargeFileProcessSummary(fileId);
                if (chargeSummary.SuccessRecords > 0 && chargeSummary.FailedRecords > 0)
                {
                    subject = Server.HtmlEncode("MBM Manual Charge file Transaction(s) Failed and Successful");
                }
                else
                {
                    if (chargeSummary.SuccessRecords > 0 && chargeSummary.FailedRecords == 0)
                    {
                        subject = Server.HtmlEncode("MBM Manual Charge file Transaction(s) Successful");
                    }
                    if (chargeSummary.SuccessRecords == 0 && chargeSummary.FailedRecords > 0)
                    {
                        subject = Server.HtmlEncode("MBM Manual Charge file Transaction(s) Failed");
                    }
                }
                body = mailbodyforProcessedData(user, fileId, fileName, chargeSummary);
                if (string.IsNullOrEmpty(subject))
                {
                    errorMsg = "<p>Hi " + user.FirstName + ",<p>" + "Your uploaded file does not have proper information";
                }
            }
            if (mailType == MailType.ManualChargeFailedTemplate)
            {
                subject = Server.HtmlEncode("MBM Manual Upload File has failed");
                body = mailbodyforBulkUploadFail(user, fileId, fileName, errorMsg);
            }
            if (string.IsNullOrEmpty(subject))
            {
                subject = Server.HtmlEncode("MBM Manual Upload File has failed");
                errorMsg = "<p>Hi " + user.FirstName + ",<p>" + "Your uploaded file does not have proper information";
                body = mailbodyforBulkUploadFail(user, fileId, fileName, errorMsg);
            }

            SendMail sendmail = new SendMail();
            string fromAddress = ConfigurationManager.AppSettings["EmailFromAddress"].ToString();

            toAddress.Add(user.MailId);

            sendmail.SendEmail(toAddress, subject, null, body, fromAddress, null);
        }
        catch (Exception ex)
        {
            LogException(ex, "SendMailNotification");
            throw ex;
        }
    }

    /// <summary>
    /// preaper mail body format.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="fileId"></param>
    /// <returns></returns>
    private string mailbodyforProcessedData(ADUserInfo userInfo, int fileId, string fileName, ManualChargeFileSummary chargeSummary)
    {
        ProfileBL objProfileBL = new ProfileBL(connectionString);
        StringBuilder strBody = new StringBuilder();

        string path = HttpContext.Current.Server.MapPath(MailTemplate.ManualChargeTemplate);

        strBody.Append(File.ReadAllText(path));
        strBody.Replace("#USER#", userInfo.FirstName);

        if (chargeSummary != null)
        {
            strBody.Replace("#Success#", chargeSummary.SuccessRecords.ToString());
            strBody.Replace("#Failure#", chargeSummary.FailedRecords.ToString());
            strBody.Replace("#FileName#", fileName);
        }
        return strBody.ToString();
    }

    private string mailbodyforBulkUploadFail(ADUserInfo userInfo, int fileId, string fileName, string errorMsg)
    {
        ProfileBL objProfileBL = new ProfileBL(connectionString);
        StringBuilder strBody = new StringBuilder();

        string path = HttpContext.Current.Server.MapPath(MailTemplate.ManualChargeFailedTemplate);

        strBody.Append(File.ReadAllText(path));
        strBody.Replace("#USER#", userInfo.FirstName);
        strBody.Replace("#FileName#", fileName);
        strBody.Replace("#Errormsg#", errorMsg);
        return strBody.ToString();
    }

    public static ADUserInfo GetADUserInfo(string strCORPNETId)
    {
        string cropid = strCORPNETId.Split(new char[] { '\\' }, StringSplitOptions.None)[1];
        DirectoryEntry entry = new DirectoryEntry("LDAP://cinbell.com");
        List<ADUserInfo> listUsers = new List<ADUserInfo>();
        DirectorySearcher Dsearch = new DirectorySearcher(entry);

        if (!string.IsNullOrEmpty(cropid))
        {
            Dsearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + cropid + "))";



            foreach (SearchResult item in Dsearch.FindAll())
            {
                if (item.GetDirectoryEntry().Properties["mail"].Value != null && item.GetDirectoryEntry().Properties["samaccountname"].Value != null && item.GetDirectoryEntry().Properties["displayname"].Value != null)
                {

                    ADUserInfo objSurveyUsers = new ADUserInfo();
                    objSurveyUsers.CorpnetId = (String)item.GetDirectoryEntry().Properties["samaccountname"].Value;
                    objSurveyUsers.MailId = (String)item.GetDirectoryEntry().Properties["mail"].Value;
                    objSurveyUsers.DisplayName = (String)item.GetDirectoryEntry().Properties["displayname"].Value;
                    objSurveyUsers.FirstName = objSurveyUsers.DisplayName.Split(',').Count() > 1 ? objSurveyUsers.DisplayName.Split(',')[1] : objSurveyUsers.DisplayName;
                    listUsers.Add(objSurveyUsers);
                }
            }

        }
        if (listUsers.Count > 0)
        {
            return listUsers.First<ADUserInfo>();
        }
        else
            return new ADUserInfo();

    }

    private int? UploadManualChargeFile(int? invoiceTypeId, string fileName, string fileStatus, string userName)
    {
        int? fileId = 0;
        ProfileBL objProfiles = new ProfileBL(connectionString);
        fileId = objProfiles.CreateManualChargeFile(invoiceTypeId, fileName, fileStatus, userName);
        return fileId;
    }

    private int? ProcessMaualChargesData(int? fileId)
    {
        ProfileBL objProfiles = new ProfileBL(connectionString);
        return objProfiles.ProcessMaualChargesData(fileId);
    }

    private string SaveExcelToServer()
    {
        string FileName = Path.GetFileName(fpManualFileuploadFilePath.PostedFile.FileName);
        string Extension = Path.GetExtension(fpManualFileuploadFilePath.PostedFile.FileName);
        if (Extension != ".xlsx" && Extension != ".xls")
            throw new Exception("Unsupported format file");
        string FolderPath = string.Empty;
        if (ConfigurationManager.AppSettings["MBMChargeFilePath"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["MBMChargeFilePath"]))
        {
            FolderPath = ConfigurationManager.AppSettings["MBMChargeFilePath"];
        }
        //string FolderPath = @"\\HVSALPOR01D\e$\MBMChargeFiles\";
        string FilePath = FolderPath + FileName;
        fpManualFileuploadFilePath.SaveAs(FilePath);

        return FilePath;
    }

    private DataTable ReadDataFromExcel(string filePath)
    {
        DataTable dt = new DataTable();
        string excelConnection = string.Empty;
        try
        {
            if (ConfigurationManager.ConnectionStrings["ExcelConnectionString"].ConnectionString != null)
            {
                excelConnection = ConfigurationManager.ConnectionStrings["ExcelConnectionString"].ConnectionString.ToString();
                //always send true to HDR(header)
                excelConnection = string.Format(excelConnection, filePath, true);//TODO: true
                OleDbConnection excelConn = new OleDbConnection(excelConnection);
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = excelConn;
                excelConn.Close();
                //Get the name of First Sheet in excel
                excelConn.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                excelConn.Close();
                //Read Data from First Sheet in excell
                excelConn.Open();
                cmd.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmd;
                oda.Fill(dt);
                excelConn.Close();
            }
        }
        catch (Exception ex)
        {
            // always close the connection if unhandle exception occuers
            excelConnection = ConfigurationManager.ConnectionStrings["ExcelConnectionString"].ConnectionString.ToString();
            excelConnection = string.Format(excelConnection, filePath, true);
            OleDbConnection excelConn = new OleDbConnection(excelConnection);
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            cmd.Connection = excelConn;
            excelConn.Close();
            LogException(ex, "ReadDataFromExcel");
            throw;
        }
        return dt;
    }

    /// <summary>
    /// Maping new columns to the table
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fileId"></param>
    /// <returns></returns>
    private DataTable MapDataTable(ref DataTable dt, int? fileId)
    {
        foreach (DataRow row in dt.Rows)
        {
            row.SetField("FileId", fileId);
            row.SetField("InvoiceTypeId", ddlInvoiceType.SelectedValue);
        }
        return dt;
    }

    /// <summary>
    /// enable the Manual charge button in particular period
    /// </summary>
    private void ManualChargeUploadEnable()
    {
        bool isEnabled = false;
        ProfileBL objProfiles = new ProfileBL(connectionString);
        try
        {
            isEnabled = objProfiles.isMChareButtonEnable();
            btnManualUploadDailog.Enabled = isEnabled;
            if (!isEnabled)
            {
                btnManualUploadDailog.ToolTip = "you are unable to add or remove bill items at this period";
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while configuring Manualcharge button enability");
            LogException(ex, "ManualChargeUploadEnable");
        }
    }

    private void showManualChargeAccoridion()
    {
        accManualUpload.Attributes["class"] = "accordion active";
        divManualUpload.Attributes["class"] = "panel show";
    }

    private void updateManualChargeFileStatus(int fileId, string filestatus)
    {
        try
        {
            ProfileBL objProfiles = new ProfileBL(connectionString);
            objProfiles.UpdateManualChargeFileStatus(fileId, filestatus);
        }
        catch (Exception ex)
        {
            LogException(ex, "updateManualChargeFileStatus");
            throw;
        }
    }
    #endregion
    //ERO 4002 end
    #endregion

    #region ControlEvents
    /// <summary>
    /// Opens the PopUp to create New InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewInvoiceType_Click(object sender, EventArgs e)
    {
        try
        {
            txtNewInvoiceType.Text = string.Empty;
            txtPrefixCode.Enabled = true;
            txtPrefixCode.Text = string.Empty;
            txtDefaultBan.Text = string.Empty;
            txtVendorName.Text = string.Empty;
            txtFTPUsername.Text = string.Empty;
            txtFTPPassword.Attributes["value"] = string.Empty;

            //automation
            chkEnableAutoPreBilling.Checked = true;
            chkEnableAutoPostBilling.Checked = true;

            lblNewInvoiceTypeExeception.Visible = false;
            SetCurrencies(DEFAULT_IMPORT_CURRENCYID, DEFAULT_EXPORT_CURRENCYID);
            txtDefaultftp.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            lblheaderInvoiceType.Text = "New Customer";
            btnCreate.Text = "Create";

            //sero-3511 start

            List<MBM.Entities.AscendonGLDepartmentCodes> ascendonGLDepartmentCodesList = new List<MBM.Entities.AscendonGLDepartmentCodes>();
            InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
            //lblNewInvoiceTypeExeception.Visible = false;
            ascendonGLDepartmentCodesList = objInvoiceBL.GetAscendonGLDepartmentCodes();
            //MBM.Entities.AscendonGLDepartmentCodes objAscendonGLDepartmentCodes = new MBM.Entities.AscendonGLDepartmentCodes();
            //objAscendonGLDepartmentCodes = ascendonGLDepartmentCodesList.Select(x => x).ToList() ;
            //var ascendonGLDepartmentCodesListResult = ascendonGLDepartmentCodesList.Select(p => new { DisplayValue = p.GLDepartmentID, DisplayText =p.GLDepartmentValue});
            ddlGLDepartmentCode.DataTextField = "GLDepartmentValue";
            ddlGLDepartmentCode.DataValueField = "GLDepartmentID";
            ddlGLDepartmentCode.DataSource = ascendonGLDepartmentCodesList;
            ddlGLDepartmentCode.DataBind();
            ddlGLDepartmentCode.Items.Insert(0, new ListItem(DEFAULT_GL_Department_Code_TEXT, DEFAULT_INVOICE_VALUE));

            //var indirectAgentRegionResult = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode).ToList().Select(p => new { DisplayValue = p.GLDepartmentID, DisplayText = p.GLDepartmentValue });
            var indirectAgentRegionResult = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode_Indirect_Channel).ToList().Select(p => p);
            ddlIndirectAgentRegion.DataTextField  = "GLDepartmentValue";
            ddlIndirectAgentRegion.DataValueField  = "GLDepartmentID";
            ddlIndirectAgentRegion.DataSource = indirectAgentRegionResult;
            ddlIndirectAgentRegion.DataBind();
            ddlIndirectAgentRegion.Items.Insert(0, new ListItem(DEFAULT_Indirect_Agent_Region_TEXT, DEFAULT_INVOICE_VALUE));

            txtContractNumber.Text = string.Empty;
            txtContractStartDate.Text = string.Empty;
            txtContractEndDate.Text = string.Empty;
            txtIndirectPartnerOrRepCode.Text = string.Empty;
            ddlBillingSystem.SelectedIndex = 0;
            divGL.Visible = false;     
        

            //sero-3511 end


            mpeNewInvoiceType.Show();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while loading New Customer popup");
            LogException(ex, "btnNewInvoiceType_Click");
        }
    }


    //Ser03511 start
    protected void ddlBillingSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var billingSystem = ddlBillingSystem.SelectedItem.Value;
            if (billingSystem=="CRM")
            {                
                divGL.Visible = false;             
            }
            else
            {              
                divGL.Visible = true;
            }

            mpeNewInvoiceType.Show();

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("");
            LogException(ex, "btnNewInvoiceType_Click");
        }
    }


    //Sero3511 end 
    //bug-31813
    protected void ddlGLDepartmentCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var glcode = ddlGLDepartmentCode.SelectedItem.Text;
            if (glcode == "999100 : Indirect Channel")
            {
                reqddlIndirectAgentRegion.Enabled = true; // Enable validation for child dropdown
            }
            else
            {
                reqddlIndirectAgentRegion.Enabled = false; // Disable validation for child dropdown
            }

            mpeNewInvoiceType.Show();

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("");
            LogException(ex, "ddlGLDepartmentCode_Change");
        }
    }

    protected void ddlIndirectAgentRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var glcode = ddlGLDepartmentCode.SelectedItem.Text;
            if (glcode == "999100 : Indirect Channel")
            {
                reqddlIndirectAgentRegion.Enabled = true; // Enable validation for child dropdown
            }
            else
            {
                reqddlIndirectAgentRegion.Enabled = false; // Disable validation for child dropdown
            }

            mpeNewInvoiceType.Show();

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("");
            LogException(ex, "ddlGLDepartmentCode_Change");
        }
    }

    protected void btnAssoicateAccountEdit_Click(object sender, EventArgs e)
    {
        lblEditAccountErrorMessage.Text = string.Empty;
        lblEditAccountErrorMessage.Visible = false;
        //ER8668
        //int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
        //Label lblAccountNumber = grdAssociateAccounts.Rows[0].FindControl("lblAccount") as Label;
        //string accountNumber = lblAccountNumber.Text;
        //AccountEffectiveDateStatus(invoiceTypeId, accountNumber);
        //ER 8668
        mpeEditAccount.Show();
    }

    /// <summary>
    /// Creates or Updates InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        MBM.Entities.InvoiceType objNewInvoiceType = new MBM.Entities.InvoiceType();
        string Event = btnCreate.Text;

        try
        {
            lblNewInvoiceTypeExeception.Visible = false;
            if (!String.IsNullOrEmpty(txtNewInvoiceType.Text.Trim()))
            {
                objNewInvoiceType.Name = txtNewInvoiceType.Text.Trim();
                if (!String.IsNullOrEmpty(txtDefaultBan.Text.Trim()))
                {
                    objNewInvoiceType.BAN = txtDefaultBan.Text.Trim();
                }
                if (!String.IsNullOrEmpty(txtPrefixCode.Text.Trim()))
                {
                    objNewInvoiceType.Prefix = txtPrefixCode.Text.Trim().ToUpper();
                }
                if (!String.IsNullOrEmpty(txtVendorName.Text.Trim()))
                {
                    objNewInvoiceType.VendorName = txtVendorName.Text.Trim();
                }
                if (!String.IsNullOrEmpty(txtDefaultftp.Text.Trim()))
                {
                    objNewInvoiceType.DefaultFTP = txtDefaultftp.Text.Trim();
                }
                //CBE_8967
                if (!String.IsNullOrEmpty(ddlOutputFileFormat.SelectedItem.Value))
                {
                    objNewInvoiceType.OutputFileFormat = Convert.ToInt32(ddlOutputFileFormat.SelectedItem.Value);
                }
                //cbe_11609
                if (!String.IsNullOrEmpty(ddlIsSOO.SelectedItem.Value))
                {
                    objNewInvoiceType.IsSOO = Convert.ToInt32(ddlIsSOO.SelectedItem.Value);
                }

                //SERO-1582
                if (!string.IsNullOrEmpty(ddlBillingSystem.SelectedItem.Value))
                {
                    objNewInvoiceType.BillingSystem = ddlBillingSystem.SelectedItem.Value;
                }
                //SERO-1582
                if (!string.IsNullOrEmpty(ddlautomationFrequency.SelectedItem.Value))
                {
                    objNewInvoiceType.iAutomationFrequency = ddlautomationFrequency.SelectedItem.Value;
                }

                if (chkEnableAutoPreBilling.Checked)
                {
                    objNewInvoiceType.IsAutoPreBilling = true;
                    objNewInvoiceType.DaysBeforeBillCycle = Convert.ToInt32(ddlDaysBeforeBillcycle.SelectedItem.Value);
                }
                else
                {
                    objNewInvoiceType.IsAutoPreBilling = false;
                    objNewInvoiceType.DaysBeforeBillCycle = 0;
                }

                if (chkEnableAutoPostBilling.Checked)
                {
                    objNewInvoiceType.IsAutoPostBilling = true;
                    //objNewInvoiceType.DaysAfterBillCycle = Convert.ToInt32(ddlDaysAfterBillcycle.SelectedItem.Value);
                    objNewInvoiceType.DaysAfterBillCycle = Convert.ToInt32(ConfigurationManager.AppSettings["AutomationPostBillDays"].ToString());
                }
                else
                {
                    objNewInvoiceType.IsAutoPostBilling = false;
                    objNewInvoiceType.DaysAfterBillCycle = 0;
                }

                if (!String.IsNullOrEmpty(txtEmailAddress.Text.Trim()))
                {
                    objNewInvoiceType.EmailAddress = txtEmailAddress.Text.Trim();
                }

                if (chkEDINew.Checked)
                {
                    objNewInvoiceType.EDI = true;
                }
                else
                {
                    objNewInvoiceType.EDI = false;
                }

                if (!String.IsNullOrEmpty(ddlImportCurrency.SelectedItem.Value) && !String.IsNullOrEmpty(ddlExportCurrency.SelectedItem.Value))
                {
                    Dictionary<string, string> dicCurrencies = new Dictionary<string, string>();
                    dicCurrencies = GetCurrenciesCode(Convert.ToInt32(ddlImportCurrency.SelectedItem.Value), Convert.ToInt32(ddlExportCurrency.SelectedItem.Value));
                    objNewInvoiceType.ImportCurrencyDefault = dicCurrencies["ImportCurrency"].ToString();
                    objNewInvoiceType.ExportCurrencyDefault = dicCurrencies["ExportCurrency"].ToString();
                }

                #region sero3511 start

                if (divGL.Visible == true)
                {
                    if (!String.IsNullOrEmpty(txtContractNumber.Text.Trim()))
                    {
                        objNewInvoiceType.ContractNumber = txtContractNumber.Text.Trim();
                    }
                    else
                    {
                        objNewInvoiceType.ContractNumber = string.Empty;
                    }

                    if (!String.IsNullOrEmpty(txtContractStartDate.Text.Trim()))
                    {
                        objNewInvoiceType.ContractStartDate = Convert.ToDateTime(txtContractStartDate.Text.ToString());
                    }
                    else
                    {
                        objNewInvoiceType.ContractStartDate = DateTime.Now;
                    }
                    if (!String.IsNullOrEmpty(txtContractEndDate.Text.Trim()))
                    {
                        objNewInvoiceType.ContractEndDate = Convert.ToDateTime(txtContractEndDate.Text.ToString());
                    }
                    else
                    {
                        objNewInvoiceType.ContractEndDate = DateTime.Now;
                    }

                    if (!String.IsNullOrEmpty(txtIndirectPartnerOrRepCode.Text.Trim()))
                    {
                        objNewInvoiceType.IndirectPartnerOrRepCode = txtIndirectPartnerOrRepCode.Text.Trim();
                    }
                    else
                    {
                        objNewInvoiceType.IndirectPartnerOrRepCode = string.Empty;
                    }
                    if (Convert.ToInt32(ddlGLDepartmentCode.SelectedItem.Value) != 0)
                    {
                        objNewInvoiceType.GLDepartmentCode = Convert.ToString(ddlGLDepartmentCode.SelectedItem.Value);
                    }
                    else
                    {
                        objNewInvoiceType.GLDepartmentCode = string.Empty;
                    }
                    if (Convert.ToInt32(ddlIndirectAgentRegion.SelectedItem.Value) != 0)
                    {
                        objNewInvoiceType.IndirectAgentRegion = Convert.ToString(ddlIndirectAgentRegion.SelectedItem.Value);
                    }
                    else
                    {
                        objNewInvoiceType.IndirectAgentRegion = string.Empty;
                    }
                }
  #endregion sero3511 end

                string strFTPUrl = txtDefaultftp.Text.Trim();
                string strFTPUsername = txtFTPUsername.Text.Trim();
                string strFTPPassword = txtFTPPassword.Text.Trim();

                if (!String.IsNullOrEmpty(strFTPUrl) && chkFTPCheckBox.Checked)
                {
                    if (!String.IsNullOrEmpty(strFTPUsername) && !String.IsNullOrEmpty(strFTPPassword) && strFTPUrl.StartsWith("ftp://"))
                    {

                        if (!strFTPUrl.StartsWith("ftp://"))
                        {
                            strFTPUrl = "ftp://" + strFTPUrl;
                        }

                        if (strFTPUrl.Contains("\\"))
                        {
                            strFTPUrl = strFTPUrl.Replace("\\", "/");
                        }

                        if (!strFTPUrl.Substring(strFTPUrl.Length - 1, 1).Equals("/"))
                        {
                            strFTPUrl = strFTPUrl + "/";
                        }

                            if (ValidateFTPPath(strFTPUrl, strFTPUsername, strFTPPassword))
                            {

                                DataEncryptor objEncrypt = new DataEncryptor();

                                objNewInvoiceType.FTPUserName = strFTPUsername;
                                objNewInvoiceType.FTPPassword = objEncrypt.Encrypt(strFTPPassword);
                                objNewInvoiceType.DefaultFTP = strFTPUrl;

                                //InsertUpdateInvoice(objNewInvoiceType, Event);
                                //lblNewInvoiceTypeExeception.Visible = false;
                                //Check if the Invoice Type exists.
                                //List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
                                //InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);

                                //Authenticate objAuth = new Authenticate();
                                //Users objUser = objAuth.AuthenticateUser(connectionString, GetUserName());

                                //invoiceTypeList = (objUser.UserRole == UserRoleType.CustomerAdministrator) ? objInvoiceBL.GetInvoiceTypesCustomerAdmin(objUser.UserId) : objInvoiceBL.GetInvoiceTypes();

                                //MBM.Entities.InvoiceType invoiceType = invoiceTypeList.Where(m => m.Prefix.ToLower() == txtPrefixCode.Text.Trim().ToLower()).FirstOrDefault();

                                //if (invoiceType != null && Event.ToLower().Equals("create"))
                                //{
                                //    CheckInvoiceType(txtPrefixCode.Text);
                                //}
                                //else if (invoiceType != null && Event.ToLower().Equals("update"))
                                //{
                                //    if (invoiceType.Prefix.ToLower() == txtPrefixCode.Text.Trim().ToLower() && invoiceType.ID != Convert.ToInt32(ddlInvoiceType.SelectedValue))
                                //    {
                                //        CheckInvoiceType(txtPrefixCode.Text);
                                //    }
                                //    else
                                //    {
                                //        InsertUpdateInvoice(objNewInvoiceType, Event.ToLower());
                                //    }
                                //}
                                //else
                                //{
                                InsertUpdateInvoice(objNewInvoiceType, Event.ToLower());
                                //}
                            }
                        //}

                     
                        else
                        {
                            mpeNewInvoiceType.Show();
                            lblNewInvoiceTypeExeception.Text = "Invalid FTP Path/Credentials";
                            lblNewInvoiceTypeExeception.Visible = true;
                            chkFTPCheckBox.Checked = true;
                            txtDefaultftp.Enabled = true;
                            txtFTPUsername.Enabled = true;
                            txtFTPPassword.Enabled = true;
                        }
                    }

                    else if (!String.IsNullOrEmpty(strFTPUsername) && !String.IsNullOrEmpty(strFTPPassword) && strFTPUrl.StartsWith("sftp://"))
                    {
                        // Remove the "sftp://" prefix
                        string cleanedUrl = strFTPUrl.Substring("sftp://".Length);
                        cleanedUrl = cleanedUrl.Substring(0, cleanedUrl.Length - 1);
                        int lastSlashIndex = cleanedUrl.LastIndexOf('/');

                        if (lastSlashIndex >= 0)
                        {
                            // Remove the part after the last '/'
                            cleanedUrl = cleanedUrl.Substring(0, lastSlashIndex);
                        }

                        if (ValidateSFTPPath(cleanedUrl, strFTPUsername, strFTPPassword))
                        {
                            DataEncryptor objEncrypt = new DataEncryptor();

                            objNewInvoiceType.FTPUserName = strFTPUsername;
                            objNewInvoiceType.FTPPassword = objEncrypt.Encrypt(strFTPPassword);
                            objNewInvoiceType.DefaultFTP = strFTPUrl;
                            InsertUpdateInvoice(objNewInvoiceType, Event.ToLower());
                        }
                        else
                        {
                            mpeNewInvoiceType.Show();
                            lblNewInvoiceTypeExeception.Text = "Invalid FTP Path/Credentials";
                            lblNewInvoiceTypeExeception.Visible = true;
                            chkFTPCheckBox.Checked = true;
                            txtDefaultftp.Enabled = true;
                            txtFTPUsername.Enabled = true;
                            txtFTPPassword.Enabled = true;
                        }
                    }
                    else
                    {
                        mpeNewInvoiceType.Show();
                        lblNewInvoiceTypeExeception.Text = "FTP UserName/Password required";
                        lblNewInvoiceTypeExeception.Visible = true;
                        chkFTPCheckBox.Checked = true;
                        txtDefaultftp.Enabled = true;
                        txtFTPUsername.Enabled = true;
                        txtFTPPassword.Enabled = true;
                    }
                }

                else
                {
                    //TODO
                    //Check if the Invoice Type exists.
                    //List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
                    //InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
                    //invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
                    //MBM.Entities.InvoiceType invoiceType = invoiceTypeList.Where(m => m.Prefix.ToLower() == txtPrefixCode.Text.Trim().ToLower()).FirstOrDefault();

                    //if (invoiceType != null && Event.ToLower().Equals("create"))
                    //{ 
                    //    CheckInvoiceType(txtPrefixCode.Text);
                    //}
                    //else if (invoiceType != null && Event.ToLower().Equals("update"))
                    //{
                    //    if (invoiceType.Prefix.ToLower() == txtPrefixCode.Text.Trim().ToLower() && invoiceType.ID != Convert.ToInt32(ddlInvoiceType.SelectedValue))
                    //    {
                    //        CheckInvoiceType(txtPrefixCode.Text);
                    //    }
                    //    else
                    //    {
                    //        InsertUpdateInvoice(objNewInvoiceType, Event);
                    //    }
                    //}
                    //else
                    //{
                            InsertUpdateInvoice(objNewInvoiceType, Event);
                    //}
                }
            }

            ddlInvoiceType_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Inserting or Updating a Customer");
            LogException(ex, "btnCreate_Click");
            lblNewInvoiceTypeExeception.Visible = false;
        }
    }

    /// <summary>
    /// Populates data related to selected InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlInvoiceType.SelectedItem.Text != "- Select InvoiceType -")
            {
                List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
                InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
                //TODO
                invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
                MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();
                //Sero-3511 start
                List<MBM.Entities.AscendonGLDepartmentCodes> ascendonGLDepartmentCodesList = new List<MBM.Entities.AscendonGLDepartmentCodes>();
                ascendonGLDepartmentCodesList = objInvoiceBL.GetAscendonGLDepartmentCodes();
                var indirectAgentRegionResult = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode_Indirect_Channel).ToList().Select(p => p);
                //Sero-3511 End
                objInvoiceType = invoiceTypeList.Where(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString())).FirstOrDefault();
                List<MBM.Entities.InvoiceType> grdinvoiceTypeList = new List<MBM.Entities.InvoiceType>();
                if (objInvoiceType != null)
                {
                    //grdinvoiceTypeList.Add(objInvoiceType);
                    //grdInvoiceType.DataSource = grdinvoiceTypeList;
                    //grdInvoiceType.DataBind();
                    if (!string.IsNullOrEmpty(objInvoiceType.Name))
                    {
                        lblCustomer.Text = objInvoiceType.Name;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.BAN))
                    {
                        lblDefaultBAN.Text = objInvoiceType.BAN;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.Prefix))
                    {
                        lblPrefix.Text = objInvoiceType.Prefix;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.VendorName))
                    {
                        lblVendor.Text = objInvoiceType.VendorName;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.ImportCurrencyDefault))
                    {
                        lblImport.Text = objInvoiceType.ImportCurrencyDefault;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.ExportCurrencyDefault))
                    {
                        lblExport.Text = objInvoiceType.ExportCurrencyDefault;
                    }
                    //cbe_8967
                    if (!string.IsNullOrEmpty(objInvoiceType.OutputFileFormat.ToString()))
                    {
                        lblOutputFormat.Text = (objInvoiceType.OutputFileFormat.ToString() == "1") ? "1 Combined File"
                                                : "5 Individual Files";
                    }
                    //cbe_11609
                    if (!string.IsNullOrEmpty(objInvoiceType.IsSOO.ToString()))
                    {
                        IsSOOlabel.Text = (objInvoiceType.IsSOO.ToString() == "1") ? "True"
                                                : "False";
                    }
                    //SERO-1582
                    if (!string.IsNullOrEmpty(objInvoiceType.BillingSystem.ToString()))
                    {
                        lblBillingSystem.Text = (objInvoiceType.BillingSystem.ToString() == "CRM") ? "CRM" : "CSG Ascendon";
                    }
                    //SERO-1582
                    if (!string.IsNullOrEmpty(objInvoiceType.iAutomationFrequency.ToString()))
                    {
                        lblautomationFrequency.Text = objInvoiceType.iAutomationFrequency.ToString();
                    }
                    if (objInvoiceType.IsAutoPreBilling)
                    {
                        lblAutoPreBiling.Text = "Enabled";
                    }
                    else
                    {
                        lblAutoPreBiling.Text = "Disabled";
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.DaysBeforeBillCycle.ToString()))
                    {
                        lblDaysBeforeBillCycle.Text = objInvoiceType.DaysBeforeBillCycle.ToString();
                    }
                    if (objInvoiceType.IsAutoPostBilling)
                    {
                        lblAutoPostBiling.Text = "Enabled";
                    }
                    else
                    {
                        lblAutoPostBiling.Text = "Disabled";
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.DaysAfterBillCycle.ToString()))
                    {
                        lblDaysAfterBillCycle.Text = objInvoiceType.DaysAfterBillCycle.ToString(); ;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.DefaultFTP))
                    {
                        lblDefaultftpPath.Text = objInvoiceType.DefaultFTP;
                    }
                    if (!string.IsNullOrEmpty(objInvoiceType.EmailAddress))
                    {
                        lblEmailAddress.Text = objInvoiceType.EmailAddress.ToString();
                    }
                    else
                    {
                        lblEmailAddress.Text = string.Empty;
                    }
                    if (objInvoiceType.EDI)
                    {
                        lblEDI.Text = "Enabled";
                    }
                    else
                    {
                        lblEDI.Text = "Disabled";
                    }

                    if (objInvoiceType.BillingSystem.ToString() == "CRM")
                    {
                        csgAdminDiv.Visible = false;
                    }
                    else
                    {
                        csgAdminDiv.Visible = true;
                        if (!string.IsNullOrEmpty(objInvoiceType.ContractNumber))
                        {
                            lblAdminContractNumber.Text = Convert.ToString(objInvoiceType.ContractNumber);
                        }
                        else
                        {
                            lblAdminContractNumber.Text = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(objInvoiceType.GLDepartmentCode))
                        {
                          var glDepartmentCodevalue=  ascendonGLDepartmentCodesList.Where(x=>x.GLDepartmentID==Convert.ToInt32(objInvoiceType.GLDepartmentCode)).Select(x=>x.GLDepartmentValue).FirstOrDefault();                        
                            lblAdminGLDepartmentCode.Text = Convert.ToString(glDepartmentCodevalue);
                        }
                        else
                        {
                            lblAdminGLDepartmentCode.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(objInvoiceType.ContractStartDate)))
                        {
                            lblAdminContractStartDate.Text = Convert.ToString(objInvoiceType.ContractStartDate);
                        }
                        else
                        {
                            lblAdminContractStartDate.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(objInvoiceType.ContractEndDate)))
                        {
                            lblAdminContractEndDate.Text = Convert.ToString(objInvoiceType.ContractEndDate);
                        }
                        else
                        {
                            lblAdminContractEndDate.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(objInvoiceType.IndirectPartnerOrRepCode))
                        {
                            lblAdminIndirectPartnerOrRepCode.Text = Convert.ToString(objInvoiceType.IndirectPartnerOrRepCode);
                        }
                        else
                        {
                            lblAdminIndirectPartnerOrRepCode.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(objInvoiceType.IndirectAgentRegion))
                        {
                            var indirectAgentRegionResultvalue = indirectAgentRegionResult.Where(x => x.GLDepartmentID == Convert.ToInt32(objInvoiceType.IndirectAgentRegion)).Select(x => x.GLDepartmentValue).FirstOrDefault();
                            lblAdminIndirectAgentRegion.Text = Convert.ToString(indirectAgentRegionResultvalue);
                        }
                        else
                        {
                            lblAdminIndirectAgentRegion.Text = string.Empty;
                        }
                    }

                    btnEdit.Visible = true;
                    PopulateAssociatedUsers(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    PopulateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    PopulateAssoicateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    PopulateProfiles(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    PopulateProfileCharges(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value), ddlInvoiceType.SelectedItem.Text);
                    populateManualChangeFileInfo(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    ManualChargeUploadEnable();
                    lblAutoPostBiling.Visible = true;
                    divAssociates.Visible = true;

                    if (lblBillingSystem.Text == "CSG Ascendon") //SERO-1582
                    {
                        accManualUpload.Visible = false;
                        divManualUpload.Visible = false;
                    }
                    else
                    {
                        accManualUpload.Visible = true;
                        divManualUpload.Visible = true;
                    }


                    //8142                    
                    if (Request.Form["div_position"] != null)
                    {
                        //If we dont clear this then other customer profile charges will scroll to last updated position
                        ClearRequestForm();
                    }
                }
                else
                {
                    btnEdit.Visible = false;//change
                }
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while selecting Customer");
            LogException(ex, "ddlInvoiceType_SelectedIndexChanged");
        }
    }

    private void ClearRequestForm()
    {
        System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        isreadonly.SetValue(this.Request.Form, false, null);
        this.Request.Form.Remove("div_position");
    }

    /// <summary>
    /// Opens the PopUp with details populated of Selected InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //foreach (GridViewRow gr in grdInvoiceType.Rows)
            //{
            List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();
            InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
            lblNewInvoiceTypeExeception.Visible = false;
            invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
            MBM.Entities.InvoiceType objInvoiceType = new MBM.Entities.InvoiceType();
            objInvoiceType = invoiceTypeList.Find(x => x.ID == Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString()));

            //Sero-3511 start
            List<MBM.Entities.AscendonGLDepartmentCodes> ascendonGLDepartmentCodesList = new List<MBM.Entities.AscendonGLDepartmentCodes>();
          
            //lblNewInvoiceTypeExeception.Visible = false;
            ascendonGLDepartmentCodesList = objInvoiceBL.GetAscendonGLDepartmentCodes();

            //Sero-3511 End

            #region 3511
           // List<MBM.Entities.AscendonGLDepartmentCodes> ascendonGLDepartmentCodesList = new List<MBM.Entities.AscendonGLDepartmentCodes>();
            //InvoiceBL objInvoiceBL = new InvoiceBL(connectionString);
            //lblNewInvoiceTypeExeception.Visible = false;
            ascendonGLDepartmentCodesList = objInvoiceBL.GetAscendonGLDepartmentCodes();
            //MBM.Entities.AscendonGLDepartmentCodes objAscendonGLDepartmentCodes = new MBM.Entities.AscendonGLDepartmentCodes();
            //objAscendonGLDepartmentCodes = ascendonGLDepartmentCodesList.Select(x => x).ToList() ;
            //var ascendonGLDepartmentCodesListResult = ascendonGLDepartmentCodesList.Select(p => new { DisplayValue = p.GLDepartmentID, DisplayText =p.GLDepartmentValue});
            ddlGLDepartmentCode.DataTextField = "GLDepartmentValue";
            ddlGLDepartmentCode.DataValueField = "GLDepartmentID";
            ddlGLDepartmentCode.DataSource = ascendonGLDepartmentCodesList;
            ddlGLDepartmentCode.DataBind();
            ddlGLDepartmentCode.Items.Insert(0, new ListItem(DEFAULT_GL_Department_Code_TEXT, DEFAULT_INVOICE_VALUE));

            //var indirectAgentRegionResult = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode).ToList().Select(p => new { DisplayValue = p.GLDepartmentID, DisplayText = p.GLDepartmentValue });
            var indirectAgentRegionResult = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode_Indirect_Channel).ToList().Select(p => p);
            ddlIndirectAgentRegion.DataTextField = "GLDepartmentValue";
            ddlIndirectAgentRegion.DataValueField = "GLDepartmentID";
            ddlIndirectAgentRegion.DataSource = indirectAgentRegionResult;
            ddlIndirectAgentRegion.DataBind();
            ddlIndirectAgentRegion.Items.Insert(0, new ListItem(DEFAULT_Indirect_Agent_Region_TEXT, DEFAULT_INVOICE_VALUE));

            txtContractNumber.Text = string.Empty;
            txtContractStartDate.Text = string.Empty;
            txtContractEndDate.Text = string.Empty;
            txtIndirectPartnerOrRepCode.Text = string.Empty;
            ddlBillingSystem.SelectedIndex = 0;
            divGL.Visible = false;     
        


            #endregion

            List<Invoice> invoiceList = new List<Invoice>();
            _billing = new BillingEngine(connectionString);
            int InvoiceType = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());

            invoiceList = _billing.InvoiceBL.FetchInvoicesByType(InvoiceType);

            if (!string.IsNullOrEmpty(objInvoiceType.EmailAddress))
            {
                txtEmailAddress.Text = objInvoiceType.EmailAddress;
            }

            if (objInvoiceType.EDI)
            {
                chkEDINew.Checked = true;
            }
            else
            {
                chkEDINew.Checked = false;
            }

            if (!String.IsNullOrEmpty(objInvoiceType.Prefix))
            {
                txtNewInvoiceType.Text = objInvoiceType.Name;
                txtPrefixCode.Text = objInvoiceType.Prefix;

                if (objInvoiceType.IsAutoPreBilling)
                {
                    chkEnableAutoPreBilling.Checked = true;
                    ddlDaysBeforeBillcycle.SelectedValue = Convert.ToString(objInvoiceType.DaysBeforeBillCycle); ;
                }
                else
                {
                    chkEnableAutoPreBilling.Checked = false;
                    ddlDaysBeforeBillcycle.SelectedValue = "6";
                }

                if (objInvoiceType.IsAutoPostBilling)
                {
                    chkEnableAutoPostBilling.Checked = true;
                    //ddlDaysAfterBillcycle.SelectedValue = Convert.ToString(objInvoiceType.DaysAfterBillCycle);
                }
                else
                {
                    chkEnableAutoPostBilling.Checked = false;
                    //ddlDaysAfterBillcycle.SelectedValue = "2";
                }

                if (invoiceList.Count > 0)
                {
                    txtPrefixCode.Enabled = false;
                }
                else
                {
                    txtPrefixCode.Enabled = true;
                }

                txtDefaultBan.Text = objInvoiceType.BAN;
                txtVendorName.Text = objInvoiceType.VendorName;
                SetCurrencies(objInvoiceType.ImportCurrencyDefault, objInvoiceType.ExportCurrencyDefault);

                txtDefaultftp.Text = objInvoiceType.DefaultFTP;
                ddlOutputFileFormat.SelectedValue = objInvoiceType.OutputFileFormat.ToString();//8967
                ddlIsSOO.SelectedValue = objInvoiceType.IsSOO.ToString();//11609
                ddlBillingSystem.SelectedValue = objInvoiceType.BillingSystem.ToString();//SERO-1582
                ddlautomationFrequency.SelectedValue = objInvoiceType.iAutomationFrequency.ToString();//SERO-1582

                //Sero-3511 start
                if (ddlBillingSystem.SelectedValue=="CRM")
                {
                    divGL.Visible = false;
                }
                else
                {
                    divGL.Visible = true;

                    if (!String.IsNullOrEmpty(objInvoiceType.ContractNumber))
                    {
                        txtContractNumber.Text = objInvoiceType.ContractNumber;
                    }
                    else
                    {
                        txtContractNumber.Text = string.Empty;
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(objInvoiceType.ContractStartDate)))
                    {
                        txtContractStartDate.Text = Convert.ToString(objInvoiceType.ContractStartDate);                        
                    }
                    else
                    {
                        txtContractStartDate.Text = DateTime.Now.ToString();
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(objInvoiceType.ContractEndDate)))
                    {
                        txtContractEndDate.Text = Convert.ToString(objInvoiceType.ContractEndDate); 
                    }
                    else
                    {
                        txtContractEndDate.Text = DateTime.Now.ToString();
                    }

                    if (!String.IsNullOrEmpty(objInvoiceType.IndirectPartnerOrRepCode))
                    {
                        txtIndirectPartnerOrRepCode.Text = objInvoiceType.IndirectPartnerOrRepCode;                       
                    }
                    else
                    {
                        txtIndirectPartnerOrRepCode.Text = string.Empty;
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(objInvoiceType.GLDepartmentCode)))
                    {
                           ddlGLDepartmentCode.DataTextField = "GLDepartmentValue";
                           ddlGLDepartmentCode.DataValueField = "GLDepartmentID";
                           ddlGLDepartmentCode.DataSource = ascendonGLDepartmentCodesList;
                           ddlGLDepartmentCode.DataBind();
                           ddlGLDepartmentCode.Items.Insert(0, new ListItem(DEFAULT_GL_Department_Code_TEXT, DEFAULT_INVOICE_VALUE));
                           ddlGLDepartmentCode.SelectedValue = objInvoiceType.GLDepartmentCode;
                    }
                    else
                    {
                        ddlGLDepartmentCode.DataTextField = "GLDepartmentValue";
                        ddlGLDepartmentCode.DataValueField = "GLDepartmentID";
                        ddlGLDepartmentCode.DataSource = ascendonGLDepartmentCodesList;
                        ddlGLDepartmentCode.DataBind();
                        ddlGLDepartmentCode.Items.Insert(0, new ListItem(DEFAULT_GL_Department_Code_TEXT, DEFAULT_INVOICE_VALUE));
                        ddlGLDepartmentCode.SelectedIndex = 0;
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(objInvoiceType.IndirectAgentRegion)))
                    {
                        var indirectAgentRegionResult1 = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode_Indirect_Channel).ToList().Select(p => p);
                        ddlIndirectAgentRegion.DataTextField = "GLDepartmentValue";
                        ddlIndirectAgentRegion.DataValueField = "GLDepartmentID";
                        ddlIndirectAgentRegion.DataSource = indirectAgentRegionResult1;
                        ddlIndirectAgentRegion.DataBind();
                        ddlIndirectAgentRegion.Items.Insert(0, new ListItem(DEFAULT_Indirect_Agent_Region_TEXT, DEFAULT_INVOICE_VALUE));
                        ddlIndirectAgentRegion.SelectedValue = objInvoiceType.IndirectAgentRegion;   
                      
                    }
                    else
                    {
                        var indirectAgentRegionResult1 = ascendonGLDepartmentCodesList.Where(x => x.GLDepartmentCode != GLDepartmentCode_Indirect_Channel).ToList().Select(p => p);
                        ddlIndirectAgentRegion.DataTextField = "GLDepartmentValue";
                        ddlIndirectAgentRegion.DataValueField = "GLDepartmentID";
                        ddlIndirectAgentRegion.DataSource = indirectAgentRegionResult1;
                        ddlIndirectAgentRegion.DataBind();
                        ddlIndirectAgentRegion.Items.Insert(0, new ListItem(DEFAULT_Indirect_Agent_Region_TEXT, DEFAULT_INVOICE_VALUE));
                        ddlIndirectAgentRegion.SelectedIndex = 0;
                    }
                }

                //Sero-3511 End


                if (!string.IsNullOrEmpty(objInvoiceType.DefaultFTP))
                {
                    chkFTPCheckBox.Checked = true;
                    txtDefaultftp.Enabled = true;
                    txtFTPUsername.Enabled = true;
                    txtFTPPassword.Enabled = true;
                    txtFTPUsername.Text = objInvoiceType.FTPUserName;

                    DataEncryptor objEncrypt = new DataEncryptor();
                    string strPassword = objEncrypt.Decrypt(objInvoiceType.FTPPassword);
                    //txtFTPPassword.Text = strPassword;
                    txtFTPPassword.Attributes["value"] = strPassword;
                    //txtFTPPassword.Text = "tests";
                    // txtFTPPassword.Text = "********";
                }
                else
                {
                    chkFTPCheckBox.Checked = false;
                    txtDefaultftp.Enabled = false;
                    txtFTPUsername.Enabled = false;
                    txtFTPPassword.Enabled = false;

                    txtDefaultftp.Text = string.Empty;
                    txtFTPUsername.Text = string.Empty;
                    txtFTPPassword.Text = string.Empty;
                }

            }

            //}            
            //Controls_ErrorSuccessNotifier.AddSuccessMessage("updated Customer successfully");
            mpeNewInvoiceType.Show();
            lblheaderInvoiceType.Text = "Update Customer";
            btnCreate.Text = "Update";
           
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while updating Customer");
            LogException(ex, "btnEdit_Click");
        }
    }

    /// <summary>
    /// Associates FileTypes for InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddFileType_Click(object sender, EventArgs e)
    {
        FileTypeBL objFileTypes = new FileTypeBL(connectionString);
        FileTypes objFileType = new FileTypes();

        objFileType.InvoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
        objFileType.FileTypeId = Convert.ToInt32(ddlFileType.SelectedItem.Value);
        objFileType.CreatedBy = GetUserName();
        try
        {
            int result = objFileTypes.InsertAssocoiatedFileType(objFileType);
            if (result >= 0)
            {
                Controls_ErrorSuccessNotifier.AddSuccessMessage("Assocaiated FileType for " + ddlInvoiceType.SelectedItem.Text + " added successfully");
                PopulateAssoicateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                PopulateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));

            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Associating FileType");
            LogException(ex, "btnAddFileType_Click");
        }
    }

    /// <summary>
    /// Deletes the Selected Associated FileType for respective InvoiceType 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdFileType_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        FileTypeBL objFileTypes = new FileTypeBL(connectionString);
        try
        {
            string AssociatedFileId = grdFileType.DataKeys[e.RowIndex].Value.ToString();
            if (!string.IsNullOrWhiteSpace(AssociatedFileId))
            {
                objFileTypes.DeleteAssociatedFileType(Convert.ToInt32(AssociatedFileId));
                PopulateAssoicateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                PopulateFileTypes(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                Controls_ErrorSuccessNotifier.AddSuccessMessage("De-Associated FileType for " + ddlInvoiceType.SelectedItem.Text + " successful");
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while de-Associating FileType");
            LogException(ex, "grdFileType_OnRowDeleting");
        }
    }

    /// <summary>
    /// Deletes the Selected Associated Users for respective InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdAssociateUsers_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        UserBL objUser = new UserBL(connectionString);
        try
        {
            string associatedUserId = grdAssociateUsers.DataKeys[e.RowIndex].Value.ToString();
            if (!string.IsNullOrWhiteSpace(associatedUserId))
            {
                objUser.DeleteAssociatedUser(Convert.ToInt32(associatedUserId));
                PopulateAssociatedUsers(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                Controls_ErrorSuccessNotifier.AddSuccessMessage("De-Associated User for " + ddlInvoiceType.SelectedItem.Text + " successful");
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while de-Associating User");
            LogException(ex, "grdAssociateUsers_OnRowDeleting");
        }
    }

    //TODO
    protected void grdAssociatedUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //If files are not processed disble the process log   
            if (((e.Row.DataItem as Users).UserRole == UserRoleType.SystemAdministrator))
            {
                ((Button)(e.Row.FindControl("btnDeleteUser"))).Enabled = false;

            }
        }
    }

    /// <summary>
    /// Performs Search for Users based on inputed Firstname or Lastname or Corpnet Id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchUser_Click(object sender, EventArgs e)
    {
        lblSearchResultNull.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtSearchUser.Text))
            {
                UserBL objUser = new UserBL(connectionString);
                List<Users> users = objUser.SearchUser(txtSearchUser.Text);
                if (users.Count > 0)
                {
                    btnAddUser.Visible = true;
                    grdSearchUserResult.DataSource = users;
                    grdSearchUserResult.DataBind();

                }
                else
                {
                    grdSearchUserResult.DataSource = null;
                    grdSearchUserResult.DataBind();
                    lblSearchResultNull.Text = "No such user found with keyword '<b>" + txtSearchUser.Text + "</b>'";
                    lblSearchResultNull.Visible = true;
                    btnAddUser.Visible = false;
                }
                lblAssociatedUserMessage.Text = "";
                lblAssociatedUserMessage.Visible = false;
                mdlSearchUserResult.Show();
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while searching User");
            LogException(ex, "btnSearchUser_Click");
        }
    }

    /// <summary>
    /// Assocaites Users to Selected InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        UserBL objUser = new UserBL(connectionString);
        try
        {
            string strUserId = string.Empty;
            List<string> lstUsers = new List<string>();

            for (int i = 0; i < grdSearchUserResult.Rows.Count; i++)
            {
                RadioButton rb = (grdSearchUserResult.Rows[i].FindControl("rbtnSelectedUser")) as RadioButton;
                if (rb.Checked == true)
                {
                    strUserId = (grdSearchUserResult.Rows[i].FindControl("lblUserId") as Label).Text;
                }

                lstUsers.Add(strUserId);
                rb.Checked = false;
            }

            int intInvoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);

            string strInsertedBy = GetUserName();

            if (!string.IsNullOrEmpty(strUserId))
            {
                int result = -1;
                List<Users> lstAssociatedUsers = new List<Users>();
                int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
                lstAssociatedUsers = objUser.GetUserDetailsByInvoiceType(invoiceTypeId);
                Users newUser = lstAssociatedUsers.Where(m => m.UserId == strUserId).FirstOrDefault();
                if (newUser != null)
                {
                    mdlSearchUserResult.Show();
                    lblAssociatedUserMessage.Text = strUserId + " User already associated to invoice type";
                    lblAssociatedUserMessage.Visible = true;
                }
                else
                {
                    result = objUser.InsertAssociatedUser(intInvoiceTypeId, strUserId, strInsertedBy);
                    lblAssociatedUserMessage.Visible = false;
                }

                if (result >= 0)
                {
                    PopulateAssociatedUsers(intInvoiceTypeId);
                    txtSearchUser.Text = string.Empty;
                }
            }

            txtSearchUser.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Associating User");
            LogException(ex, "btnAddUser_Click");
        }
    }

    /// <summary>
    /// Deletes Selected Assocaited Accounts for respective InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdAssociateAccounts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (lblBillingSystem.Text == "CRM")
            {
                CRMAccountBL objCRMAccount = new CRMAccountBL(connectionString);
            string accountnumber = grdAssociateAccounts.DataKeys[e.RowIndex].Value.ToString();
            if (!string.IsNullOrWhiteSpace(accountnumber))
            {
                    objCRMAccount.DeleteAssociatedAccount(Convert.ToInt64(accountnumber));
                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
            }
        }
            else
            {
                CSGAccountBL objCSGAccount = new CSGAccountBL(connectionString);
                string accountnumber = grdAssociateAccounts.DataKeys[e.RowIndex].Value.ToString();
                if (!string.IsNullOrWhiteSpace(accountnumber))
                {
                    objCSGAccount.DeleteAssociatedAccount_CSG(Convert.ToInt64(accountnumber));
                    PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                }
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while deleting Associated Accounts");
            LogException(ex, "grdAssociateAccounts_RowDeleting");
        }
    }

    /// <summary>
    /// Opens the Edit PopUp for Associate Accounts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdAssociateAccounts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdAssociateAccounts.EditIndex = e.NewEditIndex;
            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
            //Account Edit Pop-up logic
            Label lblAccountNumber = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblAccount") as Label;
            Label lblAccountName = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblAccountName") as Label;
            Label lblChildPays = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblChildPays") as Label;
            Label lblParent = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblParent") as Label;
            Label lblParentAccountName = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblParentAccountName") as Label;
            Label lblStringIdentifier = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblStringIdentifier") as Label;
            Label lblCreateAccount = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblCreateAccount") as Label;
            Label lblBillCycle = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblBillCycle") as Label;
            Label lblDateIdentifier = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblDateIdentifier") as Label;  //ER8668
            Label lblUserName = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblUserName") as Label;
            Label lblPassword = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblPassword") as Label;
            Label lblFTP = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblFTP") as Label;
            Label lblEmail = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblEmailId") as Label;
            Label lblEDI = grdAssociateAccounts.Rows[e.NewEditIndex].FindControl("lblEDI") as Label;

            lblEditCrmAccountNumberDisplay.Text = lblAccountNumber.Text;
            lblEditAccountNameDisplay.Text = lblAccountName.Text;
            lblEditParentAccountNumberDisplay.Text = lblParent.Text;
            lblEditParentAccountNameDisplay.Text = lblParentAccountName.Text;
            lblEditBillCycleDisplay.Text = lblBillCycle.Text;
            txtEditStringIdentifier.Text = lblStringIdentifier.Text;
            chkChildPays.Checked = Convert.ToBoolean(lblChildPays.Text);
            lblEditUserName.Text = string.IsNullOrEmpty(lblUserName.Text) ? string.Empty : lblPassword.Text;
            txtPassword.Text = string.IsNullOrEmpty(lblPassword.Text) ? string.Empty : lblUserName.Text;
            txtEmail.Text = string.IsNullOrEmpty(lblEmail.Text) ? string.Empty : lblEmail.Text;
            chkEDI.Checked = Convert.ToBoolean(lblEDI.Text);
            txtFTP.Text = string.IsNullOrEmpty(lblFTP.Text) ? string.Empty : lblFTP.Text;

            if (chkChildPays.Checked)
            {
                lblEditUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtEmail.Enabled = true;
                chkEDI.Enabled = true;
                txtFTP.Enabled = true;

            }
            else
            {
                lblEditUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtEmail.Enabled = false;
                chkEDI.Enabled = false;
                txtFTP.Enabled = false;
            }

            chkEditCreateCRMAccount.Checked = Convert.ToBoolean(lblCreateAccount.Text);
            txtEditDateIdentifier.Text = lblDateIdentifier.Text == string.Empty ? string.Empty : Convert.ToDateTime(lblDateIdentifier.Text).ToString("MM-dd-yyyy");

            int invoiceId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
            AccountEffectiveDateStatus(invoiceId, lblAccountNumber.Text);
            mpeEditAccount.Show();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while loading Associated Accounts edit popup");
            LogException(ex, "grdAssociateAccounts_RowEditing");
        }
    }

    /// <summary>
    /// Opens PopUp to Assocaite CRM Account to Selected InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddAccount_Click(object sender, EventArgs e)
    {
        try
        {
            txtCrmAccountNumber.Text = string.Empty;
            lblCRMAccountNameDisplay.Text = string.Empty;
            lblParentAccountNumberDisplay.Text = string.Empty;
            lblParentAccountNameDisplay.Text = string.Empty;
            txtStringIdentifier.Text = string.Empty;
            txtStringIdentifier.Enabled = false;
            chkCrmAccount.Checked = false;
            chkCrmAccount.Enabled = false;
            btnAddCRMAccount.Visible = false;
            lblAddAccountErrorMessage.Text = string.Empty;
            lblAddAccountErrorMessage.Visible = false;
            lblBillCycleDisplay.Text = string.Empty;
            txtDateIdentifier.Text = string.Empty;
            txtDateIdentifier.Enabled = false;
            checkChildPays.Enabled = false;
            checkChildPays.Checked = false;
            textUserName.Enabled = false;
            textUserName.Text = string.Empty;
            textPassword.Enabled = false;
            textPassword.Text = string.Empty;
            textEmail.Enabled = false;
            textEmail.Text = string.Empty;
            textFTP.Enabled = false;
            textFTP.Text = string.Empty;
            checkEDI.Enabled = false;
            checkEDI.Checked = false;

            mpeAddAccount.Show();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while loading Associate Accounts popup");
            LogException(ex, "btnAddAccount_Click");
        }
    }

    /// <summary>
    /// Assocaites CRM Account to Selected InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void btnAddCRMAccount_Click(object sender, EventArgs e)
    {
        string UserName = GetUserName();
        CRMAccount objCRMAccount = new CRMAccount();

        try
        {
            if (lblBillingSystem.Text == "CRM")
            {
                #region Input

                if (!String.IsNullOrEmpty(txtCrmAccountNumber.Text))
                {
                    objCRMAccount.AccountNumber = Convert.ToInt64(txtCrmAccountNumber.Text);
                }

                if (!String.IsNullOrEmpty(lblCRMAccountNameDisplay.Text))
                {
                    objCRMAccount.AccountName = lblCRMAccountNameDisplay.Text;
                }

                if (!String.IsNullOrEmpty(lblParentAccountNumberDisplay.Text))
                {
                    objCRMAccount.ParentAccountNumber = Convert.ToInt32(lblParentAccountNumberDisplay.Text);
                }

                if (!String.IsNullOrEmpty(lblParentAccountNameDisplay.Text))
                {
                    objCRMAccount.ParentAccountName = lblParentAccountNameDisplay.Text;
                }

                if (!String.IsNullOrEmpty(txtStringIdentifier.Text))
                {
                    objCRMAccount.StringIdentifier = txtStringIdentifier.Text.Trim();
                }

                if (!string.IsNullOrEmpty(lblBillCycleDisplay.Text))
                {
                    objCRMAccount.AccountBillCycle = Convert.ToInt32(lblBillCycleDisplay.Text);
                }

                if (!String.IsNullOrEmpty(txtDateIdentifier.Text))
                {
                    objCRMAccount.EffectiveBillDate = Convert.ToDateTime(txtDateIdentifier.Text);
                }

                if (chkCrmAccount.Checked == true)
                {
                    objCRMAccount.CreateAccount = true;
                }
                else
                {
                    objCRMAccount.CreateAccount = false;
                }
                objCRMAccount.InvoiceTypeid = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);

                if (checkChildPays.Checked == true)
                {
                    objCRMAccount.ChildPays = true;
                }
                else
                {
                    objCRMAccount.ChildPays = false;
                }

                if (!String.IsNullOrEmpty(textUserName.Text))
                {
                    objCRMAccount.UserName = textUserName.Text;
                }

                if (!String.IsNullOrEmpty(textPassword.Text))
                {
                    objCRMAccount.Password = textPassword.Text;
                }

                if (!String.IsNullOrEmpty(textFTP.Text))
                {
                    objCRMAccount.FTP = textFTP.Text;
                }

                if (!String.IsNullOrEmpty(textEmail.Text))
                {
                    objCRMAccount.EmailId = textEmail.Text;
                }

                if (checkEDI.Checked == true)
                {
                    objCRMAccount.EDI = true;
                }
                else
                {
                    objCRMAccount.EDI = false;
                }
                #endregion Input
                #region mainLogic
                if (objCRMAccount != null)
                {
                    if (objCRMAccount.AccountNumber > 0 && objCRMAccount.AccountName != null && objCRMAccount.InvoiceTypeid > 0)
                    {
                        CRMAccountBL objCRMAccountBL = new CRMAccountBL(connectionString);
                        List<CRMAccount> lstCRMAccount = new List<CRMAccount>();
                        int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
                        lstCRMAccount = objCRMAccountBL.GetAllAssoicateAccounts();

                        //To Validate Effective Billing Date
                        string response = ValidateEffectiveBillDate(objCRMAccount.InvoiceTypeid, objCRMAccount.AccountNumber.ToString(), objCRMAccount.EffectiveBillDate);

                        if (response != null)
                        {
                            if (response.Contains("Success"))
                            {
                                lblAddAccountErrorMessage.Visible = false;

                                //Condition to check duplicate CRM Account number.
                                CRMAccount crmAccount = lstCRMAccount.Find(m => m.AccountNumber == objCRMAccount.AccountNumber);
                                if (crmAccount != null)
                                {
                                    mpeAddAccount.Show();
                                    lblAddAccountErrorMessage.Text = objCRMAccount.AccountNumber + " already associated to " + crmAccount.InvoiceTypeName + " (" + crmAccount.Prefix + ")";
                                    lblAddAccountErrorMessage.Visible = true;
                                }
                                else
                                {
                                    //Condition to check duplicate string identifier.
                                    CRMAccount crmStringIdentifierFilter = lstCRMAccount.Find(m => m.StringIdentifier == objCRMAccount.StringIdentifier);

                                    CRMAccountBL objAccount = new CRMAccountBL(connectionString);
                                    List<CRMAccount> lstCRMAccountsPerCustomer = new List<CRMAccount>();

                                    lstCRMAccountsPerCustomer = objAccount.GetAssoicateAccountsByInvoiceTypeId(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));

                                    if (crmStringIdentifierFilter != null)
                                    {
                                        if (!string.IsNullOrEmpty(crmStringIdentifierFilter.StringIdentifier))
                                        {
                                            mpeAddAccount.Show();
                                            lblAddAccountErrorMessage.Text = objCRMAccount.StringIdentifier + " is already associated to CRM A/C# " + crmStringIdentifierFilter.AccountNumber + " (" + crmStringIdentifierFilter.Prefix + ")";
                                            lblAddAccountErrorMessage.Visible = true;
                                        }
                                        else
                                        {
                                            if (lstCRMAccountsPerCustomer.Count > 0)
                                            {
                                                bool blIsBillCycleSame = false;
                                                foreach (CRMAccount account in lstCRMAccountsPerCustomer)
                                                {
                                                    if (account.AccountBillCycle == objCRMAccount.AccountBillCycle)
                                                    {
                                                        blIsBillCycleSame = true;
                                                        break;
                                                    }
                                                }

                                                if (blIsBillCycleSame)
                                                {
                                                    int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
                                                    PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
                                                    lblAddAccountErrorMessage.Visible = false;
                                                }
                                                else
                                                {
                                                    mpeAddAccount.Show();
                                                    lblAddAccountErrorMessage.Text = "All CRM Accounts under a customer must have the same Bill Cycle";
                                                    lblAddAccountErrorMessage.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
                                                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                                Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
                                                lblAddAccountErrorMessage.Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (lstCRMAccountsPerCustomer.Count > 0)
                                        {
                                            bool blIsBillCycleSame = false;
                                            foreach (CRMAccount account in lstCRMAccountsPerCustomer)
                                            {
                                                if (account.AccountBillCycle == objCRMAccount.AccountBillCycle)
                                                {
                                                    blIsBillCycleSame = true;
                                                    break;
                                                }
                                            }

                                            if (blIsBillCycleSame)
                                            {
                                                int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
                                                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                                Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
                                                lblAddAccountErrorMessage.Visible = false;
                                            }
                                            else
                                            {
                                                mpeAddAccount.Show();
                                                lblAddAccountErrorMessage.Text = "All CRM Accounts under a customer must have the same Bill Cycle";
                                                lblAddAccountErrorMessage.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
                                            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                            Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
                                            lblAddAccountErrorMessage.Visible = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                mpeAddAccount.Show();
                                lblAddAccountErrorMessage.Text = response;
                                lblAddAccountErrorMessage.Visible = true;
                            }
                        }
                    }
                }
                #endregion mainLogic
            }
            else
            {
                #region CSGmainLogic
                CSGAccount objCSGAccount = new CSGAccount();
                objCSGAccount = GetCSGUserInputFromUI();
                if (objCSGAccount != null)
                {
                    if (objCSGAccount.AccountNumber > 0 && objCSGAccount.InvoiceTypeid > 0)
                    {
                        CSGAccountBL objCSGAccountBL = new CSGAccountBL(connectionString);
                        List<CSGAccount> lstCCGAccount = new List<CSGAccount>();
                        int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
                        var Isvalidate = objCSGAccountBL.GetValidateCSGAssoicateAccounts(objCSGAccount.AccountNumber, objCSGAccount.StringIdentifier, 1);

                        if (Isvalidate == Convert.ToInt32(CSGValidationCode.DuplicationOfAccountNumber))
                        {
                            mpeAddAccount.Show();
                            lblAddAccountErrorMessage.Text = " Account number already available in MBM";
                            lblAddAccountErrorMessage.Visible = true;
                        }
                        if (Isvalidate == Convert.ToInt32(CSGValidationCode.DuplicationOfStringIdentifier))
                        {
                            mpeAddAccount.Show();
                            lblAddAccountErrorMessage.Text = "Provisioning Identifier : " + objCSGAccount.StringIdentifier + " is already associated to biller account";
                            lblAddAccountErrorMessage.Visible = true;
                        }
                        if (Isvalidate == Convert.ToInt32(CSGValidationCode.Success))
                        {
                            CSGAccountBL objAccount = new CSGAccountBL(connectionString);
                            List<CSGAccount> lstCRMAccountsPerCustomer = new List<CSGAccount>();
                            int result = objAccount.InsertCSGAssociatedAccount(objCSGAccount, UserName);
                            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                            Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
                            lblAddAccountErrorMessage.Visible = false;
                        }
                    }
                }
                #endregion CSGmainLogic
            }
            //Controls_ErrorSuccessNotifier.AddSuccessMessage("updated associating Account to Customer successfully");
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while associating Account to Customer");
            LogException(ex, "btnAddCRMAccount_Click");
        }
    }

//    protected void btnAddCRMAccount_Click(object sender, EventArgs e)
//    {
//        string UserName = GetUserName();
//        CRMAccount objCRMAccount = new CRMAccount();

//        try
//        {
//            if (lblBillingSystem.Text == "CRM")
//            {
//                #region Input

//            if (!String.IsNullOrEmpty(txtCrmAccountNumber.Text))
//            {
//                    objCRMAccount.AccountNumber = Convert.ToInt64(txtCrmAccountNumber.Text);
//            }

//            if (!String.IsNullOrEmpty(lblCRMAccountNameDisplay.Text))
//            {
//                objCRMAccount.AccountName = lblCRMAccountNameDisplay.Text;
//            }

//            if (!String.IsNullOrEmpty(lblParentAccountNumberDisplay.Text))
//            {
//                objCRMAccount.ParentAccountNumber = Convert.ToInt32(lblParentAccountNumberDisplay.Text);
//            }

//            if (!String.IsNullOrEmpty(lblParentAccountNameDisplay.Text))
//            {
//                objCRMAccount.ParentAccountName = lblParentAccountNameDisplay.Text;
//            }

//            if (!String.IsNullOrEmpty(txtStringIdentifier.Text))
//            {
//                objCRMAccount.StringIdentifier = txtStringIdentifier.Text.Trim();
//            }

//            if (!string.IsNullOrEmpty(lblBillCycleDisplay.Text))
//            {
//                objCRMAccount.AccountBillCycle = Convert.ToInt32(lblBillCycleDisplay.Text);
//            }

//            if (!String.IsNullOrEmpty(txtDateIdentifier.Text))
//            {
//                objCRMAccount.EffectiveBillDate = Convert.ToDateTime(txtDateIdentifier.Text);
//            }

//            if (chkCrmAccount.Checked == true)
//            {
//                objCRMAccount.CreateAccount = true;
//            }
//            else
//            {
//                objCRMAccount.CreateAccount = false;
//            }
//            objCRMAccount.InvoiceTypeid = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);

//            if (checkChildPays.Checked == true)
//            {
//                objCRMAccount.ChildPays = true;
//            }
//            else
//            {
//                objCRMAccount.ChildPays = false;
//            }

//            if (!String.IsNullOrEmpty(textUserName.Text))
//            {
//                objCRMAccount.UserName = textUserName.Text;
//            }

//            if (!String.IsNullOrEmpty(textPassword.Text))
//            {
//                objCRMAccount.Password = textPassword.Text;
//            }

//            if (!String.IsNullOrEmpty(textFTP.Text))
//            {
//                objCRMAccount.FTP = textFTP.Text;
//            }

//            if (!String.IsNullOrEmpty(textEmail.Text))
//            {
//                objCRMAccount.EmailId = textEmail.Text;
//            }

//            if (checkEDI.Checked == true)
//            {
//                objCRMAccount.EDI = true;
//            }
//            else
//            {
//                objCRMAccount.EDI = false;
//            }
//                #endregion Input
//                #region mainLogic
//            if (objCRMAccount != null)
//            {
//                if (objCRMAccount.AccountNumber > 0 && objCRMAccount.AccountName != null && objCRMAccount.InvoiceTypeid > 0)
//                {
//                    CRMAccountBL objCRMAccountBL = new CRMAccountBL(connectionString);
//                    List<CRMAccount> lstCRMAccount = new List<CRMAccount>();
//                    int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
//                    lstCRMAccount = objCRMAccountBL.GetAllAssoicateAccounts();

//                    //To Validate Effective Billing Date
//                    string response = ValidateEffectiveBillDate(objCRMAccount.InvoiceTypeid, objCRMAccount.AccountNumber.ToString(), objCRMAccount.EffectiveBillDate);

//                    if (response != null)
//                    {
//                        if (response.Contains("Success"))
//                        {
//                            lblAddAccountErrorMessage.Visible = false;

//                                //Condition to check duplicate CRM Account number.
//                                CRMAccount crmAccount = lstCRMAccount.Find(m => m.AccountNumber == objCRMAccount.AccountNumber);
//                                if (crmAccount != null)
//                                {
//                                    mpeAddAccount.Show();
//                                    lblAddAccountErrorMessage.Text = objCRMAccount.AccountNumber + " already associated to " + crmAccount.InvoiceTypeName + " (" + crmAccount.Prefix + ")";
//                                    lblAddAccountErrorMessage.Visible = true;
//                                }
//                                else
//                                {
//                                    //Condition to check duplicate string identifier.
//                                    CRMAccount crmStringIdentifierFilter = lstCRMAccount.Find(m => m.StringIdentifier == objCRMAccount.StringIdentifier);

//                                    CRMAccountBL objAccount = new CRMAccountBL(connectionString);
//                                    List<CRMAccount> lstCRMAccountsPerCustomer = new List<CRMAccount>();

//                                    lstCRMAccountsPerCustomer = objAccount.GetAssoicateAccountsByInvoiceTypeId(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));

//                                    if (crmStringIdentifierFilter != null)
//                                    {
//                                        if (!string.IsNullOrEmpty(crmStringIdentifierFilter.StringIdentifier))
//                                        {
//                                            mpeAddAccount.Show();
//                                            lblAddAccountErrorMessage.Text = objCRMAccount.StringIdentifier + " is already associated to CRM A/C# " + crmStringIdentifierFilter.AccountNumber + " (" + crmStringIdentifierFilter.Prefix + ")";
//                                            lblAddAccountErrorMessage.Visible = true;
//                                        }
//                                        else
//                                        {
//                                            if (lstCRMAccountsPerCustomer.Count > 0)
//                                            {
//                                                bool blIsBillCycleSame = false;
//                                                foreach (CRMAccount account in lstCRMAccountsPerCustomer)
//                                                {
//                                                    if (account.AccountBillCycle == objCRMAccount.AccountBillCycle)
//                                                    {
//                                                        blIsBillCycleSame = true;
//                                                        break;
//                                                    }
//                                                }

//                                                if (blIsBillCycleSame)
//                                                {
//                                                    int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                                    PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                                    Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                                    lblAddAccountErrorMessage.Visible = false;
//                                                }
//                                                else
//                                                {
//                                                    mpeAddAccount.Show();
//                                                    lblAddAccountErrorMessage.Text = "All CRM Accounts under a customer must have the same Bill Cycle";
//                                                    lblAddAccountErrorMessage.Visible = true;
//                                                }
//                                            }
//                                            else
//                                            {
//                                                int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                                Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                                lblAddAccountErrorMessage.Visible = false;
//                                            }
//                                        }
//                                    }
//                                    else
//                                    {
//                                        if (lstCRMAccountsPerCustomer.Count > 0)
//                                        {
//                                            bool blIsBillCycleSame = false;
//                                            foreach (CRMAccount account in lstCRMAccountsPerCustomer)
//                                            {
//                                                if (account.AccountBillCycle == objCRMAccount.AccountBillCycle)
//                                                {
//                                                    blIsBillCycleSame = true;
//                                                    break;
//                                                }
//                                            }

//                                            if (blIsBillCycleSame)
//                                            {
//                                                int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                                Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                                lblAddAccountErrorMessage.Visible = false;
//                                            }
//                                            else
//                                            {
//                                                mpeAddAccount.Show();
//                                                lblAddAccountErrorMessage.Text = "All CRM Accounts under a customer must have the same Bill Cycle";
//                                                lblAddAccountErrorMessage.Visible = true;
//                                            }
//                                        }
//                                        else
//                                        {
//                                            int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                            Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                            lblAddAccountErrorMessage.Visible = false;
//                                        }
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                mpeAddAccount.Show();
//                                lblAddAccountErrorMessage.Text = response;
//                                lblAddAccountErrorMessage.Visible = true;
//                            }
//                        }
//                    }
//                }
//                #endregion mainLogic
//            }
//            else
//            {
//                #region CSGmainLogic
//                CSGAccount objCSGAccount = new CSGAccount();
//                objCSGAccount = GetCSGUserInputFromUI();
//                if (objCSGAccount != null)
//                {
//                    if (objCSGAccount.AccountNumber > 0 && objCSGAccount.InvoiceTypeid > 0)
//                    {
//                        CSGAccountBL objCSGAccountBL = new CSGAccountBL(connectionString);
//                        List<CSGAccount> lstCCGAccount = new List<CSGAccount>();
//                        int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue.ToString());
//                        var Isvalidate = objCSGAccountBL.GetValidateCSGAssoicateAccounts(objCSGAccount.AccountNumber, objCSGAccount.StringIdentifier,1);
//                            //Condition to check duplicate CRM Account number.
//                            CRMAccount crmAccount = lstCRMAccount.Find(m => m.AccountNumber == objCRMAccount.AccountNumber);
//                            if (crmAccount != null)
//                            {
//                                mpeAddAccount.Show();
//                                lblAddAccountErrorMessage.Text = objCRMAccount.AccountNumber + " already associated to " + crmAccount.InvoiceTypeName + " (" + crmAccount.Prefix + ")";
//                                lblAddAccountErrorMessage.Visible = true;
//                            }
//                            else
//                            {
//                                //Condition to check duplicate string identifier.
//                                CRMAccount crmStringIdentifierFilter = lstCRMAccount.Find(m => m.StringIdentifier == objCRMAccount.StringIdentifier);

//                                CRMAccountBL objAccount = new CRMAccountBL(connectionString);
//                                List<CRMAccount> lstCRMAccountsPerCustomer = new List<CRMAccount>();

//                                lstCRMAccountsPerCustomer = objAccount.GetAssoicateAccountsByInvoiceTypeId(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));

//                                if (crmStringIdentifierFilter != null)
//                                {
//                                    if (!string.IsNullOrEmpty(crmStringIdentifierFilter.StringIdentifier))
//                                    {
//                                        mpeAddAccount.Show();
//                                        lblAddAccountErrorMessage.Text = objCRMAccount.StringIdentifier + " is already associated to CRM A/C# " + crmStringIdentifierFilter.AccountNumber + " (" + crmStringIdentifierFilter.Prefix + ")";
//                                        lblAddAccountErrorMessage.Visible = true;
//                                    }
//                                    else
//                                    {
//                                        if (lstCRMAccountsPerCustomer.Count > 0)
//                                        {
//                                            bool blIsBillCycleSame = false;
//                                            foreach (CRMAccount account in lstCRMAccountsPerCustomer)
//                                            {
//                                                if (account.AccountBillCycle == objCRMAccount.AccountBillCycle)
//                                                {
//                                                    blIsBillCycleSame = true;
//                                                    break;
//                                                }
//                                            }

//                                            if (blIsBillCycleSame)
//                                            {
//                                                int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                                Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                                lblAddAccountErrorMessage.Visible = false;
//                                            }
//                                            else
//                                            {
//                                                mpeAddAccount.Show();
//                                                lblAddAccountErrorMessage.Text = "All CRM Accounts under a customer must have the same Bill Cycle";
//                                                lblAddAccountErrorMessage.Visible = true;
//                                            }
//                                        }
//                                        else
//                                        {
//                                            int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                            Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                            lblAddAccountErrorMessage.Visible = false;
//                                        }
//                                    }
//                                }
//                                else
//                                {
//                                    if (lstCRMAccountsPerCustomer.Count > 0)
//                                    {
//                                        bool blIsBillCycleSame = false;
//                                        foreach (CRMAccount account in lstCRMAccountsPerCustomer)
//                                        {
//                                            if (account.AccountBillCycle == objCRMAccount.AccountBillCycle)
//                                            {
//                                                blIsBillCycleSame = true;
//                                                break;
//                                            }
//                                        }

//                                        if (blIsBillCycleSame)
//                                        {
//                                            int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                            Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                            lblAddAccountErrorMessage.Visible = false;
//                                        }
//                                        else
//                                        {
//                                            mpeAddAccount.Show();
//                                            lblAddAccountErrorMessage.Text = "All CRM Accounts under a customer must have the same Bill Cycle";
//                                            lblAddAccountErrorMessage.Visible = true;
//                                        }
//                                    }
//                                    else
//                                    {
//                                        int result = objAccount.InsertAssociatedAccount(objCRMAccount, UserName);
//                                        PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                                        Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                                        lblAddAccountErrorMessage.Visible = false;
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                            mpeAddAccount.Show();
//                            lblAddAccountErrorMessage.Text = response;
//                            lblAddAccountErrorMessage.Visible = true;
//                        }
//                    }
//                }
//            }

//                        if (Isvalidate ==Convert.ToInt32(CSGValidationCode.DuplicationOfAccountNumber))
//                        {
//                            mpeAddAccount.Show();                           
//                            lblAddAccountErrorMessage.Text = " Account number already available in MBM";
//                            lblAddAccountErrorMessage.Visible = true;
//        }
//                        if (Isvalidate == Convert.ToInt32(CSGValidationCode.DuplicationOfStringIdentifier))
//                        {
//                            mpeAddAccount.Show();
//                            lblAddAccountErrorMessage.Text = "Provisioning Identifier : " + objCSGAccount.StringIdentifier + " is already associated to biller account";
//                            lblAddAccountErrorMessage.Visible = true;
//                        }
//                        if (Isvalidate == Convert.ToInt32(CSGValidationCode.Success))
//                        {
//                            CSGAccountBL objAccount = new CSGAccountBL(connectionString);
//                            List<CSGAccount> lstCRMAccountsPerCustomer = new List<CSGAccount>();
//                            int result = objAccount.InsertCSGAssociatedAccount(objCSGAccount, UserName);
//                            PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
//                            Controls_ErrorSuccessNotifier.AddSuccessMessage("Account: " + objCRMAccount.AccountName + " successfully associated to Customer");
//                            lblAddAccountErrorMessage.Visible = false;
//                        }
//                    }
//                }
//                #endregion CSGmainLogic
//            }
//            //Controls_ErrorSuccessNotifier.AddSuccessMessage("updated associating Account to Customer successfully");

//        catch (Exception ex)
//        {
//            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while associating Account to Customer");
//            LogException(ex, "btnAddCRMAccount_Click");
//        }
//    }
//}

    /// <summary>
    /// Validates CRM Account
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnValidate_Click(object sender, EventArgs e)
    {

        //CBE-1582
        string ParentFirstName;
        string ParentLastName;
        int ParentId;
        //CBE-1582



        if (lblBillingSystem.Text == "CRM") //CBE-1582
        { //CBE-1582

        CRMAccountBL objAccount = new CRMAccountBL(connectionString);
        CRMAccount objCRMAccount;
        lblEditAccountErrorMessage.Visible = false;
        lblAddAccountErrorMessage.Visible = false;


        try
        {
            objCRMAccount = objAccount.ValidateCRMAccount(txtCrmAccountNumber.Text);

            if (objCRMAccount != null)
            {
                if (objCRMAccount.AccountNumber > 0)
                {
                    lblAddAccountErrorMessage.Visible = false;
                    txtStringIdentifier.Enabled = true;
                    txtDateIdentifier.Enabled = true;
                    chkCrmAccount.Enabled = true;
                    btnAddCRMAccount.Visible = true;
                    checkChildPays.Enabled = true;

                    txtCrmAccountNumber.Text = txtCrmAccountNumber.Text;
                    lblParentAccountNameDisplay.Text = objCRMAccount.ParentAccountName;
                    lblParentAccountNumberDisplay.Text = objCRMAccount.ParentAccountNumber > 0 ? objCRMAccount.ParentAccountNumber.ToString() : string.Empty;
                    lblCRMAccountNameDisplay.Text = objCRMAccount.AccountName;
                    lblBillCycleDisplay.Text = objCRMAccount.AccountBillCycle.ToString();
                }
                else
                {
                    btnAddCRMAccount.Visible = false;
                    lblAddAccountErrorMessage.Visible = true;
                    lblAddAccountErrorMessage.Text = "No such CRM Account found.";
                    lblParentAccountNameDisplay.Text = string.Empty;
                    lblParentAccountNumberDisplay.Text = string.Empty;
                    lblCRMAccountNameDisplay.Text = string.Empty;
                    txtStringIdentifier.Text = string.Empty;
                    lblBillCycleDisplay.Text = string.Empty;
                    txtDateIdentifier.Enabled = false;
                }
            }
            else
            {
                txtStringIdentifier.Enabled = false;
                chkCrmAccount.Enabled = false;
                btnAddCRMAccount.Visible = false;

                lblParentAccountNameDisplay.Text = string.Empty;
                lblParentAccountNumberDisplay.Text = string.Empty;
                lblCRMAccountNameDisplay.Text = string.Empty;
                lblBillCycleDisplay.Text = string.Empty;
                txtDateIdentifier.Enabled = false;
            }
            //btnAddCRMAccount.Visible = true;
            //txtDateIdentifier.Enabled = true;
            mpeAddAccount.Show();
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validating CRM Account");
            LogException(ex, "btnValidate_Click");

        }

    }
        else
        {
            CSGAccountBL objAccount = new CSGAccountBL(connectionString);
            lblEditAccountErrorMessage.Visible = false;
            lblAddAccountErrorMessage.Visible = false;
            bool isValidated = true;
            try
            {
                var inputAccountNumber=txtCrmAccountNumber.Text;

                if (inputAccountNumber.Length < 11)  //10400095013
                {
                    inputAccountNumber = "0000" + inputAccountNumber;
                }

               var objCSGAccount = objAccount.ValidateCSGAccount(inputAccountNumber);
               if (objCSGAccount.AccountNumber>0)
               {                   
                   if (objCSGAccount.ParentAccountNumber > 0)
                   {
                       var validationCode = objAccount.ValidateParentAccountInMBM_CSG(objCSGAccount.ParentAccountNumber, objCSGAccount.AccountBillCycle);
                       if (validationCode == Convert.ToInt32(CSGValidationCode.ParentAccountNotAvailableInMBM))
                       {
                           lblAddAccountErrorMessage.Visible = true;
                           lblAddAccountErrorMessage.Text = "Parent Account " + objCSGAccount.ParentAccountNumber + " for entered child not available in MBM";
                           isValidated = false;
                       }
                       if (validationCode == Convert.ToInt32(CSGValidationCode.ParentChildBillCycyleNotMatched))
                       {
                           lblAddAccountErrorMessage.Visible = true;
                           lblAddAccountErrorMessage.Text = "Parent Account " + objCSGAccount.ParentAccountNumber + " Bill Cycyle Not matching with Child account " + inputAccountNumber + " Bill Cycyle In MBM";
                           isValidated = false;
                       }
                   }
                   
                       if (isValidated)
                       {
                           if (objCSGAccount != null && objCSGAccount.AccountNumber > 0)
                           {
                               lblAddAccountErrorMessage.Visible = false;
                               txtStringIdentifier.Enabled = true;
                               txtDateIdentifier.Enabled = true;
                               chkCrmAccount.Enabled = true;
                               btnAddCRMAccount.Visible = true;
                               checkChildPays.Enabled = true;
                               txtCrmAccountNumber.Text = txtCrmAccountNumber.Text;
                               lblParentAccountNameDisplay.Text = objCSGAccount.ParentAccountName;
                               lblParentAccountNumberDisplay.Text = objCSGAccount.ParentAccountNumber > 0 ? objCSGAccount.ParentAccountNumber.ToString() : string.Empty;
                               lblCRMAccountNameDisplay.Text = objCSGAccount.AccountName;
                               lblBillCycleDisplay.Text =!string.IsNullOrEmpty(objCSGAccount.AccountBillCycle)?Convert.ToString( objCSGAccount.AccountBillCycle):string.Empty;
                               hdnSubscriberId.Value =Convert.ToString(objCSGAccount.SubcriberNumber);
                           }
                       }                    
               }
               else
               {
                   txtStringIdentifier.Enabled = false;
                   chkCrmAccount.Enabled = false;
                   btnAddCRMAccount.Visible = false;
                   lblAddAccountErrorMessage.Visible = true;
                   lblAddAccountErrorMessage.Text = "No such CSG Account found.";
                   lblParentAccountNameDisplay.Text = string.Empty;
                   lblParentAccountNumberDisplay.Text = string.Empty;
                   lblCRMAccountNameDisplay.Text = string.Empty;
                   lblBillCycleDisplay.Text = string.Empty;
                   txtDateIdentifier.Enabled = false;
               }
                mpeAddAccount.Show();
            }
            catch (Exception ex)
            {
                Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validating CSG Account");
                LogException(ex, "btnValidate_Click");
            }
        }//CBE-1582
    }
    //CBE-1582

    protected string HierarchyDetails(string hierarchyId)
    {
        try
        {
            string JSONresult = string.Format("{{\"HierarchyId\":\"{0}\"}}", hierarchyId);
            byte[] byteArray = Encoding.UTF8.GetBytes(JSONresult);

            string requestUrl = ConfigurationManager.AppSettings["RetrieveHierarchy"];
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["CD-SystemID"] = ConfigurationManager.AppSettings["CSGAscendonSystemID"];
            request.Headers["CD-User"] = ConfigurationManager.AppSettings["CSGAscendonUserName"];
            request.Headers["CD-Password"] = ConfigurationManager.AppSettings["CSGAscendonPassword"];
            // request.Headers["CD-SubscriberId"] = txtCrmAccountNumber.Text;
            request.ContentLength = byteArray.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JSONresult);
            }

            string responseText = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }
            return responseText;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //CBE-1582

    //ER8668
    /// <summary>
    /// Validates Effective Bill Date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected string ValidateEffectiveBillDate(int invoiceTypeId, string accountNumber, DateTime? effectiveBillDate)
    {

        CRMAccountBL objAccount = new CRMAccountBL(connectionString);
        string response = string.Empty;
        lblEditAccountErrorMessage.Visible = false;
        lblAddAccountErrorMessage.Visible = false;
        try
        {
            response = objAccount.ValidateEffectiveBillingDate(invoiceTypeId, accountNumber, effectiveBillDate);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validating Effective Bill Date");
            LogException(ex, "ValidateEffectiveBillDate");
        }
        return response;
    }

    protected void AccountEffectiveDateStatus(int invoiceTypeId, string accountNumber)
    {

        CRMAccountBL objAccount = new CRMAccountBL(connectionString);
        int result;
        try
        {
            result = objAccount.AccountEffectiveDateStatus(invoiceTypeId, accountNumber);
            if (result == 0)
            {
                txtEditDateIdentifier.Enabled = true;
            }
            else
            {
                txtEditDateIdentifier.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validating Effective Bill Date");
            LogException(ex, "AccountEffectiveDateStatus");
        }
    }
    //ER8668

    /// <summary>
    /// Updates Associated CRM Account for Selected InvoiceType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateAccount_Click(object sender, EventArgs e)
    {
        try
        {

            if (lblBillingSystem.Text == "CRM")
            {
                #region CRM
            //updating the record  
            string UserName = GetUserName();
            CRMAccount objCRMAccount = new CRMAccount();

            if (!string.IsNullOrEmpty(lblEditCrmAccountNumberDisplay.Text.Trim()))
            {
                objCRMAccount.AccountNumber = Convert.ToInt32(lblEditCrmAccountNumberDisplay.Text.Trim());
            }

            objCRMAccount.AccountName = lblEditAccountNameDisplay.Text;

            if (!string.IsNullOrEmpty(lblEditParentAccountNumberDisplay.Text.Trim()))
            {
                objCRMAccount.ParentAccountNumber = Convert.ToInt32(lblEditParentAccountNumberDisplay.Text.Trim());
            }
            objCRMAccount.ParentAccountName = lblEditParentAccountNameDisplay.Text;
            objCRMAccount.StringIdentifier = txtEditStringIdentifier.Text.Trim();
            objCRMAccount.CreateAccount = chkEditCreateCRMAccount.Checked;
            objCRMAccount.InvoiceTypeid = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
            objCRMAccount.ChildPays = chkChildPays.Checked;
            objCRMAccount.UserName = lblEditUserName.Text;
            objCRMAccount.Password = txtPassword.Text;
            objCRMAccount.FTP = txtFTP.Text;
            objCRMAccount.EDI = chkEDI.Checked;
            objCRMAccount.EmailId = txtEmail.Text;

            if (string.IsNullOrEmpty(txtEditDateIdentifier.Text) || string.IsNullOrEmpty(txtEditDateIdentifier.Text.Trim()))
                objCRMAccount.EffectiveBillDate = null;
            else
                objCRMAccount.EffectiveBillDate = Convert.ToDateTime(txtEditDateIdentifier.Text.Trim());

            if (!string.IsNullOrEmpty(lblEditBillCycleDisplay.Text))
            {
                objCRMAccount.AccountBillCycle = Convert.ToInt32(lblEditBillCycleDisplay.Text);
            }

            CRMAccountBL objAccount = new CRMAccountBL(connectionString);
            List<CRMAccount> lstCRMAccount = new List<CRMAccount>();
            lstCRMAccount = objAccount.GetAllAssoicateAccounts();

            //Validate Effective Bill date
            string response = ValidateEffectiveBillDate(objCRMAccount.InvoiceTypeid, objCRMAccount.AccountNumber.ToString(), objCRMAccount.EffectiveBillDate);
            if (response != null)
            {
                if (response.Contains("Success"))
                {
                    lblEditAccountErrorMessage.Visible = false;

                        //Condition to check duplicate string identifier.
                        CRMAccount crmStringIdentifierFilter = lstCRMAccount.Find(m => m.StringIdentifier == objCRMAccount.StringIdentifier);

                        if (crmStringIdentifierFilter != null)
                        {
                            if (!string.IsNullOrEmpty(crmStringIdentifierFilter.StringIdentifier) && objCRMAccount.AccountNumber != crmStringIdentifierFilter.AccountNumber)
                            {
                                mpeEditAccount.Show();
                                lblEditAccountErrorMessage.Text = objCRMAccount.StringIdentifier + " is already associated to CRM A/C# " + crmStringIdentifierFilter.AccountNumber + " (" + crmStringIdentifierFilter.Prefix + ")";
                                lblEditAccountErrorMessage.Visible = true;
                            }
                            else
                            {
                                int result = objAccount.UpdateAssociatedAccount(objCRMAccount, UserName);
                                if (result >= 0)
                                {
                                    //Call ShowData method for displaying updated data  
                                    PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                                }
                            }
                        }
                        else
                        {
                            int result = objAccount.UpdateAssociatedAccount(objCRMAccount, UserName);
                            if (result >= 0)
                            {
                                //Call ShowData method for displaying updated data  
                                PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                            }
                        }
                    }
                else
                {
                    mpeEditAccount.Show();
                    lblEditAccountErrorMessage.Text = response;
                    lblEditAccountErrorMessage.Visible = true;
                }
            }
                #endregion CRM
        }
            else
            {
                #region CSG
                string UserName = GetUserName();
                CSGAccount objCSGAccount = new CSGAccount();

                if (!string.IsNullOrEmpty(lblEditCrmAccountNumberDisplay.Text.Trim()))
                {
                    objCSGAccount.AccountNumber = Convert.ToInt64(lblEditCrmAccountNumberDisplay.Text.Trim());
                }

                objCSGAccount.AccountName = lblEditAccountNameDisplay.Text;

                if (!string.IsNullOrEmpty(lblEditParentAccountNumberDisplay.Text.Trim()))
                {
                    objCSGAccount.ParentAccountNumber = Convert.ToInt64(lblEditParentAccountNumberDisplay.Text.Trim());
                }
                objCSGAccount.ParentAccountName = lblEditParentAccountNameDisplay.Text;
                objCSGAccount.StringIdentifier = txtEditStringIdentifier.Text.Trim();
                objCSGAccount.CreateAccount = chkEditCreateCRMAccount.Checked;
                objCSGAccount.InvoiceTypeid = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
                objCSGAccount.ChildPays = chkChildPays.Checked;
                objCSGAccount.UserName = lblEditUserName.Text;
                objCSGAccount.Password = txtPassword.Text;
                objCSGAccount.FTP = txtFTP.Text;
                objCSGAccount.EDI = chkEDI.Checked;
                objCSGAccount.EmailId = txtEmail.Text;

                if (string.IsNullOrEmpty(txtEditDateIdentifier.Text) || string.IsNullOrEmpty(txtEditDateIdentifier.Text.Trim()))
                    objCSGAccount.EffectiveBillDate = null;
                else
                    objCSGAccount.EffectiveBillDate = Convert.ToDateTime(txtEditDateIdentifier.Text.Trim());

                if (!string.IsNullOrEmpty(lblEditBillCycleDisplay.Text))
                {
                    objCSGAccount.AccountBillCycle = Convert.ToString(lblEditBillCycleDisplay.Text);
                }

                CSGAccountBL objAccount = new CSGAccountBL(connectionString);
                List<CSGAccount> lstCRMAccount = new List<CSGAccount>();

                lblEditAccountErrorMessage.Visible = false;
                var Isvalidate = objAccount.GetValidateCSGAssoicateAccounts(objCSGAccount.AccountNumber, objCSGAccount.StringIdentifier,2);

                if (Isvalidate == Convert.ToInt32(CSGValidationCode.DuplicationOfStringIdentifier))
                {
                    mpeEditAccount.Show();
                    lblAddAccountErrorMessage.Text = "Provisioning Identifier : " + objCSGAccount.StringIdentifier + " is already associated to biller account";
                    lblAddAccountErrorMessage.Visible = true;
                }
                if (Isvalidate == Convert.ToInt32(CSGValidationCode.Success))
                {
                    int result = objAccount.UpdateAssociatedAccount(objCSGAccount, UserName);
                    if (result >= 0)
                    {
                        Controls_ErrorSuccessNotifier.AddSuccessMessage("Updating Associated Accounts Successfully");
                        //Call ShowData method for displaying updated data  
                        PopulateAssociateAccounts(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                    }
                }
                #endregion CSG
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while updating Associated Accounts");
            LogException(ex, "btnUpdateAccount_Click");
        }
    }

    /// <summary>
    /// Get Bill Cycle using Invoice Type
    /// </summary>
    /// <param name="strInvoiceTypeId"></param>
    /// <returns></returns>
    private string GetInvoiceTypeBillCycle(string strInvoiceTypeId)
    {
        string strBillCycle = string.Empty;

        return strBillCycle;
    }
    //ER# 876 start
    #region Profile

    protected void btnAddProfileDailog_Click(object sender, EventArgs e)
    {
        mpeAddProfile.Show();
        btnAddProfile.Visible = true;
        btnUpdateProfile.Visible = false;
        ClearAddProfile();
        //showProfileAccoridion();
    }

    /// <summary>
    /// Add new Profile
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddProfile_Click(object sender, EventArgs e)
    {
        ProfileBL objProfileTypes = new ProfileBL(connectionString);
        Profile objProfileType = new Profile();
        int invoicetype = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);

        #region Validations
        if (invoicetype != null)
        {
            objProfileType.InvoiceType = invoicetype;
        }
        if (!string.IsNullOrEmpty(txtProfileName.Text.Trim()) && !string.IsNullOrEmpty(txtProfileDescription.Text))
        {
            objProfileType.ProfileName = txtProfileName.Text.Trim();
            objProfileType.Description = txtProfileDescription.Text;
        }
        else
        {
            mpeAddProfile.Show();
            lblAddProfileErrorMessage.Text = "ProfileName & ProfileDescription are mandatory fields";
            lblAddProfileErrorMessage.Visible = true;
            return;
        }
        List<Profile> profiles = GetProfiles(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
        Profile profile = profiles.Where(p => p.ProfileName.ToLower() == objProfileType.ProfileName.ToLower()).SingleOrDefault();
        if (profile != null && profile.ProfileId > 0)
        {
            mpeAddProfile.Show();
            lblAddProfileErrorMessage.Text = "Duplicate Profile are not allowed";
            lblAddProfileErrorMessage.Visible = true;
            return;
        }

        #endregion


        try
        {
            int result = objProfileTypes.AddNewProfile(objProfileType);
            Controls_ErrorSuccessNotifier.AddSuccessMessage(objProfileType.ProfileName + " added successfully");
            PopulateProfiles(invoicetype);

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Adding Profile");
            LogException(ex, "btnAddFileType_Click");
        }
        finally
        {
            //showProfileAccoridion();
        }
    }

    protected void grdProfiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ProfileBL objProfile = new ProfileBL(connectionString);
        try
        {
            int invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
            string invoiceTypeName = ddlInvoiceType.SelectedItem.Text;
            int profileid = Convert.ToInt32(grdProfiles.DataKeys[e.RowIndex].Value.ToString());
            if (profileid != null)
            {
                if (!objProfile.IsProfileExistInEnhanceData(profileid))
                {
                    objProfile.DeleteProfile(profileid);
                    PopulateProfiles(invoiceTypeId);
                    PopulateProfileCharges(invoiceTypeId, invoiceTypeName);
                }
                else
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("We can not delete the Profile which are already used for invoice ");
                }
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while deleting Profile");
            LogException(ex, "grdProfiles_RowDeleting");
        }
        finally
        {
            //showProfileAccoridion();
        }
    }

    protected void grdProfiles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ClearUpdateProfile();
            int profileId = Convert.ToInt32(((GridView)sender).DataKeys[e.NewEditIndex].Value);
            List<Profile> profiles = GetProfiles(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
            if (profiles != null && profiles.Count > 0)
            {
                Profile profile = profiles.Where(p => p.ProfileId == profileId).SingleOrDefault();
                txtProfileName.Text = profile.ProfileName;
                txtProfileDescription.Text = profile.Description;
                hdnProfileId.Value = profile.ProfileId.ToString();
                btnAddProfile.Visible = false;
                btnUpdateProfile.Visible = true;
                hAddProfile.Visible = false;
                hEditProfile.Visible = true;
                mpeAddProfile.Show();
            }
            else
            {
                lblAddProfileErrorMessage.Text = "Error while fetching profile";
            }
        }
        catch (Exception ex)
        {

            //throw;
        }
        //showProfileAccoridion();
    }

    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        try
        {
            int invoiceId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
            string invoiceName = ddlInvoiceType.SelectedItem.Text;
            List<Profile> profiles = GetProfiles(invoiceId);
            if (profiles != null && profiles.Count > 0)
            {
                int selectedProfileId = 0;
                if (!string.IsNullOrEmpty(hdnProfileId.Value))
                {
                    selectedProfileId = Convert.ToInt32(hdnProfileId.Value);
                }
                Profile exitingProfile = new Profile();
                exitingProfile = profiles.Where(p => p.ProfileName.ToLower() == txtProfileName.Text.Trim().ToLower()).SingleOrDefault();

                if (exitingProfile != null && selectedProfileId != exitingProfile.ProfileId)
                {
                    lblAddProfileErrorMessage.Text = "Profile already exists";
                    lblAddProfileErrorMessage.Visible = true;
                    mpeAddProfile.Show();

                }
                else
                {
                    Profile profile = new Profile();
                    profile.ProfileName = txtProfileName.Text;
                    profile.Description = txtProfileDescription.Text;
                    profile.ProfileId = selectedProfileId;
                    UpdateProfile(profile);
                    PopulateProfiles(invoiceId);
                    PopulateProfileCharges(invoiceId, invoiceName);
                }
            }
        }
        catch (Exception ex)
        {
            lblAddProfileErrorMessage.Text = "Something went wrong";
            mpeAddProfile.Show();
        }
        finally
        {
            //showProfileAccoridion();
        }
    }

    #endregion

    #region ProfileCharge
    protected void btnProfileChargePopDailog_Click(object sender, EventArgs e)
    {
        ClearAddProfileChargePopup();
        //bind profiles data to dropdownlist
        List<Profile> profiles = GetProfiles(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
        ddlProfileName.DataTextField = "ProfileName";
        ddlProfileName.DataValueField = "ProfileId";
        ddlProfileName.DataSource = profiles;
        ddlProfileName.DataBind();
        ddlProfileName.SelectedIndex = -1;
        txtprofileChargeAmount.Enabled = false;

        //SERO-1582

        DropDownList1.DataTextField = "ProfileName";
        DropDownList1.DataValueField = "ProfileId";
        DropDownList1.DataSource = profiles;
        DropDownList1.DataBind();
        DropDownList1.SelectedIndex = -1;
        DropDownList1.Enabled = true;




        List<Profile> offer = GetStringIdentifier(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));

        DropDownList2.DataTextField = "ChargeStringIdentifier";
        DropDownList2.DataValueField = "CatalogItemId";
        DropDownList2.DataSource = offer;
        DropDownList2.DataBind();
        DropDownList2.SelectedIndex = -1;
        DropDownList2.Enabled = true;


        if (profiles.Count == 0)
        {
            lblprofileChargesErrorMessage.Text = "Please Add Profile before adding profile charge";
            lblprofileChargesErrorMessage.Visible = true;
            lblCSGprofileChargesErrorMessage.Text = "Please Add Profile before adding profile charge";
            lblCSGprofileChargesErrorMessage.Visible = true;
            BtnSaveProfileCharge.Enabled = false;
        }
        //showProfileChargeAccoridion();
        if (lblBillingSystem.Text == "CRM") //SERO-1582
        {
        mpeCreateProfileCharge.Show();
    }
        else
        {
            mpeCreateProfileChargeCSG.Show();
        }

        //SERO-1582
    }

    protected void BtnSaveProfileCharge_Click(object sender, EventArgs e)
    {
        try
        {
            ProfileBL objProfileTypes = new ProfileBL(connectionString);
            ProfileCharge objProfileCharge = new ProfileCharge();

            if (BtnSaveProfileCharge.CommandName == "Validate")
            {
                lblprofileChargesErrorMessage.Text = "";
                lblprofileChargesErrorMessage.Visible = false;
                ValidateGLCodeWithCRM();
                BtnSaveProfileCharge.Enabled = true;
            }
            else if (BtnSaveProfileCharge.CommandName == "AddCharges")
            {
                if (ddlInvoiceType.SelectedIndex > 0)
                {
                    int invoicetype = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
                    string invoiceTypeName = ddlInvoiceType.SelectedItem.Text;

                    #region Validations
                    if (invoicetype > 0)
                    {
                        objProfileCharge.InvoiceTypeId = invoicetype;
                    }
                    ValidateGLCodeWithCRM();
                    if (!string.IsNullOrEmpty(ddlProfileName.SelectedItem.Text) && !string.IsNullOrEmpty(txtProfileChargeDesc.Text)
                            && !string.IsNullOrEmpty(txtprofileChargeFeature.Text) && !string.IsNullOrEmpty(txtProfileChargeGLCode.Text)
                            && !string.IsNullOrEmpty(txtprofileChargeAmount.Text))
                    {
                        objProfileCharge.InvoiceTypeId = invoicetype;
                        objProfileCharge.ProfileId = Convert.ToInt32(ddlProfileName.SelectedItem.Value);
                        objProfileCharge.ProfileName = ddlProfileName.SelectedItem.Text;
                        objProfileCharge.GLCode = Convert.ToInt32(txtProfileChargeGLCode.Text);
                        objProfileCharge.Feature = txtprofileChargeFeature.Text;
                        objProfileCharge.ChargeDescription = txtProfileChargeDesc.Text;
                        objProfileCharge.ChargeAmount = Convert.ToDecimal(txtprofileChargeAmount.Text);
                    }
                    else
                    {
                        mpeCreateProfileCharge.Show();
                        StringBuilder errormsg = new StringBuilder("Please Provide below details<ul>");
                        if (string.IsNullOrEmpty(ddlProfileName.SelectedItem.Text))
                        {
                            errormsg.Append("<li>ProfileId</li>");
                        }
                        if (string.IsNullOrEmpty(txtProfileChargeGLCode.Text))
                        {
                            errormsg.Append("<li>GLCode</li>");
                        }
                        if (string.IsNullOrEmpty(txtprofileChargeAmount.Text))
                        {
                            errormsg.Append("<li>Charge</li>");
                        }
                        errormsg.Append("</ul>");
                        lblprofileChargesErrorMessage.Text = errormsg.ToString();//"Provide info for all mandatory fields";
                        lblprofileChargesErrorMessage.Visible = true;
                        return;
                    }
                    List<ProfileCharge> profileCharges = GetProfileCharges(invoicetype, invoiceTypeName);
                    ProfileCharge findProfileCharge = profileCharges.Where(p => (p.GLCode == objProfileCharge.GLCode) && (p.Feature == objProfileCharge.Feature)).SingleOrDefault();
                    if (findProfileCharge != null && findProfileCharge.ProfileId > 0)
                    {
                        mpeCreateProfileCharge.Show();
                        lblprofileChargesErrorMessage.Text = "Duplicate ProfileCharge are not allowed";
                        lblprofileChargesErrorMessage.Visible = true;
                        return;
                    }
                    #endregion

                    try
                    {
                        objProfileTypes.AddNewProfileCharges(objProfileCharge);
                        Controls_ErrorSuccessNotifier.AddSuccessMessage("Profile Charge for " + ddlInvoiceType.SelectedItem.Text + " added successfully");
                        mpeCreateProfileCharge.Hide();
                        PopulateProfileCharges(invoicetype, invoiceTypeName);
                    }
                    catch (Exception ex)
                    {
                        mpeCreateProfileCharge.Show();
                        Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Adding Profile Charge");
                        LogException(ex, "btnAddFileType_Click");
                    }
                }
                else
                {
                    lblprofileChargesErrorMessage.Text = "please select profile";
                    lblprofileChargesErrorMessage.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validation");
            LogException(ex, "BtnSaveProfileCharge_Click");
        }
        finally
        {
            //showProfileChargeAccoridion();
        }

    }


    //SERO-1582//

    protected void BtnSaveProfileChargeCSG_Click(object sender, EventArgs e)
    {
        try
        {
            ProfileBL objProfileTypes = new ProfileBL(connectionString);
            ProfileCharge objProfileChargeCSG = new ProfileCharge();

            if (BtnSaveProfileChargeCSG.CommandName == "Save")
            {
                int invoicetype = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
                string invoiceTypeName = ddlInvoiceType.SelectedItem.Text;


                if (invoicetype > 0)
                {
                    objProfileChargeCSG.InvoiceTypeId = invoicetype;
                }
                if (!string.IsNullOrEmpty(txtCSGprofileChargeAmount.Text))
                {
                    if (!string.IsNullOrEmpty(DropDownList1.SelectedItem.Text) && !string.IsNullOrEmpty(DropDownList2.Text))
                    {
                        objProfileChargeCSG.InvoiceTypeId = invoicetype;
                        objProfileChargeCSG.ProfileId = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                        objProfileChargeCSG.ProfileName = DropDownList1.SelectedItem.Text;
                        objProfileChargeCSG.CatalogItemId = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                        objProfileChargeCSG.ChargeAmount = Convert.ToDecimal(txtCSGprofileChargeAmount.Text);
                       // objProfileChargeCSG.EffectiveStartDate = Convert.ToDateTime(txtCSGprofileChargeEffectiveStartDate.Text == "" ? DateTime.Now.AddDays(-30).ToString() : txtCSGprofileChargeEffectiveStartDate.Text);


                        List<ProfileCharge> profileCharges = GetProfileChargesCSG(invoicetype, invoiceTypeName);
                        ProfileCharge findProfileCharge = profileCharges.Where(p => (p.InvoiceTypeId == objProfileChargeCSG.InvoiceTypeId) && (p.CatalogItemId == objProfileChargeCSG.CatalogItemId)).SingleOrDefault();
                        if (findProfileCharge != null && findProfileCharge.CatalogItemId > 0)
                        {
                            mpeCreateProfileChargeCSG.Show();
                            lblCSGprofileChargesErrorMessage.Text = "Duplicate ProfileCharge are not allowed";
                            lblCSGprofileChargesErrorMessage.Visible = true;
                            return;
                        }

                        objProfileTypes.AddNewProfileChargesCSG(objProfileChargeCSG);

                        Controls_ErrorSuccessNotifier.AddSuccessMessage("Profile Charge for " + ddlInvoiceType.SelectedItem.Text + " added successfully");
                        mpeCreateProfileChargeCSG.Hide();
                        PopulateProfileCharges(invoicetype, invoiceTypeName);
                    }

                    else
                    {
                        mpeCreateProfileChargeCSG.Show();

                        StringBuilder errormsg = new StringBuilder("Please Provide below details<ul>");

                        if (string.IsNullOrEmpty(DropDownList1.SelectedItem.Text) || string.IsNullOrEmpty(DropDownList2.Text))
                        {
                            errormsg.Append("<li></li>");
                        }

                    }
                }
                else
                {
                    mpeCreateProfileChargeCSG.Show();
                    lblCSGprofileChargesErrorMessage.Text = "Please Add Charge Amount";
                    lblCSGprofileChargesErrorMessage.Visible = true;
                    return;
                }
            }
        }

        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validation");
            LogException(ex, "BtnSaveProfileCharge_Click");
        }
    }
    protected void BtnUpdateProfileChargeCSG_Click(object sender, EventArgs e)
    {
        try
        {
            ProfileBL objProfileTypes = new ProfileBL(connectionString);
            ProfileCharge profileCharge = new ProfileCharge();
            if (BtnUpdateProfileChargeCSG.CommandName == "UpdateCharge")
            {
                if (!string.IsNullOrEmpty(txtCSGprofileChargeAmount.Text))
                {

                    if (!string.IsNullOrEmpty(hdnChargeId.Value))
                    {
                        int invoicetypeCSG = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
                        string invoiceTypeNameCSG = ddlInvoiceType.SelectedItem.Text;

                        if (invoicetypeCSG > 0)
                        {
                            profileCharge.InvoiceTypeId = invoicetypeCSG;
                        }
                        profileCharge.ProfileId = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                        profileCharge.ChargeId = Convert.ToInt32(hdnChargeId.Value);
                        profileCharge.ChargeAmount = Convert.ToDecimal(txtCSGprofileChargeAmount.Text);
                        profileCharge.CatalogItemId = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                        //profileCharge.EffectiveStartDate = Convert.ToDateTime(txtCSGprofileChargeEffectiveStartDate.Text);
                        objProfileTypes.EditprofileChargesForCSG(profileCharge);
                        PopulateProfileCharges(invoicetypeCSG, invoiceTypeNameCSG);
                        Controls_ErrorSuccessNotifier.AddSuccessMessage("updated ProfileCharge successfully");
                        var data = Convert.ToInt32(Session["grdEditedRow"].ToString());
                        grdProfileChargesCSG.SelectedIndex = Convert.ToInt32(Session["grdEditedRow"].ToString());
                        //grdProfileChargesCSG.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#39c");Bug 31953 
                        if (grdProfileChargesCSG.SelectedRow.RowType == DataControlRowType.DataRow)
                        {
                            if (grdProfileChargesCSG.SelectedRow.RowIndex == data)
                            {
                                grdProfileChargesCSG.SelectedRow.Cells[4].Enabled = false;
                                grdProfileChargesCSG.SelectedRow.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

                            }
                        }
                    }
                }
                else
                {
                    mpeCreateProfileChargeCSG.Show();
                    lblCSGprofileChargesErrorMessage.Text = "Please Add Charge Amount";
                    lblCSGprofileChargesErrorMessage.Visible = true;
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validation");
            LogException(ex, "BtnUpdateProfileChargeCSG_Click");
        }
    }
    protected void BtnUpdateProfileCharge_Click(object sender, EventArgs e)
    {
        try
        {
            ProfileBL objProfileTypes = new ProfileBL(connectionString);
            ProfileCharge objProfileCharge = new ProfileCharge();

            if (!string.IsNullOrEmpty(hdnChargeId.Value))
            {
                ProfileCharge profileCharge = new ProfileCharge()
                {
                    ChargeId = Convert.ToInt32(hdnChargeId.Value),
                    ChargeAmount = Convert.ToDecimal(txtprofileChargeAmount.Text)
                };
                objProfileTypes.EditprofileCharges(profileCharge);
                int invoicetype = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
                string invoiceTypeName = ddlInvoiceType.SelectedItem.Text;
                PopulateProfileCharges(invoicetype, invoiceTypeName);
                Controls_ErrorSuccessNotifier.AddSuccessMessage("updated ProfileCharge successfully");
            }

            //8142            
            grdProfileCharges.SelectedIndex = Convert.ToInt32(Session["grdEditedRow"].ToString());
            //grdProfileCharges.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#39c");

            //ScriptManager.RegisterStartupScript(this, typeof(Page), "ScrollBottom", "ScrollBottom()", true);
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while validation");
            LogException(ex, "BtnSaveProfileCharge_Click");
        }
        finally
        {
            //showProfileChargeAccoridion();
        }
    }

    protected void grdProfileCharges_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ProfileBL objProfile = new ProfileBL(connectionString);
        try
        {
            int chargeid = Convert.ToInt32(grdProfileCharges.DataKeys[e.RowIndex].Value.ToString());
            if (chargeid != null)
            {
                if (!objProfile.IsProfileChargeExistInEnhanceData(chargeid))
                {
                    objProfile.DeleteProfileCharge(chargeid);
                    PopulateProfileCharges(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value), ddlInvoiceType.SelectedItem.Text);
                }
                else
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("As Profile charge is existing in billing, we are deleting the profilecharge ");
                }
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while deleting Profile");
            LogException(ex, "grdProfiles_RowDeleting");
        }
        finally
        {
            //showProfileChargeAccoridion();
        }
    }

    protected bool MbmCSGProfileChargesVerifyInEnhanceData(int chargeId)
    {
        int isValidate = 0;
        ProfileBL objProfile = new ProfileBL(connectionString);
        isValidate = objProfile.ValidateProfileChargeCSG(chargeId);
        if (isValidate == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void grdProfileCharges_RowDeletingCSG(object sender, GridViewDeleteEventArgs e)
    {
        ProfileBL objProfile = new ProfileBL(connectionString);
        try
        {
            int chargeid = Convert.ToInt32(grdProfileChargesCSG.DataKeys[e.RowIndex].Value.ToString());
            if (chargeid != null)
            {
                if (!MbmCSGProfileChargesVerifyInEnhanceData(chargeid))
                {
                    objProfile.DeleteProfileChargeCSG(chargeid);
                    PopulateProfileCharges(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value), ddlInvoiceType.SelectedItem.Text);
                    //if (!objProfile.IsProfileChargeExistInEnhanceData(chargeid))                    
                }
                else
                {
                    Controls_ErrorSuccessNotifier.AddErrorMessage("As Profile charge is existing in Enhanced data, we are deleting the profilecharge ");
                    //Controls_ErrorSuccessNotifier.Visible = true;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while deleting Profile");
            LogException(ex, "grdProfiles_RowDeleting");
        }
        finally
        {
            //showProfileChargeAccoridion();
        }
    }
    //sero 1582 need to check here

    public void grdProfileCharges_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearUpdateProfileCharge();
        int chargeId = Convert.ToInt32(((GridView)sender).DataKeys[e.NewEditIndex].Value);
        var recentEdit = e.NewEditIndex;
        hdnChargeId.Value = string.Empty;
        hdnChargeId.Value = chargeId.ToString();

        int invoicetype = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
        string invoiceTypeName = ddlInvoiceType.SelectedItem.Text;
        List<ProfileCharge> profilesCharges = null;
        if (lblBillingSystem.Text == "CRM")
        {
            profilesCharges = GetProfileCharges(invoicetype, invoiceTypeName);
        if (profilesCharges != null && profilesCharges.Count > 0)
        {
            ProfileCharge profileCharge = profilesCharges.Where(p => p.ChargeId == chargeId).SingleOrDefault();
            txtprofileChargeAmount.Text = profileCharge.ChargeAmount.ToString();
            txtProfileChargeDesc.Text = profileCharge.ChargeDescription;
            txtprofileChargeFeature.Text = profileCharge.Feature;
            txtProfileChargeGLCode.Text = profileCharge.GLCode.ToString();

            txtProfileChargeGLCode.Enabled = false;

            List<Profile> profiles = GetProfiles(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
            ddlProfileName.DataTextField = "ProfileName";
            ddlProfileName.DataValueField = "ProfileId";
            ddlProfileName.DataSource = profiles.Where(p => p.ProfileName == profileCharge.ProfileName);
            ddlProfileName.DataBind();
            ddlProfileName.Enabled = false;

            mpeCreateProfileCharge.Show();


            Session["grdEditedRow"] = e.NewEditIndex;
        }
        else
        {
            lblAddProfileErrorMessage.Text = "Error while fetching profile charge";
            lblAddProfileErrorMessage.Visible = true;
        }
        }
        else
        {
            profilesCharges = GetProfileChargesCSG(invoicetype, invoiceTypeName);
            if (profilesCharges != null && profilesCharges.Count > 0)
            {
                ProfileCharge profileCharge = profilesCharges.Where(p => p.ChargeId == chargeId).SingleOrDefault();
                txtCSGprofileChargeAmount.Text = Convert.ToString(profileCharge.ChargeAmount);
               // txtCSGprofileChargeEffectiveStartDate.Text = Convert.ToString(profileCharge.EffectiveStartDate);
                txtProfileChargeDesc.Text = profileCharge.ChargeDescription;
                List<Profile> profiles = GetProfiles(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                DropDownList1.DataTextField = "ProfileName";
                DropDownList1.DataValueField = "ProfileId";
                DropDownList1.DataSource = profiles.Where(p => p.ProfileName == profileCharge.ProfileName);
                DropDownList1.DataBind();
                DropDownList1.Enabled = false;
                List<Profile> offer = GetStringIdentifier(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
                DropDownList2.DataTextField = "ChargeStringIdentifier";
                DropDownList2.DataValueField = "CatalogItemId";
                DropDownList2.DataSource = offer.Where(p => p.ChargeStringIdentifier == profileCharge.ChargeDescription);
                DropDownList2.DataBind();
                DropDownList2.Enabled = false;
                mpeCreateProfileChargeCSG.Show();
                Session["grdEditedRow"] = e.NewEditIndex;
                BtnSaveProfileChargeCSG.Visible = false;
                BtnUpdateProfileChargeCSG.Visible = true;
                var buttonEdit = (Button)grdProfileChargesCSG.HeaderRow.FindControl("btnProfileChargeDelete");
                //(((GridView)sender).DataKeys[e.NewEditIndex].Value);
                //foreach (var item in collection)
                //{

                //}

            }
            else
            {
                lblAddProfileErrorMessage.Text = "Error while fetching profile charge";
                lblAddProfileErrorMessage.Visible = true;
            }
        }


        //showProfileChargeAccoridion();
    }

    protected void btnProfileChargeEdit_Click(object sender, EventArgs eve)
    { }
    #endregion

    #region ManualCharge
    protected void grdManualChargeFileInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ProfileBL objProfile = new ProfileBL(connectionString);
        try
        {
            int ManualChargeid = Convert.ToInt32(grdManualChargeFileInfo.DataKeys[e.RowIndex].Value.ToString());
            if (ManualChargeid > 0)
            {
                objProfile.DeleteManulaChargeFileByFileId(ManualChargeid);
                populateManualChangeFileInfo(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value));
            }
        }
        catch (Exception ex)
        {
            Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while deleting Profile");
            LogException(ex, "grdProfiles_RowDeleting");
        }
    }

    protected void btnProceessManualUploadFile_Click(object sender, EventArgs e)
    {
        if (fpManualFileuploadFilePath.HasFile)
        {
            int invoiceTypeId = 0;
            string MChargeFileName = string.Empty;
            string userName = string.Empty;
            string filePath = string.Empty;
            int fileId = 0;

            try
            {
                FileTypeBL objFileTypes = new FileTypeBL(connectionString);
                invoiceTypeId = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);
                MChargeFileName = fpManualFileuploadFilePath.FileName;
                userName = GetUserName();
                filePath = SaveExcelToServer();
                if (!string.IsNullOrEmpty(filePath))
                {
                    fileId = UploadManualChargeFile(invoiceTypeId, filePath, "Uploaded", userName) ?? default(int);
                    DataTable dt = new DataTable();
                    if (fileId > 0)
                    {
                        dt = ReadDataFromExcel(filePath);
                        dt.Columns.Add("FileId");
                        dt.Columns.Add("InvoiceTypeId");
                        MapDataTable(ref dt, fileId);
                        if (dt.Rows.Count >= 1)
                        {
                            objFileTypes.BulkCopy_MBM_ManualChargeDataToSqlServer(dt, connectionString);
                        }

                        //Process the file data
                        ProcessMaualChargesData(fileId);

                        //provide email functionality.
                        SendMailNotification(userName, fileId, MChargeFileName, MailType.ManualChargeTemplate);
                        Controls_ErrorSuccessNotifier.AddSuccessMessage("Uploaded file processed successfully");
                    }
                }
            }
            catch (OleDbException ex)
            {
                updateManualChargeFileStatus(fileId, "Invalid File");
                SendMailNotification(userName, fileId, MChargeFileName, MailType.ManualChargeFailedTemplate, ex.Message);

                Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Adding File/Processing Data. Error Message: " + ex.Message);
                LogException(ex, "btnProceessManualUploadFile_Click");
            }
            catch (InvalidOperationException ex)
            {
                updateManualChargeFileStatus(fileId, "Invalid File");
                SendMailNotification(userName, fileId, MChargeFileName, MailType.ManualChargeFailedTemplate, ex.Message);

                Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Adding File/Processing Data. Error Message: " + ex.Message);
                LogException(ex, "btnProceessManualUploadFile_Click");
            }
            catch (Exception ex)
            {
                updateManualChargeFileStatus(fileId, "Invalid File");
                SendMailNotification(userName, fileId, MChargeFileName, MailType.ManualChargeFailedTemplate, ex.Message);

                Controls_ErrorSuccessNotifier.AddErrorMessage("Problem occurred while Adding File/Processing Data. Error Message: " + ex.Message);
                LogException(ex, "btnProceessManualUploadFile_Click");
            }
            finally
            {
                //reload the filestatus info grid
                populateManualChangeFileInfo(invoiceTypeId);
            }

        }

    }

    protected void BtnMChargeProcesslog_Click(object sender, EventArgs e)
    {
        ImageButton imgButton = sender as ImageButton;
        //ExportGridDataToExcel(Convert.ToInt32(imgButton.CommandArgument), imgButton.CommandName, DownloadFileType.RecordsByFileid);
        ExportDataTableToExcel(Convert.ToInt32(imgButton.CommandArgument), imgButton.CommandName, DownloadFileType.RecordsByFileid);
    }

    protected void btnManualSuccessData_Click(object sender, EventArgs e)
    {
        //ExportGridDataToExcel(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value), ddlInvoiceType.SelectedItem.Text, DownloadFileType.SuccessRecords);
        ExportDataTableToExcel(Convert.ToInt32(ddlInvoiceType.SelectedItem.Value), ddlInvoiceType.SelectedItem.Text, DownloadFileType.SuccessRecords);
    }

    protected void grdManualChargeFileInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //If files are not processed disble the process log   
            if (!((e.Row.DataItem as ManualChargeFileDetails).MCharge_file_RecordCount).HasValue &&
                    ((e.Row.DataItem as ManualChargeFileDetails).MCharge_FileStatus.Equals("Invalid File")))
            {
                ((ImageButton)e.Row.FindControl("BtnMChargeProcesslog")).Visible = false;
            }
        }
    }
    #endregion
    //ER# 876 end
    #endregion

    protected void hfAddAccount_ValueChanged(object sender, EventArgs e)
    {

    }
    protected void grdAssociateAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
      protected CSGAccount GetCSGUserInputFromUI()
    {
          CSGAccount objCSGAccount = new CSGAccount();

            if (!String.IsNullOrEmpty(txtCrmAccountNumber.Text))
            {
                objCSGAccount.AccountNumber = Convert.ToInt64(txtCrmAccountNumber.Text);
            }

            if (!String.IsNullOrEmpty(lblCRMAccountNameDisplay.Text))
            {
                objCSGAccount.AccountName = lblCRMAccountNameDisplay.Text;
            }

            if (!String.IsNullOrEmpty(lblParentAccountNumberDisplay.Text))
            {
                objCSGAccount.ParentAccountNumber = Convert.ToInt64(lblParentAccountNumberDisplay.Text);
            }

            if (!String.IsNullOrEmpty(lblParentAccountNameDisplay.Text))
            {
                objCSGAccount.ParentAccountName = lblParentAccountNameDisplay.Text;
            }

            if (!String.IsNullOrEmpty(txtStringIdentifier.Text))
            {
                objCSGAccount.StringIdentifier = txtStringIdentifier.Text.Trim();
            }

            if (!string.IsNullOrEmpty(lblBillCycleDisplay.Text))
            {
                objCSGAccount.AccountBillCycle = lblBillCycleDisplay.Text;
            }

            if (!String.IsNullOrEmpty(txtDateIdentifier.Text))
            {
                objCSGAccount.EffectiveBillDate = Convert.ToDateTime(txtDateIdentifier.Text);
            }

            if (chkCrmAccount.Checked == true)
            {
                objCSGAccount.CreateAccount = true;
            }
            else
            {
                objCSGAccount.CreateAccount = false;
            }
            objCSGAccount.InvoiceTypeid = Convert.ToInt32(ddlInvoiceType.SelectedItem.Value);

            if (checkChildPays.Checked == true)
            {
                objCSGAccount.ChildPays = true;
            }
            else
            {
                objCSGAccount.ChildPays = false;
            }

            if (!String.IsNullOrEmpty(textUserName.Text))
            {
                objCSGAccount.UserName = textUserName.Text;
            }

            if (!String.IsNullOrEmpty(textPassword.Text))
            {
                objCSGAccount.Password = textPassword.Text;
            }

            if (!String.IsNullOrEmpty(textFTP.Text))
            {
                objCSGAccount.FTP = textFTP.Text;
            }

            if (!String.IsNullOrEmpty(textEmail.Text))
            {
                objCSGAccount.EmailId = textEmail.Text;
            }

            if (checkEDI.Checked == true)
            {
                objCSGAccount.EDI = true;
            }
            else
            {
                objCSGAccount.EDI = false;
            }
            if (!string.IsNullOrEmpty(hdnSubscriberId.Value))
            {
                objCSGAccount.SubcriberNumber = Convert.ToInt32(hdnSubscriberId.Value);
            }

            // SERO 3854
            if (!String.IsNullOrEmpty(txtCrmAccountNumber.Text))
            {
                objCSGAccount.DisplayAccountNumber = txtCrmAccountNumber.Text;

                if (objCSGAccount.DisplayAccountNumber.Length < 11)
                {
                    objCSGAccount.DisplayAccountNumber = "0000" + objCSGAccount.DisplayAccountNumber;
                }
            }

            return objCSGAccount;
    }
}