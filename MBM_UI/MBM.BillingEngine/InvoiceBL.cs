using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Principal;
using System.IO;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;

namespace MBM.BillingEngine
{
	public class InvoiceBL
	{
		private DataFactory _dal;
		private Logger _logger;
		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
		public InvoiceBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        /// <summary>
        /// Inserts New Invoice Type
        /// </summary>
        /// <param name="invoiceType"></param>
        public int InsertInvoiceType(InvoiceType invoiceType)
        {
            int result = 0;
            try
            {
               result =  _dal.Invoice.InsertInvoiceType(invoiceType);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;

        }

        /// <summary>
        /// Updates Invoice Type
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public int UpdateInvoiceType(InvoiceType invoiceType)
        {
            int result = 0;
            try
            {
                result = _dal.Invoice.UpdateInvoiceType(invoiceType);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

		/// <summary>
		/// Get a list of Invoice Types used by the system
		/// </summary>
		/// <returns>invoice types</returns>
		public List<InvoiceType> GetInvoiceTypes()
		{
			try
			{
				return _dal.Invoice.MBMGetInvoiceTypeList();
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -769257);
				throw;
			}
		}

        //SERO-1582

        #region Start Sero3511
        public List<AscendonGLDepartmentCodes> GetAscendonGLDepartmentCodes()
        {
            try
            {
                return _dal.Invoice.Get_MBMGetAscendonGLDepartmentCodeList();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }
        #endregion End Sero3511


        public List<InvoiceType> GetBillingSystem(int? invoiceId)
        {
            try
            {
                return _dal.Invoice.GetBillingSystem(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }
        /// <summary>
        /// Get a list of Invoice Types of SOO used by the system...CBE9179
        /// </summary>
        /// <returns>invoice types</returns>
        public List<InvoiceType> GetInvoiceTypesSOO()
        {
            try
            {
                return _dal.Invoice.MBMGetInvoiceTypeListSOO();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }


        /// <summary>
        /// Get a list of Accounts from SOO used by the system...CBE11609
        /// </summary>
        /// <returns>invoice types</returns>
        public List<InvoiceType> GetAccListsSOO()
        {
            try
            {
                return _dal.Invoice.MBMGetAcctListSOO();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// Get a list of Invoice Types used by the system
        /// </summary>
        /// <returns>invoice types</returns>
        public List<InvoiceType> GetInvoiceTypesCustomerAdmin(string userId)
        {
            try
            {
                return _dal.Invoice.MBMGetInvoiceTypeListCustomerAdmin(userId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Get a list of Invoice Types of SOO used by the system...CBE9179
        /// </summary>
        /// <returns>invoice types</returns>
        public List<InvoiceType> GetInvoiceTypesSOOCustomerAdmin(string userId)
        {
            try
            {
                return _dal.Invoice.MBMGetInvoiceTypeListSOOCustomerAdmin(userId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Get a list of Accounts from SOO used by the system...CBE11609
        /// </summary>
        /// <returns>invoice types</returns>
        public List<InvoiceType> GetAccListsSOOCustomerAdmin(string userId)
        {
            try
            {
                return _dal.Invoice.MBMGetAcctListSOOCustomerAdmin(userId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

		/// <summary>
		/// Returns an invoice by its ID - or default invoice if no match is found
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <returns>invoice</returns>
		public Invoice FetchInvoiceById(int invoiceID)
		{
			try
			{
				return _dal.Invoice.GetInvoice(invoiceID);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -188224);
				throw;
			}
		}

		/// <summary>
		/// Returns a list of invoices that match a selected type
		/// </summary>
		/// <param name="invoiceType">limited type of invoice</param>
		/// <returns>list of invoices</returns>
		public List<Invoice> FetchInvoicesByType(int invoiceType)
		{
			try
			{
				return _dal.Invoice.GetInvoices(invoiceType);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -704310);
				throw;
			}
		}

		/// <summary>
        /// Returns a list of all invoices that match a SOO
        /// </summary>
        /// <param name="invoiceType">limited type of invoice</param>
        /// <returns>list of invoices</returns>
        public List<Invoice> FetchInvoicesByType_All(int invoiceType)
        {
            try
            {
                return _dal.Invoice.GetInvoices_All(invoiceType);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -704310);
                throw;
            }
        }

		/// <summary>
		/// Returns a list of invoices in the data store
		/// </summary>
		/// <returns>list of invoices</returns>
		public List<Invoice> FetchAllInvoices()
		{
			try
			{
				List<Invoice> entities = new List<Invoice>();
                //to fetch all invoicetype assuming max to 30
                int[] tempArray = new int[30];
                for (int i = 1; i < tempArray.Length; i++)
                {
                    tempArray[i - 1] = i;
                }
				foreach (int e in tempArray)
				{
					entities.AddRange(_dal.Invoice.GetInvoices(e));
				}

				return entities;
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -704310);
				throw;
			}
		}

		/// <summary>
		/// Generate a new invoice number
		/// </summary>
		/// <param name="prefix">limiting prefix or empty for any prefix</param>
		/// <returns>invoice number</returns>
		public string GetNextGeneratedInvoiceNumber(
			int invoiceType,
			string prefix
			)
		{
			string invoiceNumber = string.Empty;

			try
			{
				List<Entities.Invoice> invoices = new List<Entities.Invoice>();
				invoices = _dal.Invoice.GetInvoices(invoiceType);

				// Get highest invoice number where prefix matches and remaining is a valid number.

               
				Int64 highestInvoiceNumber = 10000;
                //if (prefix.ToUpper() == "DV")       //CRQ:CRQ700001314705
                //    highestInvoiceNumber = 1000;

				foreach (Invoice i in invoices)
				{
					string selectedInvoice = i.InvoiceNumber;

					if (selectedInvoice.IndexOf(prefix, 0, prefix.Length) != -1)
					{
						selectedInvoice = selectedInvoice.Replace(prefix, String.Empty);

						Int64 stripedInvoiceNumber;
						if (Int64.TryParse(selectedInvoice, out stripedInvoiceNumber))
						{
							if (stripedInvoiceNumber > highestInvoiceNumber)
							{
								highestInvoiceNumber = stripedInvoiceNumber;
							}
						}
					}

				}

				invoiceNumber = (highestInvoiceNumber + 1).ToString();
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -962303);
				invoiceNumber = string.Empty;
			}

			return invoiceNumber;
		}

		/// <summary>
		/// Update or Insert the invoice into the data storage
		/// </summary>
		/// <param name="invoice">invoice</param>
		/// <param name="changeUser">user login who is requesting the change</param>
		public void UpsertInvoice(
			Invoice invoice,
			string changeUser
			)
		{
			try
			{
				_dal.Invoice.UpsertInvoice(invoice, changeUser);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -915034);
				throw;
			}
		}

        public void InsertInvoice(
            Invoice invoice,
            string changeUser
            )
        {
            try
            {
                _dal.Invoice.InsertInvoice(invoice, changeUser);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }

		/// <summary>
		/// Update data stored about imported files. This does not import the data - only
		/// holds the data record
		/// </summary>
		/// <param name="fileToImport">file to import into the system</param>
		/// <param name="changeUser">user login who is requesting the change</param>
		public void UpsertFileUpload(
			UploadedFile fileToImport,
			string changeUser
			)
		{
			try
			{
				if (fileToImport == null)
				{
					_logger.InsertProcessLogItem(
						new ProcessLogItemEntity()
						{
							CreatedBy = changeUser,
							Comment = "Imported file data header is empty",
						});
					return;
				}

				_dal.Invoice.UpsertFileUpload(fileToImport, changeUser);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -609628);
				throw;
			}
		}

		/// <summary>
		/// This method deletes an invoice from the database.  Cascade delete will remove it from all other tables
		/// except archieve tables.  Because the invoice is removed we cannot save a process log but we can save
		/// an application log.
		/// </summary>
		/// <param name="invoice"></param>
		/// <returns></returns>
		public bool DeleteInvoice(Invoice invoice)
		{
			bool result = false;

			DateTime date = DateTime.Now;

			ApplicationLogEntity appLogEntity = new ApplicationLogEntity();

			try
			{
				_dal.Invoice.DeleteInvoice(invoice, WindowsIdentity.GetCurrent().Name);

				appLogEntity.LogType = ApplicationLogType.ApplicationRaised;
				appLogEntity.InvoiceNumber = invoice.InvoiceNumber;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.CodeLocation = "DeleteInvoice()";
				appLogEntity.Comment = "Successful Delete Of Invoice# " + invoice.InvoiceNumber;

				result = true;
			}
			catch (Exception e)
			{
				result = false;

				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceNumber = invoice.InvoiceNumber;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "DeleteInvoice()";
				appLogEntity.Comment = "Failed to Delete Invoice# " + invoice.InvoiceNumber;
			}

			_logger.LogExceptionToDatabase(appLogEntity);

			return result;
		}

		/// <summary>
		/// Export the Billing information as a delimited file
		/// </summary>
		/// <param name="invoice">invoice to export</param>
		/// <param name="newFileName">full path and filename to export into</param>
		/// <param name="delimiter">delimiter to use</param>
		/// <param name="columnCount">number of columns to fill out if short</param>
		/// <param name="exportCurrency">currency to convert into if necessary</param>
		public void ExportBillingFile(int invoiceID, string serverPath, string delimiter, int columnCount, CurrencyConversion exportCurrency, InvoiceType objInvoiceType, string fileName, bool byLegalEntity, List<string> legalEntities, string userName, InvoiceBL objInvoiceBL)
		{
			ProcessLogItemEntity processLogItemEntity = new ProcessLogItemEntity();

			try
			{
				// Send export string to file.
				string supportFileName = String.Empty;
                string[] arr = serverPath.Split('\\');
				for (int a = 0; a < arr.Length - 1; a++)
				{
					supportFileName += arr[a] + '\\';
				}

				FileExport export = new FileExport(ConnectionString);

                if (byLegalEntity)
                {
                    export.CreateBillingFileByLegalEntity(invoiceID, delimiter, columnCount, exportCurrency, serverPath, objInvoiceType, fileName, legalEntities, userName, objInvoiceBL);
                }
                else 
                {
                    export.CreateBillingFile(invoiceID, delimiter, columnCount, exportCurrency, serverPath, objInvoiceType, fileName, userName, objInvoiceBL);
                }
				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.Exception = e;
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.CodeLocation = "ExportBillingCSVFile()";
				appLogEntity.Comment = "Failed to export billing file for Invoice# " + invoiceID;
				appLogEntity.UserName = WindowsIdentity.GetCurrent().Name;

				_logger.LogExceptionToDatabase(appLogEntity);

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}
			finally
			{
				processLogItemEntity.InvoiceID = invoiceID;
				processLogItemEntity.ProcessDateTime = DateTime.Now;
				processLogItemEntity.ProcessName = "Step 4: Export billing file";
				processLogItemEntity.CreatedBy = WindowsIdentity.GetCurrent().Name;
				_logger.InsertProcessLogItem(processLogItemEntity);
			}
		}

        /// <summary>
        /// Start:17-12-2014
        ///  Returns list of customer numbers
        /// </summary>

        public List<Customers> FetchCoustomerNumbers()
        {
            try
            {
                return _dal.Invoice.GetCustomerNumbers();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -704310);
                throw;
            }
        }


        /// <summary>
        /// Export the Billing information as a delimited file
        /// </summary>
        /// <param name="customerNumber">customer number to export</param>
        /// <param name="newFileName">full path and filename to export into</param>
        /// <param name="delimiter">delimiter to use</param>
        /// <param name="columnCount">number of columns to fill out if short</param>
        /// <param name="exportCurrency">currency to convert into if necessary</param>
        public void ExportBillingDataFile(
            int CustomerNumber,
            string newFileName,
            string delimiter,
            int columnCount,
            CurrencyConversion exportCurrency,
            bool separateGatewayBill,
            bool generateLoyaltyCreditReport,
            string typeofMode
            )
        {
            ProcessLogItemEntity processLogItemEntity = new ProcessLogItemEntity();

            try
            {
                // Send export string to file.
                string supportFileName = String.Empty;
                string[] arr = newFileName.Split('\\');
                for (int a = 0; a < arr.Length - 1; a++)
                {
                    supportFileName += arr[a] + '\\';
                }

                FileExport export = new FileExport(ConnectionString);

                if (separateGatewayBill)
                {
                 //   export.CreateBillingDataFile(CustomerNumber, delimiter, columnCount, exportCurrency, newFileName, RecordTypesDAL.RECORDS_FILTER.NO_GATEWAYS);
                    export.CreateBillingDataFile(CustomerNumber, delimiter, columnCount, exportCurrency, supportFileName + exportCurrency.Code + " Gateway Bill.csv", RecordTypesDAL.RECORDS_FILTER.GATEWAYS_ONLY,typeofMode);
                }
                else
                {
                    export.CreateBillingDataFile(CustomerNumber, delimiter, columnCount, exportCurrency, newFileName, RecordTypesDAL.RECORDS_FILTER.ALL, typeofMode);
                }

                  export.SaveAssetFile(CustomerNumber, delimiter, supportFileName + "_RecordType6.csv");
              
                processLogItemEntity.ProcessResult = Constants.COMPLETED;
            }
            catch (Exception e)
            {
                ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
                appLogEntity.Exception = e;
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.InvoiceID = CustomerNumber;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.CodeLocation = "ExportBillingCSVFile()";
                appLogEntity.Comment = "Failed to export billing file for Customer# " + CustomerNumber;
                appLogEntity.UserName = WindowsIdentity.GetCurrent().Name;

                _logger.LogExceptionToDatabase(appLogEntity);

                processLogItemEntity.ProcessResult = Constants.FAILED;
            }
            finally
            {
                processLogItemEntity.InvoiceID = CustomerNumber;
                processLogItemEntity.ProcessDateTime = DateTime.Now;
                processLogItemEntity.ProcessName = "Step 4: Export billing file";
                processLogItemEntity.CreatedBy = WindowsIdentity.GetCurrent().Name;
                _logger.InsertProcessLogItem(processLogItemEntity);
            }
        }

        
        /// <summary>
        /// CRQ CRQ700001314707
        /// This method Process the data using [usp_CBADTaxesData],[usp_GEGCOMMultiLocationSummary] and [usp_GEGCOMCBADCRMData] stopred prcoedures
        /// and load data into CBAD_Taxes,GEGCOMMultiLocationSummary and CBAD_LongDistance tables.
        /// except archieve tables.  Because the invoice is removed we cannot save a process log but we can save
        /// an application log.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public void ProcessUSBillingData()
        {
            DateTime date = DateTime.Now;

            ApplicationLogEntity appLogEntity = new ApplicationLogEntity();

            try
            {
                _dal.Invoice.ProcessUSBillingData(WindowsIdentity.GetCurrent().Name);

                appLogEntity.LogType = ApplicationLogType.ApplicationRaised;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.CodeLocation = "ProcessUSBillingData()";
               
            }
            catch (Exception e)
            {
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.Exception = e;
                appLogEntity.CodeLocation = "ProcessUSBillingData()";
            }

            _logger.LogExceptionToDatabase(appLogEntity);

        }

        /// <summary>
        /// Insert Exported file
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="exportFileName"></param>
        /// <param name="exportFilePath"></param>
        /// <param name="exportedBy"></param>
        public void InsertInvoiceFileExports(
           ExportedFile exportedFile
           )
        {
            try
            {
                _dal.Invoice.InsertInvoiceFileExports(exportedFile);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }

        public List<ExportedFile> GetInvoiceFileExports(int InvoiceId)
        {
            try
            {
                return _dal.Invoice.GetInvoiceFileExports(InvoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -704310);
                throw;
            }
        }

        public MBMAutomateStatus GetMBMAutomateStatus(int InvoiceId)
        {
            try
            {
                return _dal.Invoice.GetMBMAutomateStatus(InvoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -704310);
                throw;
            }
        }

        /// <summary>
        /// Updates Invoice Type
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public int UpdateInvoiceExportStatus(int intInvoiceId)
        {
            int result = 0;
            try
            {
                result = _dal.Invoice.UpdateInvoiceExportStatus(intInvoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

    }
}
