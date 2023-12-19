using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using MBM.Entities;
using MBM.DataAccess;

namespace MBM.DataAccess
{
	public class InvoiceDAL
	{
		#region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
		public InvoiceDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}

		#endregion

		#region Invoices

		/// <summary>
		/// Get a full list of all configuration settings
		/// </summary>
		/// <param name="entities"></param>
		/// <returns>list of configuration entities found</returns>
		public List<InvoiceType> GetInvoiceTypeList()
		{
			List<InvoiceType> entities = new List<InvoiceType>();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<InvoiceType_GetResult> results = db.InvoiceType_Get();

					foreach (InvoiceType_GetResult r in results)
					{
						InvoiceType entity = new InvoiceType()
						{
							ID = (int)r.ID,
							Name = r.InvoiceProfile,
							Prefix = r.Prefix,
							BAN = r.BAN,
							VendorName = r.VendorName,
							ImportCurrencyDefault = r.ImportCurrencyDefault,
							ExportCurrencyDefault = r.ExportCurrencyDefault,
						};

						if (entity != null)
						{
							entities.Add(entity);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -524044);
				throw;
			}

			return entities;
		}


        /// <summary>
        /// Get a full list of all configuration settings
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public List<InvoiceType> MBMGetInvoiceTypeList()
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceTypeResult> results = db.get_InvoiceType();

                    foreach (get_InvoiceTypeResult r in results)
                    {
                       InvoiceType  entity = new InvoiceType()
                        {
                            ID = (int)r.iInvoiceTypeId,
                            Name = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            BAN = r.sBAN,
                            VendorName = r.sVendorName,
                            ImportCurrencyDefault = r.sImportCurrencyDefault,
                            ExportCurrencyDefault = r.sExportCurrencyDefault,
                            DefaultFTP= r.sDefaultFTP,
                            FTPUserName=r.sFTPUsername,
                            FTPPassword=r.sFTPPassword,
                            IsAutoPreBilling=r.bIsAutoPreBill.GetValueOrDefault(),
                            DaysBeforeBillCycle=r.iDaysBeforeBillCycle.GetValueOrDefault(),
                            IsAutoPostBilling=r.bIsAutoPostBill.GetValueOrDefault(),
                            DaysAfterBillCycle=r.iDaysAfterBillCycle.GetValueOrDefault(),
                            OutputFileFormat=r.iOutputFileFormat.GetValueOrDefault(),//CBE_8967
                            IsSOO=r.isSOO.GetValueOrDefault(),//11609
                            EmailAddress = r.sEmailAddress,
                            EDI = r.bEDI,
                            BillingSystem = r.sBillingSystem, //SERO-1582
                            iAutomationFrequency = r.iAutomationFrequency,
                            //Sero-3511 Start
                            ContractNumber= r.sContractNumber ,
                            ContractStartDate=Convert.ToDateTime(r.dContractStartDate ),
                            ContractEndDate=Convert.ToDateTime(r.dContractEndDate ),
                            IndirectPartnerOrRepCode=r.sIndirectPartnerOrRepCode ,
                            GLDepartmentCode=r.sGLDepartmentCode ,
                            IndirectAgentRegion=r.sIndirectAgentRegion
                            //Sero-3511 End                     
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }


        //SERO-1582

        #region Start Sero3511
        public List<AscendonGLDepartmentCodes> Get_MBMGetAscendonGLDepartmentCodeList()
        {
            List<AscendonGLDepartmentCodes> entities = new List<AscendonGLDepartmentCodes>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<Get_AscendonGLDepartmentCodesResult> results = db.Get_MBMGetAscendonGLDepartmentCodes();

                    foreach (Get_AscendonGLDepartmentCodesResult data in results)
                    {
                        AscendonGLDepartmentCodes entity = new AscendonGLDepartmentCodes()
                        {
                            GLDepartmentID=data.iGLDepartmentID,
                            GLDepartmentCode=data.sGLDepartmentCode,
                            GLDepartmentName=data.sGLDepartmentName,
                            GLDepartmentValue=data.sGLDepartmentValue
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }
        #endregion Start Sero3511
        public List<InvoiceType> GetBillingSystem(int? invoiceId)
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceTypeResult> results = db.GetBillingSystem(invoiceId);

                    foreach (get_InvoiceTypeResult r in results)
                    {
                        InvoiceType entity = new InvoiceType()
                        {
                           
                            BillingSystem = r.sBillingSystem, //SERO-1582


                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        /// <summary>
        /// Get a full list of all configuration settings for SOO
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public List<InvoiceType> MBMGetInvoiceTypeListSOO()
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceType_SOOResult> results = db.get_InvoiceType_SOO();

                    foreach (get_InvoiceType_SOOResult r in results)
                    {
                        InvoiceType entity = new InvoiceType()
                        {
                            ID = (int)r.iInvoiceTypeId,
                            Name = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            BAN = r.sBAN,
                            VendorName = r.sVendorName,
                            ImportCurrencyDefault = r.sImportCurrencyDefault,
                            ExportCurrencyDefault = r.sExportCurrencyDefault,
                            DefaultFTP = r.sDefaultFTP,
                            FTPUserName = r.sFTPUsername,
                            FTPPassword = r.sFTPPassword,
                            IsAutoPreBilling = r.bIsAutoPreBill.GetValueOrDefault(),
                            DaysBeforeBillCycle = r.iDaysBeforeBillCycle.GetValueOrDefault(),
                            IsAutoPostBilling = r.bIsAutoPostBill.GetValueOrDefault(),
                            DaysAfterBillCycle = r.iDaysAfterBillCycle.GetValueOrDefault(),
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

       /// <summary>
        /// Get a full list of all accounts for SOO cbe_11609
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public List<InvoiceType> MBMGetAcctListSOO()
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceType_SOOReportResult> results = db.get_InvoiceType_SOOReport();

                    foreach (get_InvoiceType_SOOReportResult r in results)
                    {
                        InvoiceType entity = new InvoiceType()
                        {
                            ID = (int)r.iInvoiceTypeId,
                            Name = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            BAN = r.sBAN,
                            VendorName = r.sVendorName,
                            ImportCurrencyDefault = r.sImportCurrencyDefault,
                            ExportCurrencyDefault = r.sExportCurrencyDefault,
                            DefaultFTP = r.sDefaultFTP,
                            FTPUserName = r.sFTPUsername,
                            FTPPassword = r.sFTPPassword,
                            IsAutoPreBilling = r.bIsAutoPreBill.GetValueOrDefault(),
                            DaysBeforeBillCycle = r.iDaysBeforeBillCycle.GetValueOrDefault(),
                            IsAutoPostBilling = r.bIsAutoPostBill.GetValueOrDefault(),
                            DaysAfterBillCycle = r.iDaysAfterBillCycle.GetValueOrDefault(),
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        /// Get a full list of all configuration settings
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public List<InvoiceType> MBMGetInvoiceTypeListCustomerAdmin(string userId)
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceTypesbyAssociatedUserResult> results = db.get_InvoiceTypesbyAssociatedUser(userId);

                    foreach (get_InvoiceTypesbyAssociatedUserResult r in results)
                    {
                        InvoiceType entity = new InvoiceType()
                        {
                            ID = (int)r.iInvoiceTypeId,
                            Name = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            BAN = r.sBAN,
                            VendorName = r.sVendorName,
                            ImportCurrencyDefault = r.sImportCurrencyDefault,
                            ExportCurrencyDefault = r.sExportCurrencyDefault,
                            DefaultFTP = r.sDefaultFTP,
                            FTPUserName = r.sFTPUsername,
                            FTPPassword = r.sFTPPassword,
                            IsAutoPreBilling = r.bIsAutoPreBill.GetValueOrDefault(),
                            DaysBeforeBillCycle = r.iDaysBeforeBillCycle.GetValueOrDefault(),
                            IsAutoPostBilling = r.bIsAutoPostBill.GetValueOrDefault(),
                            DaysAfterBillCycle = r.iDaysAfterBillCycle.GetValueOrDefault(),
                            OutputFileFormat = r.iOutputFileFormat.GetValueOrDefault(),//CBE_8967
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        /// <summary>
        /// Get a full list of all configuration settings for SOO
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public List<InvoiceType> MBMGetInvoiceTypeListSOOCustomerAdmin(string userId)
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceTypesbyAssociatedUserSOOResult> results = db.get_InvoiceTypesbyAssociatedUserSOO(userId);

                    foreach (get_InvoiceTypesbyAssociatedUserSOOResult r in results)
                    {
                        InvoiceType entity = new InvoiceType()
                        {
                            ID = (int)r.iInvoiceTypeId,
                            Name = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            BAN = r.sBAN,
                            VendorName = r.sVendorName,
                            ImportCurrencyDefault = r.sImportCurrencyDefault,
                            ExportCurrencyDefault = r.sExportCurrencyDefault,
                            DefaultFTP = r.sDefaultFTP,
                            FTPUserName = r.sFTPUsername,
                            FTPPassword = r.sFTPPassword,
                            IsAutoPreBilling = r.bIsAutoPreBill.GetValueOrDefault(),
                            DaysBeforeBillCycle = r.iDaysBeforeBillCycle.GetValueOrDefault(),
                            IsAutoPostBilling = r.bIsAutoPostBill.GetValueOrDefault(),
                            DaysAfterBillCycle = r.iDaysAfterBillCycle.GetValueOrDefault(),
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        /// <summary>
        /// Get a full list of all accounts for SOO cbe_11609
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public List<InvoiceType> MBMGetAcctListSOOCustomerAdmin(string userId)
        {
            List<InvoiceType> entities = new List<InvoiceType>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceTypesbyAssociatedUser_SOOReportResult> results = db.get_InvoiceTypesbyAssociatedUser_SOOReport(userId);

                    foreach (get_InvoiceTypesbyAssociatedUser_SOOReportResult r in results)
                    {
                        InvoiceType entity = new InvoiceType()
                        {
                            ID = (int)r.iInvoiceTypeId,
                            Name = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            BAN = r.sBAN,
                            VendorName = r.sVendorName,
                            ImportCurrencyDefault = r.sImportCurrencyDefault,
                            ExportCurrencyDefault = r.sExportCurrencyDefault,
                            DefaultFTP = r.sDefaultFTP,
                            FTPUserName = r.sFTPUsername,
                            FTPPassword = r.sFTPPassword,
                            IsAutoPreBilling = r.bIsAutoPreBill.GetValueOrDefault(),
                            DaysBeforeBillCycle = r.iDaysBeforeBillCycle.GetValueOrDefault(),
                            IsAutoPostBilling = r.bIsAutoPostBill.GetValueOrDefault(),
                            DaysAfterBillCycle = r.iDaysAfterBillCycle.GetValueOrDefault(),
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    var compareDate = "1/1/0001 12:00:00 AM";
                    if (invoiceType.ContractStartDate.ToString() == compareDate)
                    {
                        invoiceType.ContractStartDate = DateTime.Now;
                    }
                    if (invoiceType.ContractEndDate.ToString() == compareDate)
                    {
                        invoiceType.ContractEndDate = DateTime.Now;
                    }

                    result = db.Insert_InvoiceType(invoiceType.Name, invoiceType.Prefix, invoiceType.BAN, invoiceType.VendorName,
                        invoiceType.ImportCurrencyDefault, invoiceType.ExportCurrencyDefault, invoiceType.DefaultFTP, invoiceType.FTPUserName,
                        invoiceType.FTPPassword, invoiceType.CreatedBy, invoiceType.IsAutoPreBilling, invoiceType.DaysBeforeBillCycle,
                        //invoiceType.IsAutoPostBilling, invoiceType.DaysAfterBillCycle, invoiceType.OutputFileFormat, invoiceType.IsSOO, invoiceType.EmailAddress, invoiceType.EDI);//CBE_8967
                        invoiceType.IsAutoPostBilling, invoiceType.DaysAfterBillCycle, invoiceType.OutputFileFormat, invoiceType.IsSOO, 
                        invoiceType.EmailAddress, invoiceType.EDI, invoiceType.BillingSystem, invoiceType.iAutomationFrequency, invoiceType.ContractNumber,
                        invoiceType.ContractStartDate, invoiceType.ContractEndDate, invoiceType.IndirectPartnerOrRepCode, invoiceType.GLDepartmentCode, invoiceType.IndirectAgentRegion);//CBE_8967 //SERO-1582

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Add  InvoiceType [{0}]", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
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
                var compareDate = "1/1/0001 12:00:00 AM";
                if (invoiceType.ContractStartDate.ToString() == compareDate)
                {
                    invoiceType.ContractStartDate = DateTime.Now;
                }
                if (invoiceType.ContractEndDate.ToString() == compareDate)
                {
                    invoiceType.ContractEndDate = DateTime.Now;
                }

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_InvoiceType(invoiceType.ID, invoiceType.Name, invoiceType.Prefix, invoiceType.BAN, invoiceType.VendorName, invoiceType.ImportCurrencyDefault, invoiceType.ExportCurrencyDefault, invoiceType.DefaultFTP, invoiceType.FTPUserName, invoiceType.FTPPassword, invoiceType.UpdatedBy, invoiceType.IsAutoPreBilling, invoiceType.DaysBeforeBillCycle, invoiceType.IsAutoPostBilling, invoiceType.DaysAfterBillCycle, invoiceType.OutputFileFormat, invoiceType.IsSOO, invoiceType.EmailAddress, invoiceType.EDI, invoiceType.BillingSystem, invoiceType.iAutomationFrequency,
                        invoiceType.ContractNumber,
                        invoiceType.ContractStartDate, invoiceType.ContractEndDate, invoiceType.IndirectPartnerOrRepCode, invoiceType.GLDepartmentCode, invoiceType.IndirectAgentRegion);//CBE_8967
                   // result = db.Update_InvoiceType(invoiceType.ID, invoiceType.Name, invoiceType.Prefix, invoiceType.BAN, invoiceType.VendorName, invoiceType.ImportCurrencyDefault, invoiceType.ExportCurrencyDefault, invoiceType.DefaultFTP, invoiceType.FTPUserName, invoiceType.FTPPassword, invoiceType.UpdatedBy, invoiceType.IsAutoPreBilling, invoiceType.DaysBeforeBillCycle, invoiceType.IsAutoPostBilling, invoiceType.DaysAfterBillCycle, invoiceType.OutputFileFormat, invoiceType.BillingSystem, invoiceType.iAutomationFrequency);//CBE_8967//SERO-1582

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update  InvoiceType [{0}]", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        } 

		/// <summary>
		/// Retrieve a list of all Invoices and the files that have been uploaded for them
		/// </summary>
		/// <param name="invoiceType">Type of Invoices to retrieve</param>
		/// <returns>entities found</returns>
		public List<Invoice> GetInvoices(int invoiceType)
		{
			List<Invoice> entities = new List<Invoice>();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<get_InvoiceNumbersByTypeIdResult> results = db.get_InvoiceNumbersByTypeId((int)invoiceType);

                    foreach (get_InvoiceNumbersByTypeIdResult r in results)
					{
						Invoice entity = new Invoice()
						{
							CreatedBy = r.sCreatedBy,
							CreatedDate = r.dtCreatedDate,
							ID = r.iInvoiceId,
							TypeOfBill = (int)r.iInvoiceTypeId,
							InvoiceNumber = r.sInvoiceNumber,
							LastAction = r.sLastAction,
							LastUpdatedBy = r.sLastUpdatedBy,
							LastUpdatedDate = r.dtLastUpdatedDate,
							Status = r.sStatus,
							BillingMonth = r.iBillingMonth,
							BillingYear = r.iBillingYear,
							DefaultImportCurrencyID = r.iDefaultImportCurrencyID,
							RecordType1DateTime = r.dtRecordType1_DateTime,
							RecordType1Status = r.sRecordType1_Status,
							RecordType2DateTime = r.dtRecordType2_DateTime,
							RecordType2Status = r.sRecordType2_Status,
							RecordType3DateTime = r.dtRecordType3_DateTime,
							RecordType3Status = r.sRecordType3_Status,
							RecordType4DateTime = r.dtRecordType4_DateTime,
							RecordType4Status = r.sRecordType4_Status,
							RecordType5DateTime = r.dtRecordType5_DateTime,
							RecordType5Status = r.sRecordType5_Status,
							BillingFileExportDateTime = r.dtBillingFileExport_DateTime,
							BillingFileExportPath = r.sBillingFileExport_Path,
							BillingFileExportStatus = r.sBillingFileExport_Status,
							ExportCurrencyID = r.sExportCurrencyID,
                            BillingSystem=r.sBillingSystem,
						};

						entity.UploadedFiles = GetFileUploads(r.iInvoiceId);

						if (entity != null)
						{
							entities.Add(entity);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -875683);
				throw;
			}

			return entities;
		}

		/// <summary>
        /// Retrieve a list of all Invoices and the files that have been uploaded for SOO
        /// </summary>
        /// <param name="invoiceType">Type of Invoices to retrieve</param>
        /// <returns>entities found</returns>
        public List<Invoice> GetInvoices_All(int invoiceType)
        {
            List<Invoice> entities = new List<Invoice>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceNumbersByTypeId_AllResult> results = db.get_InvoiceNumbersByTypeId_All((int)invoiceType);

                    foreach (get_InvoiceNumbersByTypeId_AllResult r in results)
                    {
                        Invoice entity = new Invoice()
                        {
                            CreatedBy = r.sCreatedBy,
                            CreatedDate = r.dtCreatedDate,
                            ID = r.iInvoiceId,
                            TypeOfBill = (int)r.iInvoiceTypeId,
                            InvoiceNumber = r.sInvoiceNumber,
                            LastAction = r.sLastAction,
                            LastUpdatedBy = r.sLastUpdatedBy,
                            LastUpdatedDate = r.dtLastUpdatedDate,
                            Status = r.sStatus,
                            BillingMonth = r.iBillingMonth,
                            BillingYear = r.iBillingYear,
                            DefaultImportCurrencyID = r.iDefaultImportCurrencyID,
                            RecordType1DateTime = r.dtRecordType1_DateTime,
                            RecordType1Status = r.sRecordType1_Status,
                            RecordType2DateTime = r.dtRecordType2_DateTime,
                            RecordType2Status = r.sRecordType2_Status,
                            RecordType3DateTime = r.dtRecordType3_DateTime,
                            RecordType3Status = r.sRecordType3_Status,
                            RecordType4DateTime = r.dtRecordType4_DateTime,
                            RecordType4Status = r.sRecordType4_Status,
                            RecordType5DateTime = r.dtRecordType5_DateTime,
                            RecordType5Status = r.sRecordType5_Status,
                            BillingFileExportDateTime = r.dtBillingFileExport_DateTime,
                            BillingFileExportPath = r.sBillingFileExport_Path,
                            BillingFileExportStatus = r.sBillingFileExport_Status,
                            ExportCurrencyID = r.sExportCurrencyID,
                        };

                        entity.UploadedFiles = GetFileUploads(r.iInvoiceId);

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -875683);
                throw;
            }

            return entities;
        }

		/// <summary>
		/// Retrieve a single invoices and the files that have been uploaded for it
		/// </summary>
		/// <param name="id">Invoice identifier to retrieve</param>
		/// <returns>invoice found</returns>
		public Invoice GetInvoice(
			int id
			)
		{
			Invoice invoice = new Invoice();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<get_InvoiceDetailsByNumberResult> results = db.get_InvoiceDetailsByNumber(id);

                    get_InvoiceDetailsByNumberResult r = results.FirstOrDefault<get_InvoiceDetailsByNumberResult>();

					if (r == null)
					{
						throw new Exception(String.Format("Invoice id={0} not found", id));
					}

					Invoice entity = new Invoice()
					{
                        CreatedBy = r.sCreatedBy,
                        CreatedDate = r.dtCreatedDate,
                        ID = r.iInvoiceId,
                        TypeOfBill = (int)r.iInvoiceTypeId,
                        InvoiceNumber = r.sInvoiceNumber,
                        LastAction = r.sLastAction,
                        LastUpdatedBy = r.sLastUpdatedBy,
                        LastUpdatedDate = r.dtLastUpdatedDate,
                        Status = r.sStatus,
                        BillingMonth = r.iBillingMonth,
                        BillingYear = r.iBillingYear,
                        DefaultImportCurrencyID = r.iDefaultImportCurrencyID,
                        RecordType1DateTime = r.dtRecordType1_DateTime,
                        RecordType1Status = r.sRecordType1_Status,
                        RecordType2DateTime = r.dtRecordType2_DateTime,
                        RecordType2Status = r.sRecordType2_Status,
                        RecordType3DateTime = r.dtRecordType3_DateTime,
                        RecordType3Status = r.sRecordType3_Status,
                        RecordType4DateTime = r.dtRecordType4_DateTime,
                        RecordType4Status = r.sRecordType4_Status,
                        RecordType5DateTime = r.dtRecordType5_DateTime,
                        RecordType5Status = r.sRecordType5_Status,
                        BillingFileExportDateTime = r.dtBillingFileExport_DateTime,
                        BillingFileExportPath = r.sBillingFileExport_Path,
                        BillingFileExportStatus = r.sBillingFileExport_Status,
                        ExportCurrencyID = r.sExportCurrencyID,
                        BillStartDate = r.dtInvoiceBillPeriodStart,
                        BillEndDate = r.dtInvoiceBillPeriodEnd
					};

                    entity.UploadedFiles = GetFileUploads(r.iInvoiceId);

					invoice = entity;
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -875683);
				throw;
			}

			return invoice;
		}

		/// <summary>
		/// Update/Insert the invoice
		/// </summary>
		/// <param name="invoice">Invoice</param>
		/// <param name="userMakingChange">login of the user requesting the change</param>
		public void UpsertInvoice(Invoice invoice, string userMakingChange)
		{
			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					int result = db.Invoice_Upsert(
						invoice.ID,
						(int)invoice.TypeOfBill,
						invoice.InvoiceNumber,
						invoice.BillingMonth,
						invoice.BillingYear,
						invoice.DefaultImportCurrencyID,
						userMakingChange,
						invoice.Status,
						invoice.LastAction,

						invoice.RecordType1Status,
						invoice.RecordType1DateTime,
						invoice.RecordType2Status,
						invoice.RecordType2DateTime,
						invoice.RecordType3Status,
						invoice.RecordType3DateTime,
						invoice.RecordType4Status,
						invoice.RecordType4DateTime,
						invoice.RecordType5Status,
						invoice.RecordType5DateTime,

						invoice.BillingFileExportStatus,
						invoice.BillingFileExportDateTime,
						invoice.BillingFileExportPath,
						invoice.ExportCurrencyID,
						null
						);

					if (result < 0)
					{
						throw new Exception(String.Format("Failed to Upsert Invoice [{0}]", result));
					}

					foreach (UploadedFile uf in invoice.UploadedFiles)
					{
						result = UpsertFileUpload(uf, userMakingChange);
						if (result < 0)
						{
							throw new Exception(String.Format("Failed to Upsert File Upload {0} [-187947]", uf.FileType));
						}
					}
                    
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -722071);
				throw;
			}
		}

        public void InsertInvoice(Invoice invoice, string userMakingChange)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    int result = db.insert_Invoice(
                        (int)invoice.TypeOfBill,
                        invoice.InvoiceNumber,
                        invoice.BillingMonth,
                        invoice.BillingYear,
                        invoice.DefaultImportCurrencyID,
                        userMakingChange,
                        invoice.Status,
                        invoice.LastAction,

                        invoice.RecordType1Status,
                        invoice.RecordType1DateTime,
                        invoice.RecordType2Status,
                        invoice.RecordType2DateTime,
                        invoice.RecordType3Status,
                        invoice.RecordType3DateTime,
                        invoice.RecordType4Status,
                        invoice.RecordType4DateTime,
                        invoice.RecordType5Status,
                        invoice.RecordType5DateTime,

                        invoice.BillingFileExportStatus,
                        invoice.BillingFileExportDateTime,
                        invoice.BillingFileExportPath,
                        invoice.ExportCurrencyID,
                        null
                        );

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Upsert Invoice [{0}]", result));
                    }

                    foreach (UploadedFile uf in invoice.UploadedFiles)
                    {
                        result = UpsertFileUpload(uf, userMakingChange);
                        if (result < 0)
                        {
                            throw new Exception(String.Format("Failed to Upsert File Upload {0} [-187947]", uf.FileType));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

		/// <summary>
		/// Delete an existing Invoice
		/// </summary>
		/// <param name="userMakingChange">account of the user requesting the change</param>
		/// <param name="invoice">invoice</param>
		public void DeleteInvoice(Invoice invoice, string userMakingChange)
		{
			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					db.Invoice_DeleteByInvoice(invoice.ID, userMakingChange);
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -585073);
				throw;
			}
		}

		/// <summary>
		/// Get count of data stored in invoices
		/// </summary>
		/// <param name="invoiceID">invoice identifier to search or null for full list</param>
		/// <param name="numberOfInvoices">number of invoices found</param>
		/// <returns>positive on success or negative on failure</returns>
		public int GetArtifactCountForInvoiceImport(
			int? invoiceID,
			out int numberOfInvoices
			)
		{
			int status = -663049;
			numberOfInvoices = 0;

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<RecordType1_GetArtifactCountForInvoiceImportResult> results = db.RecordType1_GetArtifactCountForInvoiceImport(invoiceID);

					if (results != null)
					{
						RecordType1_GetArtifactCountForInvoiceImportResult result = results.First<RecordType1_GetArtifactCountForInvoiceImportResult>();

						numberOfInvoices = (result.count.HasValue) ? result.count.Value : 0;
					}

					status = 1;
				}
			}
			catch (Exception ex)
			{
				status = -340022;
				_logger.Exception(ex, status);
			}

			return status;
		}

		#endregion

		#region File Uploads

		/// <summary>
		/// Retrieve a list of all Uploaded Files
		/// </summary>
		/// <param name="invoiceID">invoice identifier for a single invoice or null for all uploaded files</param>
		/// <returns>entities found</returns>
		public UploadedFileCollection GetFileUploads(
			int? invoiceID
			)
		{
			UploadedFileCollection entities = new UploadedFileCollection();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<Get_InvoiceFileUploads_ByInvoiceIdResult> results = db.Get_InvoiceFileUploads_ByInvoiceId(invoiceID);

                    foreach (Get_InvoiceFileUploads_ByInvoiceIdResult r in results)
					{
						UploadedFile entity = new UploadedFile()
						{
							InvoiceID = r.iInvoiceId,
							FilePath = r.sFilePath,
                            //FileType = (UploadedFileType)r.iFileTypeId,
                            FileType = r.sFileType,
							UploadedStatus = r.sUploadedStatus,
							LastUpdatedBy = r.sUploadedBy,
                            LastUpdatedDate = Convert.ToString(r.dtUploadedDate),
                            SnapshotId = Convert.ToString(r.iSnapshotId),
                            UploadedFileId = r.iUploadedFileId
						};

						if (entity != null)
						{
							entities.Add(entity);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -397816);
				throw;
			}

			return entities;
		}

		/// <summary>
		/// Update/Insert a Uploaded File
		/// </summary>
		/// <param name="uploadedFile">File Uploaded</param>
		/// <param name="userMakingChange">login of the user requesting the change</param>
		public int UpsertFileUpload(
			UploadedFile uploadedFile,
			string userMakingChange
			)
		{
			int status = -591394;

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    //status = db.InvoiceFileUploads_Upsert(
                    //    uploadedFile.InvoiceID,
                    //    (int)uploadedFile.FileType,
                    //    uploadedFile.FilePath,
                    //    uploadedFile.UploadedStatus,
                    //    userMakingChange
                    //    );
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -360551);
				throw;
			}

			return status;
		}

		/// <summary>
		/// Returns the number of entities in the tax import file
		/// </summary>
		/// <param name="invoiceID">invoice identifier to check or null for full list</param>
		/// <param name="entityCount">number of entities found</param>
		/// <returns>positive on success or negative on failure</returns>
		public int GetArtifactCountForTaxImport(
			int? invoiceID,
			out int entityCount
			)
		{
			int status = -217553;
			entityCount = 0;

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<CbadTaxes_GetArtifactCountForTaxImportResult> results = db.CbadTaxes_GetArtifactCountForTaxImport(invoiceID);

					if (results != null)
					{
						CbadTaxes_GetArtifactCountForTaxImportResult result = results.First<CbadTaxes_GetArtifactCountForTaxImportResult>();

						entityCount = (result.count.HasValue) ? result.count.Value : 0;
					}

					status = 1;
				}
			}
			catch (Exception ex)
			{
				status = -229242;
				_logger.Exception(ex, status);
			}

			return status;
		}

		/// <summary>
		/// Rollback imported CBAD data
		/// </summary>
		/// <param name="invoice">invoice</param>
		/// <returns>positive on success or negative on failure</returns>
		public int RollbackCBADImport(Invoice invoice)
		{
			int status = -768616;

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					db.RecordType1_DeleteByInvoice(invoice.ID, invoice.CreatedBy);

					status = 1;
				}
			}
			catch (Exception ex)
			{
				status = -964646;
				_logger.Exception(ex, status);
			}

			return status;
		}

		/// <summary>
		/// Rollback the CBAD tax file information
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		public void RollbackTaxFileImport(
			int invoiceID,
			string callerLogin
			)
		{
			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					int status = db.CbadTaxes_DeleteByInvoice(invoiceID, callerLogin);
					if (status != 0)
					{
						throw new Exception(String.Format("Failed to Rollback CBAD TaxFile Error={0}", status));
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -147185);
				throw;
			}
		}

		#endregion


        #region InvoiceFileExports

        /// <summary>
        /// Inserts New Invoice Type
        /// </summary>
        /// <param name="invoiceType"></param>
        public int InsertInvoiceFileExports(ExportedFile exportedFile)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.insert_InvoiceFileExports(exportedFile.InvoiceID, exportedFile.ExportedFileName, exportedFile.ExportedFilePath, exportedFile.ExportedFileBy);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to InvoiceFileExports [{0}]", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Get ExportedFile
        /// </summary>
        /// <returns></returns>
        public List<ExportedFile> GetInvoiceFileExports(int InvoiceId)
        {
            List<ExportedFile> entities = new List<ExportedFile>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_InvoiceFileExportsResult> results = db.get_InvoiceFileExports(InvoiceId);

                    foreach (get_InvoiceFileExportsResult r in results)
                    {
                        ExportedFile entity = new ExportedFile()
                        {
                            InvoiceNumber = r.sInvoiceNumber,
                            ExportedFileName = r.sExportFileName,
                            ExportedFilePath = r.sExportFilePath,
                            ExportedFileDate = Convert.ToDateTime(r.dtExportDate),
                            ExportedFileBy = r.sExportedBy
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        #endregion

        /// <summary>
        /// Get MBMAutamate Status
        /// </summary>
        /// <returns></returns>
        public MBMAutomateStatus GetMBMAutomateStatus(int InvoiceId)
        {
            MBMAutomateStatus automateStatus=new MBMAutomateStatus();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {                                                           
                    var r =db.Get_MBMAutomateStatusByInvoiceId(InvoiceId).FirstOrDefault();
                    if (r!=null)
                    {                       
                        MBMAutomateStatus entity = new MBMAutomateStatus()
                        {
                            InvoiceId=r.InvoiceId,
                            IsFileTypeUploaded = r.IsFileTypeUploaded,
                            IsComparedToCRM = r.IsComparedToCRM,
                            IsViwedChanged = r.IsViwedChanged,
                            IsApprovalMailSent = r.IsApprovalMailSent,
                            IsApprovedByMail = r.IsApprovedByMail,
                            IsApprovedChange=r.IsApprovedChange,
                            IsSyncedCRM = r.IsSyncedCRM,
                            IsImportCRM = r.IsImportCRM,
                            IsProcessInvoice = r.IsProcessInvoice,
                            IsExportInvoice = r.IsExportInvoice,
                            LastAction = r.LastAction,
                            isPreBillingOverriden = r.isPreBillingOverriden
                        };
                        automateStatus = entity;
                    }               
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return automateStatus;
        }


        /// <summary>
        /// Start:17-12-2014
        ///  Returns list of customer numbers
        /// </summary>
        public List<Customers> GetCustomerNumbers()
        {
            List<Customers> entities = new List<Customers>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<usp_GetCustomerNumbersResult> results = db.usp_GetCustomerNumbers();

                    foreach (usp_GetCustomerNumbersResult r in results)
                    {
                        Customers entity = new Customers()
                        {
                           CustomerNum=r.CustomerNbr.ToString()
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        /// <summary>
        /// CRQ #CRQ700001314707  
        /// Get compared Account details
        /// </summary>
        /// <param name="caller">Invoice number</param>
        /// <param name="caller">login of calling account</param>
        /// <returns>list of Accounts</returns>
        public List<AccountDetails> SendAccountDetails(string UserName)
        {
            List<AccountDetails> entities = new List<AccountDetails>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<usp_GetAccountDetailsbyInvoiceResult> results = db.usp_GetAccountDetailsbyInvoice(UserName);

                    foreach (usp_GetAccountDetailsbyInvoiceResult r in results)
                    {
                        AccountDetails entity = new AccountDetails()
                        {
                            SnapshotID=r.SnapshotID,
                            AccountNumber = r.AccountNumber,
                            TelephoneNumber=r.TelephoneNumber,
                            AccountStatus = r.AccountStatus.ToString(),
                            AccountStatusID=r.AccountStatusID,
                            InstallationDate=r.InstallationDate,
                            DeactivateDate=r.DeactivateDate
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
                return entities;
            }
            catch
            {
                return null;
                throw;
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
        /// /// <param name="Logged in user"></param>
        /// <returns>response </returns>
        public void ProcessUSBillingData(
           
            string LoggedinUser
            )
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.usp_ProcessUSBillingData(LoggedinUser);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -585073);
                throw;
            }
        }

        /// <summary>
        /// Updates Invoice Export Status
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public int UpdateInvoiceExportStatus(int intInvoiceId)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_InvoiceExportStatus(intInvoiceId);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update Invoice Export Status [{0}]", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        } 
    }
}
