using MBM.Entities;
using MBM.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BillingEngine
{
    public class ExportInvoiceLogic
    {
        string strConnectionString;
        public ExportInvoiceLogic()
        {
           strConnectionString= ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();
        }
        private BillingEngine _billing;
        public bool ExportInvoice(int invoiceId,string invoiceNumber,int invoiceTypeId,string userName,bool isByLegalEntity)
        {
            bool isExported = false;            

            try
            {
                FileExport export = new FileExport(strConnectionString);
                //int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
                CurrencyConversion currencyConversion = GetCurrencyConversion(invoiceId, userName);
                CurrencyConversion exportCurrency = new CurrencyConversion();

                if (currencyConversion != null)
                {
                    exportCurrency.Code = currencyConversion.Code;
                    exportCurrency.ConversionRate = currencyConversion.ConversionRate;
                    exportCurrency.Description = currencyConversion.Description;
                    exportCurrency.ID = currencyConversion.ID;
                    exportCurrency.Symbol = currencyConversion.Symbol;
                }

                string delimiter = string.Empty;
                string serverPath = string.Empty;
                int columns = 0;
                bool isValid = true;
                string fileName = string.Empty;
                bool byLegalEntity = false;
                List<string> legalEntities = new List<string>();

                if (isByLegalEntity)
                {
                    byLegalEntity = true;
                }

                if (ConfigurationManager.AppSettings["Delimiter"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["Delimiter"]))
                {
                    delimiter = ConfigurationManager.AppSettings["Delimiter"].ToString();
                }
                else
                {
                    isValid = false;
                }

                if (ConfigurationManager.AppSettings["ServerPath"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ServerPath"]))
                {
                    //string strInvoiceNumber = string.Empty;
                    //strInvoiceNumber = GetInvoiceNumberFromInvoiceNumberDropDownList(ddlInvoiceNumber.SelectedItem.Text);

                    if (byLegalEntity)
                    {
                        fileName = invoiceNumber;

                        CRMAccountBL objCRMAccountBL = new CRMAccountBL(strConnectionString);
                        List<CRMAccount> lstCRMAccount = new List<CRMAccount>();
                        lstCRMAccount = objCRMAccountBL.GetAssoicateAccountsByInvoiceTypeId(invoiceTypeId);
                        if (lstCRMAccount != null && lstCRMAccount.Count > 0)
                        {
                            legalEntities = lstCRMAccount.Select(x => x.AccountNumber.ToString()).ToList();
                        }
                        //serverPath = ConfigurationManager.AppSettings["ServerPath"].ToString() + fileName;
                        serverPath = ConfigurationManager.AppSettings["ServerPath"].ToString();
                    }
                    else
                    {
                        //string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //fileName = strInvoiceNumber + "_" + dateTime + ".csv";
                        fileName = invoiceNumber;
                        //serverPath = ConfigurationManager.AppSettings["ServerPath"].ToString() + fileName;
                        serverPath = ConfigurationManager.AppSettings["ServerPath"].ToString();
                    }
                }
                else
                {
                    isValid = false;
                }

                if (ConfigurationManager.AppSettings["Columns"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["Columns"]))
                {
                    columns = Convert.ToInt32(ConfigurationManager.AppSettings["Columns"]);
                }
                else
                {
                    isValid = false;
                }

                DataEncryptor dataEncryptor = new DataEncryptor();
                List<MBM.Entities.InvoiceType> invoiceTypeList = new List<MBM.Entities.InvoiceType>();

                

                InvoiceBL objInvoiceBL = new InvoiceBL(strConnectionString);
                invoiceTypeList = objInvoiceBL.GetInvoiceTypes();
                MBM.Entities.InvoiceType objInvoiceType = invoiceTypeList.Where(x => x.ID == invoiceTypeId).FirstOrDefault();


                if (objInvoiceType != null && isValid == true)
                {
                    if (!string.IsNullOrEmpty(objInvoiceType.DefaultFTP) && !string.IsNullOrEmpty(objInvoiceType.FTPUserName) && !string.IsNullOrEmpty(objInvoiceType.FTPPassword))
                    {
                        objInvoiceType.FTPPassword = dataEncryptor.Decrypt(objInvoiceType.FTPPassword);
                        InvoiceBL invoiceBL = new InvoiceBL(strConnectionString);
                        invoiceBL.ExportBillingFile(invoiceId, serverPath, delimiter, columns, exportCurrency, objInvoiceType, fileName, byLegalEntity, legalEntities, userName, invoiceBL);

                        //Update Billing Export File Status
                        invoiceBL.UpdateInvoiceExportStatus(invoiceId);

                        //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.ExportInvoiceFile);
                        ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
                        objStatus.InvoiceNumber = invoiceNumber;
                        objStatus.CompareToCRM = true;
                        objStatus.ViewChange = true;
                        objStatus.ApproveChange = true;
                        objStatus.SyncCRM = true;
                        objStatus.ImportCRMData = true;
                        objStatus.ProcessInvoice = true;
                        objStatus.ExportInvoiceFile = true;

                        //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);                    
                        ProcessWorkflowStatusBL processWorkflowStatusBL = new ProcessWorkflowStatusBL(strConnectionString);
                        processWorkflowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(objStatus);


                        isExported = true;
                        //if (byLegalEntity)
                        //{
                        //    notification = "Exported Invoice files by Legal Entity to FTP sucessfully";
                        //}
                        //else
                        //{
                        //    notification = string.Format("{0} File exported to FTP sucessfully", fileName);
                        //}
                    }
                    else
                    {
                        isExported = false;
                        //notification = string.Format("There is no FTP path defined for {0}", invoiceNumber);
                    }
                }                                
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return isExported;
        }

        private CurrencyConversion GetCurrencyConversion(int invoiceId,string userName)
        {
            CurrencyConversion currencyConversion = new CurrencyConversion();

            try
            {
                Invoice invoiceDetails = GetInvoiceNumberDetails(invoiceId);
                int exportCurrencyId = invoiceDetails.ExportCurrencyID;
                List<CurrencyConversion> _importCurrencies = _billing.Configurations.Currencies(DateTime.Now);
                currencyConversion = _importCurrencies.Where(m => m.ID == exportCurrencyId).FirstOrDefault();
                return currencyConversion;
            }
            catch (Exception ex)
            {
                LogException(ex, "GetCurrencyConversion",userName);
                return currencyConversion;
            }
        }

        private Invoice GetInvoiceNumberDetails(int invoiceId)
        {
            Invoice invoiceDetail = new Invoice();
            _billing = new BillingEngine(strConnectionString);

            //int invoiceId = Convert.ToInt32(ddlInvoiceNumber.SelectedValue.ToString());
            invoiceDetail = _billing.InvoiceBL.FetchInvoiceById(invoiceId);
            return invoiceDetail;
        }

        private void LogException(Exception exception, string methodName,string userName)
        {
            AppExceptionBL objAppExceptionBL = new AppExceptionBL(strConnectionString);
            AppException objAppException = new AppException();

            objAppException.ErrorMessage = exception.Message;
            objAppException.LoggedInUser = userName;
            objAppException.MethodName = methodName;
            objAppException.StackTrace = exception.StackTrace;

            objAppExceptionBL.InsertApplicationException(objAppException);
        }
    }
}
