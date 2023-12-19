using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;
using MBM.DataAccess;
using System.Globalization;

namespace MBM.DataAccess
{
    public class CUCDMDataDAL
    {
        #region Setup
		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
        public CUCDMDataDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}
		#endregion

        /// <summary>
        /// Gets Unextracted CUCDMData
        /// </summary>
        /// <returns></returns>
        public List<CUCDMData> GetUnextractedCUCDMData()
        {
            List<CUCDMData> lstCUCDMData = new List<CUCDMData>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_UnextractedCUCDMDataResult> results = db.get_UnextractedCUCDMData();

                    foreach (get_UnextractedCUCDMDataResult r in results)
                    {
                        CUCDMData objCUCDMData = new CUCDMData()
                        {
                             Id = r.id,
                             SnapshotId = r.snapshot_id,
                             SubIdentifier = r.subidentifier,
                             FirstName = r.first_name,
                             LastName = r.last_name,
                             AssetSearch = r.asset_search,
                             ServiceProfileID = r.service_profile_id,
                             LegalEntity = r.legal_entity,
                             ServiceStartDate = r.service_start_date,
                             State = r.state,
                             PostalCode = r.postal_code,
                             ServiceEndDate = r.service_end_date,
                             Country = r.country,
                             DirectoryNumber = r.directory_number,
                             MacAddress = r.mac_address
                        };

                        if (objCUCDMData != null)
                        {
                            lstCUCDMData.Add(objCUCDMData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstCUCDMData;
        }

        /// <summary>
        /// Gets Enhanced Data
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EnhancedData> GetEnhancedData(int? invoiceId)
        {
            List<EnhancedData> lstEnhancedData = new List<EnhancedData>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_EHCSEhancedDataResult> results = db.get_EHCSEhancedData(invoiceId);

                    foreach (get_EHCSEhancedDataResult r in results)
                    {
                        EnhancedData objEnhancedData = new EnhancedData()
                        {
                            Customer = r.Customer,
                            InvoiceNumber = r.InvoiceNumber,
                            SnapshotId = r.iSnapshotId,
                            SubIdentifier = r.sSubIdentifier,
                            FirstName = r.sFirstName,
                            LastName = r.sLastName,
                            PrimaryUniqueIdentifier = r.sAssetSearchCode1,
                            SecondaryUniqueIdentifier = r.sAssetSearchCode2,
                            ServiceProfile = r.sProfileName,
                            LegalEntity = r.sLegalEntity,
                            ChargeCode = r.iGLCode.ToString(),
                            Charge = r.mCharge.ToString(),
                            StartDate = r.dtMainStart,
                            EndDate = r.dtMainEnd,
                            FeatureCode = r.sFeatureCode,
                            e164_mask = r.e164_mask,
                            ActiveCharge = r.ActiveCharge,
                            EffectiveBillingDate = r.EffectiveBillingDate
                        };

                        if (objEnhancedData != null)
                        {
                            lstEnhancedData.Add(objEnhancedData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstEnhancedData;
        }

        /// <summary>
        /// Gets UnEnhanced Data
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<UnEnhancedData> GetUnEnhancedData(int? invoiceId)
        {
            List<UnEnhancedData> lstUnEnhancedData = new List<UnEnhancedData>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_EHCSUnEhancedDataResult> results = db.get_EHCSUnEhancedData(invoiceId);

                    foreach (get_EHCSUnEhancedDataResult r in results)
                    {
                        UnEnhancedData objUnEnhancedData = new UnEnhancedData()
                        {
                            Customer = r.Customer,
                            InvoiceNumber = r.InvoiceNumber,
                            SnapshotId = r.iSnapshotId,
                            SubIdentifier = r.sSubIdentifier,
                            FirstName = r.sFirstName,
                            LastName = r.sLastName,
                            PrimaryUniqueIdentifier = r.sAssetSearchCode1,
                            SecondaryUniqueIdentifier = r.sAssetSearchCode2,
                            ServiceProfile = r.sServiceProfileId,
                            LegalEntity = r.sLegalEntity,
                            ServiceStartDate = r.dtMainStart,
                            ServiceEndDate = r.dtMainEnd,
                            State = r.state,
                            PostalCode = r.zipCode,
                            Country = r.country,
                            e164_mask = r.e164_mask,
                            ActiveCharge = r.ActiveCharge,
                            EffectiveBillingDate = r.EffectiveBillingDate
                        };

                        if (objUnEnhancedData != null)
                        {
                            lstUnEnhancedData.Add(objUnEnhancedData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstUnEnhancedData;
        }

        /// <summary>
        /// Gets Enhanced Data
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EHCSRawData> GetEHCSRawData(string customer, string startDate, string endDate)
        {
            List<EHCSRawData> lstEHCSRawData = new List<EHCSRawData>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_EHCSRawDataResult> results = db.get_EHCSRawData(startDate, endDate, customer);

                    foreach (get_EHCSRawDataResult r in results)
                    {
                        EHCSRawData objEnhancedData = new EHCSRawData()
                        {
                            SnapshotId = r.snapshot_id,
                            SubIdentifier = r.subidentifier,
                            FirstName = r.first_name,
                            LastName = r.last_name,
                            PrimaryUniqueIdentifier = r.asset_search,
                            ServiceProfileId = r.service_profile_id,
                            ServiceStartDate = r.service_start_date,
                            ServiceEndDate = r.service_end_date,
                            LegalEntity = r.legal_entity,
                            State = r.state,
                            PostalCode = r.postal_code,
                            Country = r.country,
                            DirectoryNumber = r.directory_number,
                            ServiceProfileUid = r.service_profile_uid, 
                            e164_mask = r.e164_mask,
                            ActiveCharge = r.ActiveCharge,
                            EffectiveBillingDate = r.EffectiveBillingDate
                        };

                        if (objEnhancedData != null)
                        {
                            lstEHCSRawData.Add(objEnhancedData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstEHCSRawData;
        }

        /// <summary>
        /// Gets SOO Accural Revenue Report Data, cbe_9941
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SOOReportData> GetSOOReportData(string customer, string startDate, string endDate)
        {
            List<SOOReportData> SOOReportData = new List<SOOReportData>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    var results = db.get_SOOFixedChargesData(startDate, endDate, customer);

                    foreach (get_SOOFixedChargesDataResult r in results)
                    {
                        SOOReportData objReportData = new SOOReportData()
                        {
                            CRM_ACCOUNT = Convert.ToInt64(r.CRM_ACCOUNT),
                            TN = r.TN,
                            PROFILE_TYPE = r.PROFILE_TYPE,
                            AMOUNT = r.mCharge,
                            START_DTT = (r.dtMainStart != null) ? r.dtMainStart.ToString().Substring(0, 10) : null,
                            END_DTT = (r.dtMainEnd != null) ? r.dtMainEnd.ToString().Substring(0, 10) : null,
                            CUSTOMER_DEPARTMENT = r.CUSTOMER_DEPARTMENT,
                            CUSTOMER_COST_CENTER = r.CUSTOMER_COST_CENTER,
                            CUSTOMER_ALI_CODE = r.CUSTOMER_ALI_CODE,
                            SUPERVISOR_NAME = r.SUPERVISOR_NAME,
                            FIRST_NAME = r.FIRST_NAME,
                            LAST_NAME = r.LAST_NAME,
                            EMAIL = r.EMAIL,
                            QUANTITY = r.QUANTITY,
                            UNIT_PRICE = r.UNIT_PRICE,
                            DESCRIPTION = r.DESCRIPTION
                        };

                        if (objReportData != null)
                        {
                            SOOReportData.Add(objReportData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return SOOReportData;
        }

        public List<EDIReportData> GetEDIReportData()
        {
            List<EDIReportData> EDIReportData = new List<EDIReportData>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    var results = db.get_EDIReportData();

                    foreach (get_EDIReportDataResult r in results)
                    {
                        EDIReportData objReportData = new EDIReportData()
                        {
                            AccountNumber =1, //r.iAccountNumber,
                            UserName = r.sFTPUsername,
                            Password = r.sFTPPassword,
                            FTPSite = r.sDefaultFTP,
                            Email = r.sEmailAddress,
                            EDI = r.bEDI
                        };

                        if (objReportData != null)
                        {
                            EDIReportData.Add(objReportData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return EDIReportData;
        }
    }
}
