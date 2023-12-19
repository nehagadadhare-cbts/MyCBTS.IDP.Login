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
	public class FileImport
	{
		private DataFactory _dal;
		private Logger _logger;
		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
		public FileImport(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

		/// <summary>
		/// Import the CBAD long distance File into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="BAN">Billing account number to use</param>
		/// <param name="billDate">Invoice date of the bill in MMDDYYYY standard. This is the first date of
		/// the month of the invoice, not the date the bill is issued.</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportCBADFile(
			int invoiceID,
			string filePath,
			string BAN,
			string billDate,
			string caller
			)
		{
			bool result = false;

			try
			{
				if (File.Exists(filePath))
				{
					int artifactsFound = 0;
					int status = 1; // _dal.Invoice.GetArtifactCountForInvoiceImport(invoiceID, out artifactsFound);
					if (status > 0 && artifactsFound == 0)
					{
						FileImport_CBADLongDistance cbadImport = new FileImport_CBADLongDistance(ConnectionString);
						result = cbadImport.Import(invoiceID, filePath, BAN, billDate, caller);
					}
					else
					{
						result = false;

						_logger.LogExceptionToDatabase(new ApplicationLogEntity()
						{
							LogType = ApplicationLogType.ApplicationRaised,
							UserName = caller,
							InvoiceID = invoiceID,
							ExceptionDateTime = DateTime.Now,
							CodeLocation = "ImportCBADFile()",
							Comment = "This invoice has left over artifacts that must be removed before import. " + "CBADFilePath = " + filePath,
						});
					}
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportCBADFile()",
						Comment = "Returned false for FileExists(invoice.CBADFilePath) " + "CBADFilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessName = "Import CBAD file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportCBADFile()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		public bool RollbackCBADFileImport(Invoice invoice)
		{
			bool result = false;

            DateTime date = new DateTime();
			var processLogItemEntity = new ProcessLogItemEntity();

			try
			{
				_dal.Invoice.RollbackCBADImport(invoice);
				processLogItemEntity.ProcessResult = Constants.COMPLETED;

				//invoice.UploadedFiles.Item(invoice.ID, UploadedFileType.CBAD).UploadedStatus = "Rolled Back";
				invoice.LastAction = "Rolled Back CBAD Import";
			}
			catch (Exception e)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					Exception = e,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceNumber = invoice.InvoiceNumber,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "RemoveCBADImport()",
					Comment = "Failed to Rollback import for Invoice# " + invoice.InvoiceNumber,
					UserName = WindowsIdentity.GetCurrent().Name,
				});

				processLogItemEntity.ProcessResult = Constants.FAILED;
				//invoice.UploadedFiles.Item(invoice.ID, UploadedFileType.CBAD).UploadedStatus = "(Failed) Roll Back)";
				invoice.LastAction = "(Failed) Roll Back of CBAD Import";
			}

			processLogItemEntity.InvoiceNumber = invoice.InvoiceNumber;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Step 1: Roll Back CBAD Import";
			processLogItemEntity.CreatedBy = WindowsIdentity.GetCurrent().Name;
			_logger.InsertProcessLogItem(processLogItemEntity);

			//invoice.UploadedFiles.Item(invoice.ID, UploadedFileType.CBAD).LastUpdatedDate = date.ToString();
			invoice.LastUpdatedDate = date;
			invoice.LastUpdatedBy = WindowsIdentity.GetCurrent().Name;
			//UpsertInvoice(invoice, WindowsIdentity.GetCurrent().Name);

			return result;
		}

		public static void ValidateHeaderRecord(ref DataSet ds, string tableName, int colNumber, string colName)
		{
			if (ds.Tables[tableName].Columns[colNumber].ColumnName != colName)
			{
				// Insert the proper column name
				ds.Tables[tableName].Columns[colNumber].ColumnName = colName;
			}
		}
		public static void ValidateHeaderRecord(ref DataTable dt, int colNumber, string colName)
		{
			if (dt.Columns[colNumber].ColumnName != colName)
			{
				// Insert the proper column name
				dt.Columns[colNumber].ColumnName = colName;
			}
		}

		/// <summary>
		/// Import the Long Distance Taxes File into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportTaxFile(
			int invoiceID,
			string filePath,
			string caller
			)
		{
			bool result = false;

			try
			{
				if (File.Exists(filePath))
				{
					int artifactsFound = 0;
					int status = _dal.Invoice.GetArtifactCountForTaxImport(invoiceID, out artifactsFound);
					if (status > 0 && artifactsFound == 0)
					{
						FileImport_CBADTaxFile taxImport = new FileImport_CBADTaxFile(ConnectionString);
						result = taxImport.Import(invoiceID, filePath, caller);
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						UserName = caller,
						LogType = ApplicationLogType.ApplicationRaised,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportTaxFile()",
						Comment = "Returned false for FileExists(invoice.TaxFilePath) " + "TaxFilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessName = "Import Tax file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportTaxFile()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		/// <summary>
		/// Rollback a tax file import step
		/// </summary>
		/// <param name="invoiceID">invoice to rollback</param>
		/// <param name="callerLogin">login of caller</param>
		public void RollbackTaxFileImport(
			int invoiceID,
			string callerLogin
			)
		{
			ProcessLogItemEntity processLogItemEntity = new ProcessLogItemEntity();

			try
			{
				_dal.Invoice.RollbackTaxFileImport(invoiceID, callerLogin);
				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					Exception = e,
					CodeLocation = "RemoveTaxFileImport()",
					Comment = "Failed to Rollback Tax File Import for Invoice# " + invoiceID,
				});

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}

			processLogItemEntity.InvoiceID = invoiceID;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Roll Back Tax File Import";
			processLogItemEntity.CreatedBy = callerLogin;
			_logger.InsertProcessLogItem(processLogItemEntity);
		}

		/// <summary>
		/// Import the One Source Networks file into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="currencyID">currency used for the import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportOneSourceLongDistance(
			int invoiceID,
			int currencyID,
			string filePath,
			string caller
			)
		{
			bool result = false;

			try
			{
				if (File.Exists(filePath))
				{
					FileImport_OSNLongDistance osnImport = new FileImport_OSNLongDistance(ConnectionString);
					result = osnImport.Import(invoiceID, filePath, caller, currencyID);
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportOneSourceLongDistance()",
						Comment = "Returned false for FileExists(invoice.ImportOneSourceLongDistance) " + "OneSourceLongDistanceFilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessName = "Import OneSourceNetworks LongDistance file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportOneSourceLongDistance()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		/// <summary>
		/// Import the One Source Networks Usage Summary file into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="currencyID">currency used for the import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportOneSourceUsageSummary(
			int invoiceID,
			int currencyID,
			string filePath,
			string caller
			)
		{
			bool result = false;

			try
			{
				if (File.Exists(filePath))
				{
					FileImport_OSNUsageSummary osnSummary = new FileImport_OSNUsageSummary(ConnectionString);
					result = osnSummary.Import(invoiceID, filePath, caller, currencyID);
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportOneSourceUsageSummary()",
						Comment = "Returned false for FileExists(invoice.ImportOneSourceUsageSummary) " + "OneSourceUsageSummaryFilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessName = "Import OneSourceNetworks UsageSummary file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportOneSourceUsageSummary()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		/// <summary>
		/// Import the One Source Networks Flat Rate Summary file into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="currencyID">currency used for the import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportOneSourceFlatRate(
			int invoiceID,
			int currencyID,
			string filePath,
			string caller
			)
		{
			bool result = false;

			try
			{
				if (File.Exists(filePath))
				{
					FileImport_OSNFlatRate osnImport = new FileImport_OSNFlatRate(ConnectionString);
					result = osnImport.Import(invoiceID, filePath, caller, currencyID);
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportOneSourceFlatRate()",
						Comment = "Returned false for FileExists(invoice.ImportOneSourceFlatRate) " + "OneSourceFlatRateFilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessName = "Import OneSourceNetworks FlatRate file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportOneSourceFlatRate()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		/// <summary>
		/// Import the One-Time Charges File into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportOneTimeChargesFile(
			int invoiceID,
			string filePath,
			string caller
			)
		{
			bool result = false;

			try
			{
				if (File.Exists(filePath))
				{
					FileImport_OneTimeCharges otcImport = new FileImport_OneTimeCharges(ConnectionString);
					result = otcImport.Import(invoiceID, filePath, caller);
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportOneTimeChargesFile()",
						Comment = "Returned false for FileExists(invoice.Step1_OneTimeChargesImportStatus) " + "OneTimeChargesFilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessName = "Import OneTimeCharges file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportOneTimeChargesFile()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		/// <summary>
		/// Import the MLS File
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportGEGCOMMultiLocationSummaryFile(
			int invoiceID,
			string filePath,
			string caller
			)
		{
			bool result = false;
			DateTime date = DateTime.Now;

			try
			{
				if (File.Exists(filePath))
				{
					FileImport_MLS mlsImport = new FileImport_MLS(ConnectionString);
					result = mlsImport.Import(invoiceID, filePath, caller);
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportGEGCOMMultiLocationSummaryFile()",
						Comment = "Returned false for FileExists(invoice.GEGCOMMultiLocationSummaryFilePath) " + "GEGCOMMultiLocationSummaryFilePath = " + Constants.FAILED,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = date,
					ProcessName = "Import GEGCOMMultiLocationSummaryFilePath file",
					CreatedBy = caller,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportGEGCOMMultiLocationSummaryFile()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

		/// <summary>
		/// Import EC500 File into datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool ImportEC500File(
			int invoiceID,
			string filePath,
			string caller
			)
		{
			bool result = false;
			try
			{
				if (File.Exists(filePath))
				{
					FileImport_EC500 ecImport = new FileImport_EC500(ConnectionString);
					result = ecImport.Import(invoiceID, filePath, caller);
				}
				else
				{
					result = false;

					_logger.LogExceptionToDatabase(new ApplicationLogEntity()
					{
						LogType = ApplicationLogType.ApplicationRaised,
						UserName = caller,
						InvoiceID = invoiceID,
						ExceptionDateTime = DateTime.Now,
						CodeLocation = "ImportEC500File()",
						Comment = "Returned false for FileExists(invoice.EC500FilePath) " + "EC500FilePath = " + filePath,
					});
				}

				_logger.InsertProcessLogItem(new ProcessLogItemEntity()
				{
					InvoiceID = invoiceID,
					ProcessDateTime = DateTime.Now,
					ProcessResult = (result) ? Constants.COMPLETED : Constants.FAILED,
					ProcessName = "Import EC500 file",
					CreatedBy = caller,
				});
			}
			catch (Exception ex)
			{
				result = false;

				_logger.LogExceptionToDatabase(new ApplicationLogEntity()
				{
					UserName = caller,
					LogType = ApplicationLogType.SystemRaised,
					InvoiceID = invoiceID,
					ExceptionDateTime = DateTime.Now,
					CodeLocation = "ImportEC500File()",
					Comment = "System Exception",
					Exception = ex,
				});
			}

			return result;
		}

        /// <summary>
        /// CRQ #CRQ700001314707  
        /// Import International video Snapshot data from VIPR to GCOM local.
        /// </summary>
        /// <param name="caller">Invoice number</param>
        /// <param name="caller">login of calling account</param>
        /// <returns>true on success or false on failure</returns>
        public bool ImportInternationalSnapshotData(string Notes, string UserName)
        {
           bool result = false;
           try
           {
               result = _dal.RecordTypes.ImportIntenationalSnapshotData(Notes);
           }
           catch (Exception ex)
           {
              
               _logger.LogExceptionToDatabase(new ApplicationLogEntity()
               {
                   UserName = UserName,
                   LogType = ApplicationLogType.SystemRaised,
                   ExceptionDateTime = DateTime.Now,
                   CodeLocation = "ImportIntenationalSnapshotData()",
                   Comment = "System Exception",
                   Exception = ex,
               });
           }
                 return result;
        }

        /// <summary>
        /// CRQ #CRQ700001314707  
        /// Import US video Snapshot data from VIPR to GCOM local.
        /// </summary>
        /// <param name="caller">login of calling account</param>
        /// <returns>true on success or false on failure</returns>
        public bool ImportUSSnapshotData(string Notes,string UserName)
        {
            bool result = false;
            try
            {
                result = _dal.RecordTypes.ImportUSSnapshotData(Notes);
            }
            catch (Exception ex)
            {

                _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                {
                    UserName = UserName,
                    LogType = ApplicationLogType.SystemRaised,
                    ExceptionDateTime = DateTime.Now,
                    CodeLocation = "ImportUSSnapshotData()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }
            return result;
        }
    }
}
