using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;
using MBM.DataAccess;

namespace MBM.DataAccess
{
    public class CRMAccountDAL
    {
        #region Setup
		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
        public CRMAccountDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}
		#endregion

        #region Enum ActionTypes
        /// <summary>
        /// 
        /// </summary>
        enum ActionTypes
        {
            AddBillItem = 1,
            DeleteBillItem = 2,
            AddCharge = 3,
            UpdateCharge = 4,
            DeleteCharge = 5,
            AddTelephone = 6,
            DeleteTelephone = 7,
            ChangeTelephone = 8,
        };

        enum CSGActionTypes
        {
            AddService = 1,
            AddServiceFeature = 2,
            RemoveServiceFeature = 3,
            RemoveService = 4
        };
        #endregion

        /// <summary>
        /// Gets Assoicated Accounts for InvoiceType
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<CRMAccount> GetAssoicateAccountsByInvoiceTypeId(int InvoiceTypeId)
        {
            List<CRMAccount> lstCRMAccounts = new List<CRMAccount>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AssoicateAccountsByInvoiceTypeIdResult> results = db.get_AssoicateAccountsByInvoiceTypeId(InvoiceTypeId);
                    foreach (get_AssoicateAccountsByInvoiceTypeIdResult r in results)
                    {
                        var date = (r.EffectiveBillDate) == null ? string.Empty : DateTime.Parse(((DateTime)r.EffectiveBillDate).ToString()).ToString("MM-dd-yyyy");

                        string provisioningIdentifierValue = string.Empty;
                        provisioningIdentifierValue = r.sStringIdentifier;


                        CRMAccount lstCRMAccount = new CRMAccount()
                        {
                            AccountNumber = r.iAccountNumber,
                            AccountName = r.sAccountName,
                            ParentAccountNumber = r.iParentAccountNumber,
                            ParentAccountName = r.sParentAccountName,
                            CreateAccount = r.bCreateAccount,
                            StringIdentifier = provisioningIdentifierValue,
                            AccountBillCycle = r.iBillCycle,
                            EffectiveBillingDate = date,
                            ChildPays = r.bChildPays,
                            UserName = r.sUserName,
                            Password = r.sPassword,
                            FTP = r.sFTP,
                            EmailId = r.sEmailID,
                            EDI = Convert.ToBoolean(r.bEDI)
                        };

                        if (lstCRMAccount != null)
                        {
                            lstCRMAccounts.Add(lstCRMAccount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstCRMAccounts;
        }

        /// <summary>
        /// Gets All Assoicated Account
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<CRMAccount> GetAllAssoicateAccounts()
        {
            List<CRMAccount> lstCRMAccounts = new List<CRMAccount>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AllAssoicateAccountsResult> results = db.get_AllAssoicateAccounts();

                    foreach (get_AllAssoicateAccountsResult r in results)
                    {
                        CRMAccount lstCRMAccount = new CRMAccount()
                        {
                            InvoiceTypeName = r.sInvoiceTypeName,
                            Prefix = r.sPrefix,
                            AccountNumber = r.iAccountNumber,
                            StringIdentifier = r.sStringIdentifier
                        };

                        if (lstCRMAccount != null)
                        {
                            lstCRMAccounts.Add(lstCRMAccount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstCRMAccounts;
        }


        /// <summary>
        /// Delete Associated Account
        /// </summary>
        /// <param name="AccountNumber"></param>
        public void DeleteAssociatedAccount(long AccountNumber)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.delete_AssoicatedAccount(AccountNumber);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.insert_AssocaitedAccounts(crmAccount.AccountNumber
                                                          , crmAccount.InvoiceTypeid
                                                          , crmAccount.AccountName
                                                          , crmAccount.ParentAccountNumber
                                                          , crmAccount.ParentAccountName
                                                          , crmAccount.StringIdentifier
                                                          , crmAccount.CreateAccount
                                                          , Username
                                                          , crmAccount.AccountBillCycle
                                                          , crmAccount.EffectiveBillDate
                                                          , crmAccount.ChildPays
                                                          , crmAccount.UserName
                                                          , crmAccount.Password
                                                          , crmAccount.FTP
                                                          , crmAccount.EmailId
                                                          , crmAccount.EDI
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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_AssociateAccount(crmAccount.AccountNumber, crmAccount.InvoiceTypeid, crmAccount.AccountName, crmAccount.ParentAccountNumber, crmAccount.ParentAccountName, crmAccount.StringIdentifier, crmAccount.CreateAccount, Username, crmAccount.AccountBillCycle, crmAccount.EffectiveBillDate, crmAccount.ChildPays, crmAccount.UserName, crmAccount.Password, crmAccount.FTP, crmAccount.EmailId, crmAccount.EDI); //

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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_ValidateCRMAccountNumberResult> results = db.get_ValidateCRMAccountNumber(CRMAccuntNumber);

                    foreach (get_ValidateCRMAccountNumberResult r in results)
                    {
                        lstCRMAccount = new CRMAccount()
                        {
                            AccountNumber = Convert.ToInt32(r.CRMAccountNumber),
                            //AccountName = r.CRMAccountFirstName + " " + r.CRMAccountLastName,
                            AccountName = r.CRMAccountLastName + "" + r.CRMAccountFirstName,
                            ParentAccountNumber = Convert.ToInt32(r.CRMAccountParentAccountNumber),
                            //ParentAccountName = r.CRMAccountParentFirstName + " " + r.CRMAccountParentLastName,
                            ParentAccountName = r.CRMAccountParentLastName + "" + r.CRMAccountParentFirstName,
                            AccountBillCycle = r.CRMAccountBillCycle
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return lstCRMAccount;
        }
        //ER8668
        /// <summary>
        /// Validates Effective Billing date and returns response message
        /// </summary>
        /// <param name="CRMAccuntNumber"></param>
        /// <returns>String</returns>
        public string ValidateEffectiveBillingDate(int InvoiceTypeId, string CRMAccuntNumber, DateTime? EffectiveBillingDate)
        {
            string response = string.Empty;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                     ISingleResult<get_ValidateEffectiveDateResult> result = db.get_ValidateEffectiveDate(InvoiceTypeId, CRMAccuntNumber, EffectiveBillingDate);
                     foreach (get_ValidateEffectiveDateResult item in result)
                     {
                         response = item.Response;
                     }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return response;
        }

        public int AccountEffectiveDateStatus(int InvoiceTypeId, string CRMAccuntNumber)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AccountEffectiveDate_StatusResult> response = db.get_AccountEffectiveDate_Status(InvoiceTypeId, CRMAccuntNumber);
                    foreach (get_AccountEffectiveDate_StatusResult r in response)
                    {
                        result = r.Response;
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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Insert_MBM_ComparisonResult(invoiceId, invoiceTypeId);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Insert MBM Comparison Result Data"));
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

        //ER8668
        /// <summary>
        /// Inserts record in MBM_Comparison_Result database table.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// /// <param name="invoiceTypeId"></param>
        /// <returns>int</returns>
        public int InsertMBMComparisonResultCSG(int? invoiceId, int? invoiceTypeId)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Insert_MBM_ComparisonResultCSG(invoiceId, invoiceTypeId);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Insert MBM Comparison Result Data"));
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
        /// Gets MBMComparisonResult record based on invoiceid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public List<MBMComparisonResult> GetMBMComparisonResult(int? invoiceId)
        {
            List<MBMComparisonResult> listComparisonResult = new List<MBMComparisonResult>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<Get_MBM_ComparisonResultResult> results = db.Get_MBM_ComparisonResult(invoiceId);

                    foreach (Get_MBM_ComparisonResultResult r in results)
                    {
                        MBMComparisonResult comparisonResult = new MBMComparisonResult()
                        {
                           SnapShotId = r.iSnapshotId,
                           InvoiceId = r.iInvoiceId,
                           CRMAccountNumber = r.sCRMAccountNumber,
                           AssetSearchCode1 = r.sAssetSearchCode1,
                           AssetSearchCode2 = r.sAssetSearchCode2,
                           ProfileName = r.sProfileName,
                           FeatureCode = r.sFeatureCode,
                           GLCode = r.iGLCode,
                           Charge = r.mCharge,
                           ActionType = Enum.GetName(typeof(ActionTypes), r.iActionType),
                            Action = r.iActionType,
                           StartDate = r.dtMainStart,
                           EndDate = r.dtMainEnd,
                           ItemType = r.sItemType,
                           SubType = r.sSubType,
                           SwitchId = r.sSwitchId,
                           ItemId = r.sItemId,
                           SequenceId = r.sSequenceId,
                           LoadId = r.iLoadId,
                           Processed = r.iProcessed.GetValueOrDefault() == 1 ? "YES" : "NO"
                        };

                        if (comparisonResult != null)
                        {
                            listComparisonResult.Add(comparisonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
            return listComparisonResult;
        }


        //SERO-1582

        public List<MBMComparisonResultCSG> GetMBMComparisonResultCSG(int? invoiceId)
        {
            List<MBMComparisonResultCSG> listComparisonResult = new List<MBMComparisonResultCSG>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<Get_MBM_ComparisonResultResultCSG> results = db.Get_MBM_ComparisonResultCSG(invoiceId);

                    foreach (Get_MBM_ComparisonResultResultCSG r in results)
                    {
                        MBMComparisonResultCSG comparisonResult = new MBMComparisonResultCSG()
                        {
                            SnapShotId = r.iSnapshotId,
                            InvoiceId = r.iInvoiceId,
                            //CRMAccountNumber = r.sCSGAccountNumber,
                            AssetSearchCode1 = r.sAssetSearchCode1,
                            AssetSearchCode2 = r.sAssetSearchCode2,
                            ProfileName = r.sProfileName,
                            //FeatureCode = r.sFeatureCode,
                            //GLCode = r.iGLCode,
                            OfferExternalRef = r.sOfferExternalRef,
                            ProductExternalRef = r.sProductExternalRef,
                            PricingPlanExternalRef = r.sPricingPlanExternalRef,
                            MBMUniqueID= r.sMBMUniqueID,
                            SubscriberId = r.sCSGAccountNumber,

                            Charge = r.mCharge,
                            ActionType = Enum.GetName(typeof(CSGActionTypes), r.iActionType),
                            Action = r.iActionType,
                            //StartDate = r.dtMainStart,   //tempppp
                            StartDate = r.dtMainStart == null ? Convert.ToDateTime((System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy"))) : Convert.ToDateTime(r.dtMainStart),//temppp
                            EndDate = r.dtMainEnd,
                            ItemType = r.sItemType,
                            SubType = r.sSubType,
                            SwitchId = r.sSwitchId,
                            ItemId = r.sItemId,
                            SequenceId = r.sSequenceId,
                            LoadId = r.iLoadId,
                            Processed = r.iProcessed.GetValueOrDefault() == 1 ? "YES" : "NO"
                        };

                        if (comparisonResult != null)
                        {
                            listComparisonResult.Add(comparisonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return listComparisonResult;
        }

        public int UpdateMBMComparisonResult(int? invoiceId)
        {

            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_MBM_ComparisonResult(invoiceId);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update MBM comparison result [{0}]", result));
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

        public int UpdateMBMComparisonResultCSG(int? invoiceId, long snapshotId, int? loadId, string account, bool ProcessFirst)
        {

            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_MBM_ComparisonResult_CSG(invoiceId, snapshotId, loadId, account, ProcessFirst);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update MBM comparison result [{0}]", result));
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
        /// Gets GetMBMCRMInput record based on invoiceid and loginUser
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="loginUser"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public void GetCRMInputData(string invoiceNumber, string loginUser)
        {
            List<MBMComparisonResult> listComparisonResult = new List<MBMComparisonResult>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.get_CRMInputData(invoiceNumber, loginUser);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
        }

        //new 1582
        /// <summary>
        /// Gets GetMBMCSGInput record based on invoiceid and loginUser
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="loginUser"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public void GetCSGInputData(string invoiceNumber, string loginUser)
        {
            List<MBMComparisonResult> listComparisonResult = new List<MBMComparisonResult>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.get_CSGInputData(invoiceNumber, loginUser);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_FetchCRMDataFromUI(invoiceNumber, loginUser);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
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
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_FetchCSGDataFromUI(invoiceNumber, loginUser);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
        }


        /// <summary>
        /// Gets Unprocessed MBMComparisonResult record based on invoiceid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public List<MBMComparisonResult> GetUnprocessedMBMComparisonResult(int? invoiceId)
        {
            List<MBMComparisonResult> listComparisonResult = new List<MBMComparisonResult>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<Get_UnprocessedMBMComparisonResultResult> results = db.Get_UnprocessedMBMComparisonResult(invoiceId);

                    foreach (Get_UnprocessedMBMComparisonResultResult r in results)
                    {
                        MBMComparisonResult comparisonResult = new MBMComparisonResult()
                        {
                            SnapShotId = r.iSnapshotId,
                            InvoiceId = r.iInvoiceId,
                            CRMAccountNumber = r.sCRMAccountNumber,
                            AssetSearchCode1 = r.sAssetSearchCode1,
                            AssetSearchCode2 = r.sAssetSearchCode2,
                            ProfileName = r.sProfileName,
                            FeatureCode = r.sFeatureCode,
                            GLCode = r.iGLCode,
                            Charge = r.mCharge,
                            ActionType = Enum.GetName(typeof(ActionTypes), r.iActionType),
                            Action = r.iActionType,
                            StartDate = r.dtMainStart,
                            EndDate = r.dtMainEnd,
                            ItemType = r.sItemType,
                            SubType = r.sSubType,
                            SwitchId = r.sSwitchId,
                            ItemId = r.sItemId,
                            SequenceId = r.sSequenceId,
                            LoadId = r.iLoadId,
                        };

                        if (comparisonResult != null)
                        {
                            listComparisonResult.Add(comparisonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
            return listComparisonResult;
        }

        /// <summary>
        /// Gets Unprocessed MBMComparisonResultCSG record based on invoiceid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List<MBMComparisonResult></returns>
        public List<MBMComparisonResultCSG> GetUnprocessedMBMComparisonResultCSG(int? invoiceId)
        {
            List<MBMComparisonResultCSG> listComparisonResult = new List<MBMComparisonResultCSG>();
            var defaultdate= "1/1/0001 12:00:00 AM";
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<Get_UnprocessedMBMComparisonResultResultCSG> results = db.Get_UnprocessedMBMComparisonResultCSG(invoiceId);

                    foreach (Get_UnprocessedMBMComparisonResultResultCSG r in results)
                    {
                        MBMComparisonResultCSG comparisonResult = new MBMComparisonResultCSG()
                        {
                            SnapShotId = r.iSnapshotId,
                            InvoiceId = r.iInvoiceId,
                            //CRMAccountNumber = r.sCSGAccountNumber,
                            AssetSearchCode1 = r.sAssetSearchCode1,
                            AssetSearchCode2 = r.sAssetSearchCode2,
                            ProfileName = r.sProfileName,
                            //FeatureCode = r.sFeatureCode,
                            //GLCode = r.iGLCode,
                            OfferExternalRef = r.sOfferExternalRef,
                            ProductExternalRef = r.sProductExternalRef,
                            PricingPlanExternalRef = r.sPricingPlanExternalRef,
                            MBMUniqueID = r.sMBMUniqueID,
                            SubscriberId = r.sSubscriberId,
                            Charge = r.mCharge,
                            ActionType = Enum.GetName(typeof(CSGActionTypes), r.iActionType),
                            Action = r.iActionType,
                            StartDate = Convert.ToDateTime(r.dtMainStart) == Convert.ToDateTime(defaultdate) ? Convert.ToDateTime((System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy"))) : Convert.ToDateTime(r.dtMainStart),//temppp
                            EndDate = Convert.ToDateTime(r.dtMainEnd) == Convert.ToDateTime(defaultdate) ? Convert.ToDateTime((System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy"))) : Convert.ToDateTime(r.dtMainEnd),
                            ItemType = r.sItemType,
                            SubType = r.sSubType,
                            SwitchId = r.sSwitchId,
                            ItemId = r.sItemId,
                            SequenceId = r.sSequenceId,
                            LoadId = r.iLoadId,
                            Processed = r.iProcessed.GetValueOrDefault() == 1 ? "YES" : "NO",
                            ProcessFirst = r.iProcessFirst.GetValueOrDefault() == 1 ? "YES" : "NO",
                            ContractNumber=r.sContractNumber,
                            ContractStartDate = r.dContractStartDate == Convert.ToDateTime(defaultdate) ? Convert.ToDateTime((System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy"))) : Convert.ToDateTime(r.dContractStartDate.ToString("MM/dd/yyyy")),
                            ContractEndDate = r.dContractEndDate == Convert.ToDateTime(defaultdate) ? Convert.ToDateTime((System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy"))) : Convert.ToDateTime(r.dContractEndDate.ToString("MM/dd/yyyy")),
                            GlDepartmentCode=r.sGLDepartmentCode,
                            IndirectAgentRegion=r.sIndirectAgentRegion,
                            IndirectPartnerCode=r.sIndirectPartnerCode
                        };
                        if (comparisonResult != null)
                        {
                            listComparisonResult.Add(comparisonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
            return listComparisonResult;
        }


         /// <summary>
        /// Gets All csg service attributes Account
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<MBMServiceAttributes> GetAllCSGServiceAttributes()
        {
            List<MBMServiceAttributes> lstCRMAccounts = new List<MBMServiceAttributes>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AllCSGServiceAttributesResult> results = db.get_AllCSGServiceAttributes();

                    foreach (get_AllCSGServiceAttributesResult r in results)
                    {
                        MBMServiceAttributes lstCRMAccount = new MBMServiceAttributes()
                        {
                            ServiceAttributeExternalRef = r.sServiceAttributeExternalRef,
                            AttributeType = r.sAttributeType,
                            ServiceAttributeValue = r.sServiceAttributeValue
                        };

                        if (lstCRMAccount != null)
                        {
                            lstCRMAccounts.Add(lstCRMAccount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstCRMAccounts;
        }


        /// <summary>
        /// Gets All csg service features Account
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<MBMServiceFeatures> GetAllCSGServiceFeatures(int? invoiceId)
        {
            List<MBMServiceFeatures> lstCRMAccounts = new List<MBMServiceFeatures>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AllCSGServiceFeaturesResult> results = db.get_AllCSGServiceFeatures(invoiceId);

                    foreach (get_AllCSGServiceFeaturesResult r in results)
                    {
                        MBMServiceFeatures lstCRMAccount = new MBMServiceFeatures()
                        {
                            ProductExternalRef = r.sProductExternalRef,                            
                            PricingPlanExternalRef = r.sPricingPlanExternalRef
                        };

                        if (lstCRMAccount != null)
                        {
                            lstCRMAccounts.Add(lstCRMAccount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstCRMAccounts;
        }
                
    }
}
