using MBM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DataAccess
{
    public class CSGAccountDAL
    {
        #region Setup
        private readonly Logger _logger;
        private readonly string _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConnection">database connection</param>
        public CSGAccountDAL(string dbConnection)
        {
            this._connection = dbConnection;
            _logger = new Logger(dbConnection);
        }
        #endregion

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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_ValidateCSGAccountNumberResult> results = db.get_CSGAccountNumberInformationFromDW(csgAccuntNumber);

                    foreach (get_ValidateCSGAccountNumberResult r in results)
                    {
                        lstCSGAccount = new CSGAccount()
                        {
                            AccountNumber = Convert.ToInt64(r.CSGAccountNumber),
                            //AccountName = r.CRMAccountFirstName + " " + r.CRMAccountLastName,
                            AccountName = r.CSGSubcriberFirstName + "" + r.CSGSubcriberLastName,
                            ParentAccountNumber = Convert.ToInt64(r.CSGParentAccountNumber),
                            //ParentAccountName = r.CRMAccountParentFirstName + " " + r.CRMAccountParentLastName,
                            ParentAccountName = r.CSGParentLastName + "" + r.CSGParentFirstName,
                            AccountBillCycle = Convert.ToString(r.CSGBillcycleName),
                            SubcriberNumber = Convert.ToInt32(r.CSGSubcriberNumber)
                        };

                        #region commented code
                        //lstCSGAccount = new CSGAccount()
                        //{
                        //    AccountNumber = Convert.ToInt64(10400095435),
                        //    //AccountName = r.CRMAccountFirstName + " " + r.CRMAccountLastName,
                        //    AccountName = "Prasad" + "" + "Tondare",
                        //    ParentAccountNumber = Convert.ToInt64(10400095419),
                        //    //ParentAccountName = r.CRMAccountParentFirstName + " " + r.CRMAccountParentLastName,
                        //    ParentAccountName = "Ashok" + "" + "Tondare",
                        //    AccountBillCycle = Convert.ToString("Bills on 12th"),
                        //    SubcriberNumber = Convert.ToInt32(5404507)
                        //};    
                        #endregion commented code

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return lstCSGAccount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csgAccount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int InsertAssociatedAccount_CSG(CSGAccount csgAccount, string username)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.insert_AssocaitedAccounts_CSG(csgAccount.AccountNumber,
                                                          csgAccount.InvoiceTypeid,
                                                          csgAccount.AccountName,
                                                          csgAccount.ParentAccountNumber,
                                                          csgAccount.ParentAccountName,
                                                          csgAccount.StringIdentifier,
                                                          csgAccount.CreateAccount,
                                                          username,
                                                          csgAccount.AccountBillCycle,
                                                          csgAccount.EffectiveBillDate,
                                                          csgAccount.ChildPays,
                                                          csgAccount.UserName,
                                                          csgAccount.Password,
                                                          csgAccount.FTP,
                                                          csgAccount.EmailId,
                                                          csgAccount.EDI,
                                                          csgAccount.SubcriberNumber,
                                                          csgAccount.DisplayAccountNumber
                                                          );

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Associate  Account [{0}]", result));
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
        /// Delete Associated Account
        /// </summary>
        /// <param name="accountNumber"></param>
        public void DeleteAssociatedAccount_CSG(long accountNumber)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.delete_AssoicatedAccount(accountNumber);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="stringIdentifier"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public int GetValidateCSGAssoicateAccounts(long accountNumber, string stringIdentifier, int actionType)
        {
            var results = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    results = db.get_ValidateCSGAssoicateAccounts(accountNumber, stringIdentifier, actionType);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
            return results;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="csgAccount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int UpdateAssociatedAccount_CSG(CSGAccount csgAccount, string username)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_AssociateAccount_CSG(csgAccount.AccountNumber
                                                        , csgAccount.InvoiceTypeid
                                                        , csgAccount.AccountName
                                                        , csgAccount.ParentAccountNumber
                                                        , csgAccount.ParentAccountName
                                                        , csgAccount.StringIdentifier
                                                        , csgAccount.CreateAccount
                                                        , username
                                                        , csgAccount.AccountBillCycle
                                                        , csgAccount.EffectiveBillDate
                                                        , csgAccount.ChildPays
                                                        , csgAccount.UserName
                                                        , csgAccount.Password
                                                        , csgAccount.FTP
                                                        , csgAccount.EmailId
                                                        , csgAccount.EDI);
                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update Associate  Account [{0}]", result));
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

        public int ValidateParentAccountInMBM_CSG(long? csgParentAccountNumber, string csgChildAccountBillCycle)
        {
            int spResult = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    spResult = db.get_validateCsgParentAccountNumber(csgParentAccountNumber, csgChildAccountBillCycle);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return spResult;
        }
    }
}
