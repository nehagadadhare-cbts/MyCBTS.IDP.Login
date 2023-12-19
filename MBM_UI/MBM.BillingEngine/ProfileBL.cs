using MBM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;
using System.Configuration;

namespace MBM.BillingEngine
{
    public class ProfileBL
    {
        private DataFactory _dal;
		private Logger _logger;
		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public ProfileBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        #region Profiles
        public List<Profile> FetchProfilesByInvoiceId(int invoiceID)
        {
            try
			{

                return _dal.ProfileDetails.getProfileByInvoiceType(invoiceID);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -188224);
				throw;
			}
        }

        //SERO-1582
        public List<Profile> FetchChargeStringIdentifierByInvoiceId(int invoiceID)
        {
            try
            {

                return _dal.ProfileDetails.getChargeStringIdentifierByInvoiceType(invoiceID);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }
        
        public int AddNewProfile(Profile profile)
        {
            try
            {
                return _dal.ProfileDetails.CreateProfile(profile);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
				throw;
            }
        }

        public int DeleteProfile(int profileid)
        {
            try
            {
                return _dal.ProfileDetails.DeleteProfile(profileid);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        public int EditProfile(Profile profile)
        {
            try
            {
                return _dal.ProfileDetails.EditProfile(profile);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public bool IsProfileExistInEnhanceData(int profileId)
        {
            try
            {
                return _dal.ProfileDetails.IsProfileExistInEnhanceData(profileId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }
        #endregion

        #region ProfileCharge
        public List<ProfileCharge> FetchProfileChargesByInvoiceId(int invoiceID, string invoiceName)
        {
            try
            {
                return _dal.ProfileDetails.getProfileChargesByInvoiceType(invoiceID, invoiceName);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public List<ProfileCharge> FetchCSGProfileChargesByInvoiceId(int invoiceID, string invoiceName)
        {
            try
            {
                return _dal.ProfileDetails.getCSGProfileChargesByInvoiceType(invoiceID, invoiceName);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }
        //SERO-1582

        public List<ProfileCharge> FetchCSGPricingPlanIdByInvoiceId(int invoiceID, string invoiceName)
        {
            try
            {
                return _dal.ProfileDetails.getCSGPricingPlanIdByInvoiceId(invoiceID, invoiceName);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public void AddNewProfileCharges(ProfileCharge profileCharge)
        {
            try
            {
                 _dal.ProfileDetails.CreateProfileCharge(profileCharge);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        //PC
        public void AddNewProfileChargesCSG(ProfileCharge profileCharge)
        {
            try
            {
                _dal.ProfileDetails.CreateProfileChargeCSG(profileCharge);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }


        public int EditprofileCharges(ProfileCharge profileCharge)
        {
            try
            {
                return _dal.ProfileDetails.EditprofileCharges(profileCharge);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public int EditprofileChargesForCSG(ProfileCharge profileCharge)
        {
            try
            {
                return _dal.ProfileDetails.EditprofileChargesForCSG(profileCharge);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public int DeleteProfileCharge(int chargeId)
        {
            try
            {
                return _dal.ProfileDetails.DeleteProfileCharge(chargeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public int DeleteProfileChargeCSG(int chargeId)
        {
            try
            {
                return _dal.ProfileDetails.DeleteProfileChargeCSG(chargeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public int ValidateProfileChargeCSG(int chargeId)
        {
            try
            {
                return _dal.ProfileDetails.ValidateProfileChargeCSG(chargeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public ProfileCharge ValidateProfileChargeFromCRM(string glCode)
        {
            try
            {
                return _dal.ProfileDetails.ValidateProfileChargeFromCRM(glCode);
                
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }
        public bool IsProfileChargeExistInEnhanceData(int chargeId)
        {
            try
            {
                return _dal.ProfileDetails.IsProfileChargeExistInEnhanceData(chargeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }
        #endregion

        #region Maulacharge
        public int? CreateManualChargeFile(int? invoiceTypeId, string fileName, string fileStatus, string userName)
        {
            try
            {
                return _dal.ProfileDetails.CreateManualChargeFile(invoiceTypeId, fileName, fileStatus, userName);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public List<ManualChargeFileDetails> getMaualChargeFilesInfoByInvoiceType(int invoiceTypeId)
        {
            try
            {
                return _dal.ProfileDetails.getMaualChargeFilesInfoByInvoiceType(invoiceTypeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public int DeleteManulaChargeFileByFileId(int fileId)
        {
            try
            {
                return _dal.ProfileDetails.DeleteManulaChargeByFileId(fileId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        public List<ManualCharge_Stage> getMaualChargesByFileId(int fileId)
        {
            try
            {
                return _dal.ProfileDetails.getMaualChargesByFileId(fileId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public List<ManualCharge_Success> getSuccessRecordsByInvoiceId(int invoiceid)
        {
            try
            {
                return _dal.ProfileDetails.getSuccessRecordsByInvoiceId(invoiceid);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public int? ProcessMaualChargesData(int? fileId)
        {
            try
            {
                return _dal.ProfileDetails.ProcessManualChargeData(fileId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }

        public ManualChargeFileSummary getManualChargeFileProcessSummary(int fileId)
        {
            try
            {
                return _dal.ProfileDetails.getManualChargeFileProcessSummary(fileId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }
        
        public bool isMChareButtonEnable()
        {
            bool isEnabled = false;
            try
            {
                if (ConfigurationManager.AppSettings["OffsetDaysBefore"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["OffsetDaysBefore"].ToString())
                && ConfigurationManager.AppSettings["OffsetDaysAfter"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["OffsetDaysAfter"].ToString())
                && ConfigurationManager.AppSettings["CurrentDayOffset"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["CurrentDayOffset"].ToString()))
                {
                    int intOffsetDaysBefore = Convert.ToInt32(ConfigurationManager.AppSettings["OffsetDaysBefore"].ToString());
                    int intOffsetDaysAfter = Convert.ToInt32(ConfigurationManager.AppSettings["OffsetDaysAfter"].ToString());
                    int intCurrentDayOffset = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentDayOffset"].ToString());

                    DateTime dtCurrentDateTime = DateTime.Now;
                    dtCurrentDateTime = dtCurrentDateTime.AddDays(intCurrentDayOffset);

                    DateTime InvoiceBillStartDate = new DateTime(dtCurrentDateTime.Year, dtCurrentDateTime.Month, 1);
                    DateTime InvoiceBillEndDate = InvoiceBillStartDate.AddMonths(1).AddDays(-1);

                    if (dtCurrentDateTime >= InvoiceBillStartDate.AddDays(intOffsetDaysAfter) && dtCurrentDateTime < InvoiceBillEndDate.AddDays(intOffsetDaysBefore))
                    {
                        isEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
            return isEnabled;
        }

        public int UpdateManualChargeFileStatus(int fileId, string filestatus)
        {
            try
            {
                return _dal.ProfileDetails.UpdateManualChargeFileStatus(fileId, filestatus);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -188224);
                throw;
            }
        }
        #endregion
    }
}
