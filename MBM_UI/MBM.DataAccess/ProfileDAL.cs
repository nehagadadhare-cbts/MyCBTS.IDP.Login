using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;
using MBM.DataAccess;
using System.Data.Linq;

namespace MBM.DataAccess
{
    public class ProfileDAL
    {
        #region Setup

        private readonly Logger _logger;
        private readonly string _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConnection">database connection</param>
        public ProfileDAL(string dbConnection)
        {
            this._connection = dbConnection;
            _logger = new Logger(dbConnection);
        }

        #endregion

        //ERO 4002 start
        #region Profiles
        public List<Profile> getProfileByInvoiceType(int invoiceTypeId)
        {
            List<Profile> entities = new List<Profile>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_Profiles_GetByInvoiceIdResult> results = db.mbm_Profiles_GetByInvoiceId(invoiceTypeId);

                    foreach (mbm_Profiles_GetByInvoiceIdResult r in results)
                    {
                        Profile entity = new Profile()
                        {

                            ProfileId = r.iProfileId,
                            Description = r.sDescription,
                            ProfileName = r.sProfileName
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
        public List<Profile> getChargeStringIdentifierByInvoiceType(int invoiceTypeId)
        {
            List<Profile> entities = new List<Profile>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ChargeStringIdentifier_GetByInvoiceIdResult> results = db.mbm_ChargeStringIdentifier_GetByInvoiceId(invoiceTypeId);

                    foreach (mbm_ChargeStringIdentifier_GetByInvoiceIdResult r in results)
                    {
                        Profile entity = new Profile()
                        {

                            ProfileId = r.iProfileId,
                            ChargeStringIdentifier = r.iChargeStringIdentifier,
                            CatalogItemId = r.iCatalogItemId
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

        public int CreateProfile(Profile profile)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.mbm_Profiles_Create(profile.ProfileName, profile.InvoiceType, profile.Description);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Add  Profile [{0}]", result));
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

        public int DeleteProfile(int profileid)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    return db.mbm_Profiles_DeleteById(profileid);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        public int EditProfile(Profile profile)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.mbm_Profiles_Edit(profile.ProfileId, profile.ProfileName, profile.Description);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Add  Profile [{0}]", result));
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

        public bool IsProfileExistInEnhanceData(int profileId)
        {
            int recCount = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    recCount = db.mbm_Profiles_VerifyInEnhanceData(profileId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return (recCount > 0) ? true : false;
        }
        #endregion

        #region Profile Charges
        public List<ProfileCharge> getProfileChargesByInvoiceType(int invoiceTypeId, string invoiceTypeName)
        {
            List<ProfileCharge> entities = new List<ProfileCharge>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ProfileCharges_GetByInvoiceResult> results = db.mbm_ProfileCharges_GetByInvoice(invoiceTypeId, invoiceTypeName);

                    foreach (mbm_ProfileCharges_GetByInvoiceResult r in results)
                    {
                        ProfileCharge entity = new ProfileCharge()
                        {
                            ProfileId = r.iprofileid,
                            ChargeId = r.ichargeid,
                            ProfileName = r.sprofilename,
                            ChargeDescription = r.schargetype,
                            ChargeAmount = r.mcharge,
                            GLCode = r.iglcode,
                            Feature = r.sfeature
                            //ProfileIdentifier = r.sChargeIdentifier //SERO-1582
                            //  ProductId=r.sprofilename,
                            //ChargeStringIdentifier=r.schargetype,
                            //PricingPlanId=r.sfeature,
                            //OfferId = r.sChargeIdentifier,
                            //Price=r.mcharge




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
        //PC for CSG
        public List<ProfileCharge> getCSGProfileChargesByInvoiceType(int invoiceTypeId, string invoiceTypeName)
        {
            List<ProfileCharge> entities = new List<ProfileCharge>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ProfileCharges_GetByInvoice_CSGResult> results = db.mbm_ProfileCharges_GetByInvoice_CSG(invoiceTypeId, invoiceTypeName);

                    foreach (mbm_ProfileCharges_GetByInvoice_CSGResult r in results)
                    {
                        ProfileCharge entity = new ProfileCharge()
                        {

                            ProfileName = r.sprofilename,
                            ChargeDescription = r.sChargeIdentifier,
                            ChargeId = r.ichargeid,
                            CatalogItemId = r.icatalogitemid,
                            ProfileId = r.iprofileid,
                            InvoiceTypeId = r.iinvoicetypeid,
                               ChargeAmount = r.mcharge,
                            //EffectiveStartDate=r.dEffectiveStartDate,
                            bIsQuorumSynced=r.bIsQuorumSynced

                                                       
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

        //end
        //SERO-1582

        public List<ProfileCharge> getCSGPricingPlanIdByInvoiceId(int invoiceTypeId, string invoiceTypeName)
        {
            List<ProfileCharge> entities = new List<ProfileCharge>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_CSGPricingPlanId_GetByInvoiceResult> results = db.mbm_CSGPricingPlanId_GetByInvoice(invoiceTypeId, invoiceTypeName);

                    foreach (mbm_CSGPricingPlanId_GetByInvoiceResult r in results)
                    {
                        ProfileCharge entity = new ProfileCharge()
                        {
                            //ProfileId = r.iprofileid,
                            //ChargeId = r.ichargeid,
                            // ProfileName = r.sprofilename,
                            // ChargeDescription = r.schargetype,
                            //ChargeAmount = r.mcharge,
                            // GLCode = r.iglcode,
                            //Feature = r.sfeature
                            //ProfileIdentifier = r.sChargeIdentifier //SERO-1582
                            //  ProductId=r.sprofilename,
                            //ChargeStringIdentifier=r.schargetype,
                            PricingPlanId = r.iPricingPlanId
                            //OfferId = r.sChargeIdentifier,
                            //Price=r.mcharge




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

        public ProfileCharge ValidateProfileChargeFromCRM(string glCode)
        {
            ProfileCharge profileCharge = new ProfileCharge();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ProfileCharges_ValidateFromCRMResult> result = db.mbm_ProfileCharges_ValidateFromCRM(glCode);

                    foreach (mbm_ProfileCharges_ValidateFromCRMResult res in result)
                    {
                        profileCharge.ChargeDescription = res.ProfileDesc;
                        profileCharge.Feature = res.Feature;
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return profileCharge;
        }

        public int CreateProfileCharge(ProfileCharge profileCharge)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_ProfileCharges_Create(profileCharge.ProfileId, profileCharge.GLCode.ToString(), profileCharge.ChargeAmount,
                                                 profileCharge.ChargeDescription, profileCharge.Feature, profileCharge.InvoiceTypeId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        }

        //PC
        public int CreateProfileChargeCSG(ProfileCharge profileCharge)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_ProfileCharges_Create_CSG(profileCharge.ProfileId, profileCharge.InvoiceTypeId, profileCharge.CatalogItemId,profileCharge.ChargeAmount);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        }
        public int EditprofileChargesForCSG(ProfileCharge profileCharges)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.mbm_ProfileCharges_CSG_EditByChargeId(profileCharges.ProfileId, profileCharges.InvoiceTypeId, profileCharges.CatalogItemId, profileCharges.ChargeAmount);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Add  Profile [{0}]", result));
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
        public int EditprofileCharges(ProfileCharge profileCharges)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.mbm_ProfileCharges_EditByChargeId(profileCharges.ChargeId, profileCharges.ChargeAmount);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Add  Profile [{0}]", result));
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

        public int DeleteProfileCharge(int chargeId)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    return db.mbm_ProfileCharges_DeleteByChargeId(chargeId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        public int DeleteProfileChargeCSG(int chargeId)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    return db.mbm_ProfileCharges_CSG_DeleteByChargeId(chargeId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        public int ValidateProfileChargeCSG(int chargeId)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    return db.mbm_CSGProfileCharges_VerifyInEnhanceDataByChargeId(chargeId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        public bool IsProfileChargeExistInEnhanceData(int chargeId)
        {
            int recCount = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    recCount = db.mbm_ProfileCharges_VerifyInEnhanceData(chargeId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return (recCount > 0) ? true : false;
        }
        #endregion
        //ERO 4002 end

        #region ManualCharge

        public int? CreateManualChargeFile(int? invoiceTypeId, string fileName, string fileStatus, string userName)
        {
            int? result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_ManualChargeFile_Create(invoiceTypeId, fileName, fileStatus, userName, ref result);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Add  Profile [{0}]", result));
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

        public List<ManualChargeFileDetails> getMaualChargeFilesInfoByInvoiceType(int invoiceTypeId)
        {
            List<ManualChargeFileDetails> entities = new List<ManualChargeFileDetails>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ManualCharge_GetFilesInfoByInvoiceIdResult> results = db.mbm_ManualCharge_GetFilesInfoByInvoiceId(invoiceTypeId);

                    foreach (mbm_ManualCharge_GetFilesInfoByInvoiceIdResult r in results)
                    {
                        ManualChargeFileDetails entity = new ManualChargeFileDetails()
                        {
                            MCharge_FileId = r.iFileId,
                            InvoiceTypeId = r.iInvoiceTypeId,
                            MCharge_FileName = r.ManualChargeFileName,
                            MCharge_filepath = r.FilePath,
                            MCharge_file_RecordCount = r.RecordCount,
                            MCharge_FileStatus = r.sFileStatus,
                            MCharge_ImportedBy = r.sImportedBy,
                            MCharge_Imported = r.dtImported
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

        public int DeleteManulaChargeByFileId(int fileId)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    return db.mbm_ManualCharge_DeleteByFileId(fileId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        //mbm_ManualCharge_GetChargesByFileId
        public List<ManualCharge_Stage> getMaualChargesByFileId(int fileId)
        {
            List<ManualCharge_Stage> entities = new List<ManualCharge_Stage>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ManualCharge_GetChargesByFileIdResult> results = db.mbm_ManualCharge_GetChargesByFileId(fileId);

                    foreach (mbm_ManualCharge_GetChargesByFileIdResult r in results)
                    {
                        ManualCharge_Stage entity = new ManualCharge_Stage()
                        {
                            Subidentifier = r.sSubIdentifier,
                            LegalEntity = r.sLegalEntity,
                            AssetSearch = r.sAssetSearch,
                            ServiceProfile = r.sServiceProfileId,
                            StartDate = r.dtMainStart,
                            EndDate = r.dtMainEnd,
                            DirectoryNumber = r.sDirectoryNumber,
                            ServiceProfileUId = r.sServiceProfileUid,
                            Status = r.sStatus,
                            Comments = r.sComment
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

        //mbm_ManualCharge_getSuccessRecordsByInvoiceId
        public List<ManualCharge_Success> getSuccessRecordsByInvoiceId(int invoiceId)
        {
            List<ManualCharge_Success> entities = new List<ManualCharge_Success>();
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_ManualCharge_getSuccessRecordsByInvoiceIdResult> results = db.mbm_ManualCharge_getSuccessRecordsByInvoiceId(invoiceId);

                    foreach (mbm_ManualCharge_getSuccessRecordsByInvoiceIdResult r in results)
                    {
                        ManualCharge_Success entity = new ManualCharge_Success()
                        {
                            Subidentifier = r.sSubIdentifier,
                            LegalEntity = r.sLegalEntity,
                            AssetSearch = r.sAssetSearch,
                            ServiceProfile = r.sServiceProfileId,
                            StartDate = r.dtMainStart,
                            EndDate = r.dtMainEnd,
                            DirectoryNumber = r.sDirectoryNumber,
                            ServiceProfileUId = r.sServiceProfileUid,
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

        public int? ProcessManualChargeData(int? fileid)
        {
            int? result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_ManualCharge_ProcessData(fileid);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Process Data [{0}]", result));
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

        public ManualChargeFileSummary getManualChargeFileProcessSummary(int fileId)
        {
            ManualChargeFileSummary summary;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    int? successrec = 0;
                    int? failurerec = 0;
                    int results = db.mbm_ManualCharge_GetFileProcessSummary(fileId, ref successrec, ref failurerec);

                    summary = new ManualChargeFileSummary()
                    {
                        FileId = fileId,
                        SuccessRecords = successrec ?? default(int),
                        FailedRecords = failurerec ?? default(int)
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
            return summary;
        }

        public int UpdateManualChargeFileStatus(int? fileid, string filestatus)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.mbm_ManualCharge_updateFileStatus(fileid, filestatus);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to update manual charge file status [{0}]", result));
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
        #endregion
    }
}
