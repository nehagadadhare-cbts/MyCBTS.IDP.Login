using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;

namespace MBM.BillingEngine
{
    public class CRMAccountBL
    {
        private DataFactory _dal;
        private Logger _logger;
        private string ConnectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public CRMAccountBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        /// <summary>
        /// Gets Assoicated Accounts for InvoiceType
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<CRMAccount> GetAssoicateAccountsByInvoiceTypeId(int InvoiceTypeId)
        {
            try
            {
                return _dal.CRMAccount.GetAssoicateAccountsByInvoiceTypeId(InvoiceTypeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Gets All Assoicated Accounts
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<CRMAccount> GetAllAssoicateAccounts()
        {
            try
            {
                return _dal.CRMAccount.GetAllAssoicateAccounts();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }


        /// <summary>
        /// Delete Associated Account
        /// </summary>
        /// <param name="AccountNumber"></param>
        public void DeleteAssociatedAccount(long AccountNumber)
        {
            try
            {
                _dal.CRMAccount.DeleteAssociatedAccount(AccountNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }

        /// <summary>
        /// Associates the Account to Invoice Type
        /// </summary>
        /// <param name="crmAccount"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public int InsertAssociatedAccount(CRMAccount crmAccount, string Username)
        {
            int result = 0;
            try
            {
                result = _dal.CRMAccount.InsertAssociatedAccount(crmAccount,Username);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;

        }

        /// <summary>
        /// Updates the AssociatedAccount to Invoice Type
        /// </summary>
        /// <param name="crmAccount"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public int UpdateAssociatedAccount(CRMAccount crmAccount, string Username)
        {
            int result = 0;
            try
            {
                result = _dal.CRMAccount.UpdateAssociatedAccount(crmAccount, Username);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Validates and returns CRMAccount details
        /// </summary>
        /// <param name="CRMAccuntNumber"></param>
        /// <returns>CRMAccount</returns>
        public CRMAccount ValidateCRMAccount(string CRMAccuntNumber)
        {
            CRMAccount lstCRMAccount = new CRMAccount();
            try
            {
                lstCRMAccount = _dal.CRMAccount.ValidateCRMAccount(CRMAccuntNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return lstCRMAccount;
        }

        //ER8668
        /// <summary>
        /// Validates Effective Billing date and returns CRMAccount details
        /// </summary>
        /// <param name="CRMAccuntNumber"></param>
        /// <returns>CRMAccount</returns>
        public string ValidateEffectiveBillingDate(int InvoiceTypeId,string CRMAccuntNumber, DateTime? EffectiveBillDate)
        {
            string response = string.Empty;
            try
            {
                response = _dal.CRMAccount.ValidateEffectiveBillingDate(InvoiceTypeId, CRMAccuntNumber, EffectiveBillDate);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return response;
        }


        public int AccountEffectiveDateStatus(int InvoiceTypeId, string CRMAccuntNumber)
        {
            int result;
            try
            {
                result = _dal.CRMAccount.AccountEffectiveDateStatus(InvoiceTypeId, CRMAccuntNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        //ER8668
        /// <summary>
        /// Inserts record in MBM_Comparison_Result database table.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// /// <param name="invoiceTypeId"></param>
        /// <returns>int</returns>
        public int InsertMBMComparisonResult(int? invoiceId, int? invoiceTypeId)
        {
            int result = 0;
            try
            {
                result = _dal.CRMAccount.InsertMBMComparisonResult(invoiceId, invoiceTypeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        //ER8668
        /// <summary>
        /// Inserts record in MBM_Comparison_ResultCSG database table.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// /// <param name="invoiceTypeId"></param>
        /// <returns>int</returns>
        public int InsertMBMComparisonResultCSG(int? invoiceId, int? invoiceTypeId)
        {
            int result = 0;
            try
            {
                result = _dal.CRMAccount.InsertMBMComparisonResultCSG(invoiceId, invoiceTypeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Gets MBMComparisonResult record based on invoiceid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public List<MBMComparisonResult> GetMBMComparisonResult(int? invoiceId)
        {
            try
            {
                return _dal.CRMAccount.GetMBMComparisonResult(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        //SERO-1582

        public List<MBMComparisonResultCSG> GetMBMComparisonResultCSG(int? invoiceId)
        {
            try
            {
                return _dal.CRMAccount.GetMBMComparisonResultCSG(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }


        /// <summary>
        /// Updates the MBMComparisonResult table processed flag.
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        public int UpdateMBMComparisonResult(int? invoiceId)
        {
            int result = 0;
            try
            {
                result = _dal.CRMAccount.UpdateMBMComparisonResult(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        public int UpdateMBMComparisonResultCSG(int? invoiceId, long snapshotId, int? loadId,string  account, bool ProcessFirst)
        {
            int result = 0;
            try
            {
                result = _dal.CRMAccount.UpdateMBMComparisonResultCSG(invoiceId, snapshotId, loadId, account, ProcessFirst);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Gets crm input record based on invoiceid and username
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userName"></param>
        /// <returns>void</returns>
        public void GetCRMInputData(string invoiceNumber, string userName)
        {
            try
            {
                _dal.CRMAccount.GetCRMInputData(invoiceNumber, userName);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        //new 1582
        /// <summary>
        /// Gets crm input record based on invoiceid and username
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userName"></param>
        /// <returns>void</returns>
        public void GetCSGInputData(string invoiceNumber, string userName)
        {
            try
            {
                _dal.CRMAccount.GetCSGInputData(invoiceNumber, userName);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }
        //end new 1582

        /// <summary>
        /// Gets latest CRM Data after sync.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="loginUser"></param>
        public void GetSyncedCRMData(string invoiceNumber, string loginUser)
        {
            try
            {
                _dal.CRMAccount.GetSyncedCRMData(invoiceNumber, loginUser);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }
        //1582-2
        /// <summary>
        /// Gets latest CSG Data after sync.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="loginUser"></param>
        public void GetSyncedCSGData(string invoiceNumber, string loginUser)
        {
            try
            {
                _dal.CRMAccount.GetSyncedCSGData(invoiceNumber, loginUser);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }
        /// <summary>
        /// Gets unprocessed MBMComparisonResult record based on invoiceid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public List<MBMComparisonResult> GetUnprocessedMBMComparisonResult(int? invoiceId)
        {
            try
            {
                return _dal.CRMAccount.GetUnprocessedMBMComparisonResult(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Gets unprocessed MBMComparisonResultCSG record based on invoiceid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public List<MBMComparisonResultCSG> GetUnprocessedMBMComparisonResultCSG(int? invoiceId)
        {
            try
            {
                return _dal.CRMAccount.GetUnprocessedMBMComparisonResultCSG(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }


        /// <summary>
        /// Gets All csg service attributes
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<MBMServiceAttributes> GetAllCSGServiceAttributes()
        {
            try
            {
                return _dal.CRMAccount.GetAllCSGServiceAttributes();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Gets All csg service features/ldplans
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<MBMServiceFeatures> GetAllCSGServiceFeatures(int? invoiceId)
        {
            try
            {
                return _dal.CRMAccount.GetAllCSGServiceFeatures(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

    }
}
