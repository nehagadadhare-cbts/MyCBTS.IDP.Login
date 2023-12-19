using MBM.DataAccess;
using MBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BillingEngine
{
   public class CSGAccountBL
    {
         private DataFactory _dal;
        private Logger _logger;
        private string _connectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public CSGAccountBL(string connectionString)
		{
			_connectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

       /// <summary>
       /// 
       /// </summary>
       /// <param name="csgAccuntNumber"></param>
       /// <returns></returns>
        public CSGAccount ValidateCSGAccount(string csgAccuntNumber)
        {
            CSGAccount lstCSGAccount = new CSGAccount();
            try
            {
                lstCSGAccount = _dal.CSGAccount.ValidateCSGAccount(csgAccuntNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return lstCSGAccount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="stringIdentifier"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public int GetValidateCSGAssoicateAccounts(long accountNumber, string stringIdentifier,int actionType)
        {
            try
            {
                return _dal.CSGAccount.GetValidateCSGAssoicateAccounts(accountNumber, stringIdentifier, actionType);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Updates the AssociatedAccount to Invoice Type
        /// </summary>
        /// <param name="crmAccount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int UpdateAssociatedAccount(CSGAccount csgAccount, string username)
        {
            int result = 0;
            try
            {
                result = _dal.CSGAccount.UpdateAssociatedAccount_CSG(csgAccount, username);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="csgParentAccountNumber"></param>
      /// <param name="csgChildAccountBillCycle"></param>
      /// <returns></returns>
        public int ValidateParentAccountInMBM_CSG(long? csgParentAccountNumber, string csgChildAccountBillCycle)
        {
            var validationCode = 0;
            try
            {
                validationCode = _dal.CSGAccount.ValidateParentAccountInMBM_CSG(csgParentAccountNumber, csgChildAccountBillCycle);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return validationCode;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="csgAccount"></param>
      /// <param name="username"></param>
      /// <returns></returns>
        public int InsertCSGAssociatedAccount(CSGAccount csgAccount, string username)
        {
            int result = 0;
            try
            {
                result = _dal.CSGAccount.InsertAssociatedAccount_CSG(csgAccount, username);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;

        }


        /// <summary>
        /// Delete Associated Account
        /// </summary>
        /// <param name="accountNumber"></param>
        public void DeleteAssociatedAccount_CSG(long accountNumber)
        {
            try
            {
                _dal.CSGAccount.DeleteAssociatedAccount_CSG(accountNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }

    }
}
