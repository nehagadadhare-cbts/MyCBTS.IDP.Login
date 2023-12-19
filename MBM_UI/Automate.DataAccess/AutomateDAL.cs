using Automate.DataAccess.edmx;
using Automate.DataAccess.Log;
using Automate.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Automate.DataAccess
{
    public class AutomateDAL
    {
        public TokenHistory GetTokenHistory(string invoiceNumber, Guid guid)
        {
            TokenHistory tokenHistory = new TokenHistory();
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    tokenHistory = db.TokenHistories.Where(x => x.InvoiceNumber == invoiceNumber
                                                        && x.Guid == guid).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tokenHistory;
        }

        public void UpdateTokenHistory(string invoiceNumber, Guid guid, string approvedBy)
        {
            TokenHistory tokenHistory = new TokenHistory();
            using (AutomateEntities db = new AutomateEntities())
            {
                tokenHistory = db.TokenHistories.Where(x => x.InvoiceNumber == invoiceNumber
                                                    && x.Guid == guid).FirstOrDefault();
                if (tokenHistory != null)
                {
                    tokenHistory.IsApproved = true;
                    tokenHistory.ApprovedDate = DateTime.Now;
                    tokenHistory.ApprovedBy = approvedBy;

                    db.SaveChanges();
                }
            }
        }

        //public void UpdateMBMAutomateStatus(string invoiceNumber)
        //{
        //    try
        //    {
        //        using (AutomateEntities db = new AutomateEntities())
        //        {
        //            MBMAutomateStatu automateStatus = new MBMAutomateStatu();
        //            automateStatus = db.MBMAutomateStatus.Where(x => x.InvoiceNumber == invoiceNumber).FirstOrDefault();

        //            if (automateStatus != null)
        //            {
        //                automateStatus.IsApprovedByMail = true;
        //                db.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        ExceptionLogging.SendErrorTomail(ex);
        //    }
        //}              

        public List<MBMAutomateStatu> GetInvoicesForPreBilling()
        {
            List<MBMAutomateStatu> automateStatus = new List<MBMAutomateStatu>();
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    automateStatus = (from automate in db.MBMAutomateStatus
                                      join invoiceType in db.mbm_InvoiceTypes
                                      on automate.InvoiceTypeId equals invoiceType.iInvoiceTypeId
                                      where automate.IsNewInvoice == false
                                          && automate.IsProcessed == false
                                          && automate.isPreBillingCompleted == false
                                          && invoiceType.bIsAutoPreBill == true
                                      select automate).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
            return automateStatus;
        }

        public List<MBMAutomateStatu> GetInvoicesForPostBilling()
        {
            List<MBMAutomateStatu> automateStatus = new List<MBMAutomateStatu>();
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    automateStatus = db.MBMAutomateStatus.Where(x => x.IsProcessed == false
                                                                && x.IsImportCRM == true
                                                                && x.IsProcessInvoice == true
                                                                && x.IsExportInvoice == false
                                                                && x.CanStartPostBilling == true
                                                                && x.IsPostBillingCompleted == false).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
            return automateStatus;
        }

        //public bool IsManuallyApproved(string invoiceNumber)
        //{
        //    bool isApproved = false;
        //    using(AutomateEntities db=new AutomateEntities()){
        //        isApproved = Convert.ToBoolean(db.mbm_ProcessWorkFlowStatus.Where(p => p.sInvoiceNumber == invoiceNumber).Select(x => x.sApproveChange));
        //    }
        //    return isApproved;
        //}

        public void UpdateMBMAutomateStatus(int id, string statusOf)
        {
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    MBMAutomateStatu automateStatus = new MBMAutomateStatu();
                    automateStatus = db.MBMAutomateStatus.Where(x => x.InvoiceId == id).FirstOrDefault();

                    if (automateStatus != null)
                    {
                        switch (statusOf)
                        {
                            case "FileUploaded":
                                automateStatus.IsFileTypeUploaded = true;
                                break;

                            case "CompareToCRM":
                                automateStatus.IsComparedToCRM = true;
                                break;

                            case "ViewCRMChanges":
                                automateStatus.IsViwedChanged = true;
                                automateStatus.IsApprovalMailSent = true;
                                break;

                            case "ApproveChanges":
                                automateStatus.IsApprovedByMail = true;
                                automateStatus.IsApprovedChange = true;
                                break;

                            case "SyncCRM":
                                automateStatus.IsSyncedCRM = true;
                                automateStatus.isPreBillingCompleted = true;
                                break;

                            case "ImportCRMData":
                                automateStatus.IsImportCRM = true;
                                break;

                            case "ProcessInvoice":
                                automateStatus.IsProcessInvoice = true;
                                break;

                            case "ExportInvoice":
                                automateStatus.IsExportInvoice = true;
                                automateStatus.IsPostBillingCompleted = true;
                                automateStatus.CanStartPostBilling = false;
                                automateStatus.IsProcessed = true;
                                break;
                        }
                        automateStatus.LastAction = statusOf;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
                throw ex;
            }
        }


        public void UpdateMBMStatus(string invoiceNumber, string statusOf)
        {
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    mbm_ProcessWorkFlowStatus processFlow = new mbm_ProcessWorkFlowStatus();
                    processFlow = db.mbm_ProcessWorkFlowStatus.Where(p => p.sInvoiceNumber == invoiceNumber).FirstOrDefault();
                    if (processFlow != null)
                    {
                        switch (statusOf)
                        {
                            case "CompareToCRM":
                                processFlow.sCompareToCRM = true;
                                break;

                            case "ViewCRMChanges":
                                processFlow.sViewChange = true;
                                break;

                            case "ApproveChanges":
                                processFlow.sApproveChange = true;
                                break;

                            case "SyncCRM":
                                processFlow.sSyncCRM = true;
                                break;

                            case "ImportCRMData":
                                processFlow.sImportCRMData = true;
                                break;

                            case "ProcessInvoice":
                                processFlow.sProcessInvoice = true;
                                break;

                            case "ExportInvoice":
                                processFlow.sExportInvoiceFile = true;
                                break;
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
        }

        public UploadedFileCollection GetFileUploads(int? invoiceID)
        {
            UploadedFileCollection entities = new UploadedFileCollection();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    var results = db.Get_InvoiceFileUploads_ByInvoiceId(invoiceID);

                    foreach (var r in results)
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
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }

            return entities;
        }

        public Invoice GetInvoiceByID(int invoiceID)
        {
            Invoice invoice = new Invoice();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    var r = db.get_InvoiceDetailsByNumber(invoiceID).FirstOrDefault();

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
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }

            return invoice;
        }

        public string GetCustomerName(int id)
        {
            string customerName = "";
            using (AutomateEntities db = new AutomateEntities())
            {
                customerName = db.mbm_InvoiceTypes.Where(x => x.iInvoiceTypeId == id).Select(name => name.sInvoiceTypeName).FirstOrDefault();
            }
            return customerName;
        }

        public int GetInvoiceIdByNumber(string invoiceNumber)
        {
            int id = 0;
            using (AutomateEntities db = new AutomateEntities())
            {
                id = db.mbm_Invoice.Where(i => i.sInvoiceNumber == invoiceNumber).Select(s => s.iInvoiceId).FirstOrDefault();
            }
            return id;
        }
       public string  GetMBMBillingSystem(int? invoiceId)
        {
            string billingSystem = "";
            var invoiceTypeId = 0;
            using (AutomateEntities db = new AutomateEntities())
            {
                invoiceTypeId = db.mbm_Invoice.Where(x => x.iInvoiceId == invoiceId).Select(x => x.iInvoiceTypeId).FirstOrDefault();
                billingSystem = db.mbm_InvoiceTypes.Where(x => x.iInvoiceTypeId == invoiceTypeId).Select(name => name.sBillingSystem).FirstOrDefault();
            }
            return billingSystem;
        }

        public List<MBMComparisonResult> GetMBMComparisonResult(int? invoiceId)
        {
            List<MBMComparisonResult> listComparisonResult = new List<MBMComparisonResult>();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    var results = db.Get_MBM_ComparisonResult(invoiceId);

                    foreach (var r in results)
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
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
            return listComparisonResult;
        }

        public List<MBMComparisonResultCSG> GetMBMComparisonResultCSG(int? invoiceId)
        {
            List<MBMComparisonResultCSG> listComparisonResult = new List<MBMComparisonResultCSG>();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                   var results = db.Get_MBM_ComparisonResultCSG(invoiceId);

                    foreach (var r in results)
                    {
                        MBMComparisonResultCSG comparisonResult = new MBMComparisonResultCSG()
                        {
                            SnapShotId = r.iSnapshotId,
                            InvoiceId = r.iInvoiceId,
                            AssetSearchCode1 = r.sAssetSearchCode1,
                            AssetSearchCode2 = r.sAssetSearchCode2,
                            ProfileName = r.sProfileName,
                            OfferExternalRef = r.sOfferExternalRef,
                            ProductExternalRef = r.sProductExternalRef,
                            PricingPlanExternalRef = r.sPricingPlanExternalRef,
                            MBMUniqueID = r.sMBMUniqueID,
                            SubscriberId = r.sCSGAccountNumber,

                            Charge = r.mCharge,
                            ActionType = Enum.GetName(typeof(CSGActionTypes), r.iActionType),
                            Action = r.iActionType,
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
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }

            return listComparisonResult;
        }

        public List<MBMComparisonResult> GetUnprocessedMBMComparisonResult(int? invoiceId)
        {
            List<MBMComparisonResult> listComparisonResult = new List<MBMComparisonResult>();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    var results = db.Get_UnprocessedMBMComparisonResult(invoiceId);

                    foreach (var r in results)
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
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
            return listComparisonResult;
        }

        public int GetUnprocessedMBMComparisonResultCount(int id)
        {
            int count = 0;
            List<MBMComparisonResult> listComparisonResult = GetUnprocessedMBMComparisonResult(id);
            count = listComparisonResult.Count();

            return count;
        }

        public void InsertTokenHistory(Guid guid, string invoiceNumber)
        {
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    var token = new TokenHistory
                    {
                        Guid = guid,
                        InvoiceNumber = invoiceNumber,
                        InsertedDate = DateTime.Now
                    };
                    db.TokenHistories.Add(token);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
        }

        public void InsertInvoiceInMBMAutomate()
        {
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    db.insert_InvoiceInMBMAutomate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
        }

        public void InsertCreateProcess()
        {
            //try
            //{
            //    using (AutomateEntities db = new AutomateEntities())
            //    {
            //        db.insert_InvoiceByMBMAutomate();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    ExceptionLogging.SendErrorTomail(ex);
            //}
        }

        //cbe9719       
        public void InsertCreateProcessById()
        {

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    var result = db.insert_InvoiceByMBMAutomate_all().ToList(); //list
                    foreach (var InvoiceTypeID in result)
                    {
                        if (InvoiceTypeID > 0)
                            db.insert_InvoiceByMBMAutomate_Process(InvoiceTypeID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
        }
        //cbe9719

        public void PostBillingStart(PostBillingInMBMAutomate_GetInvoiceRecords_Result postBillingInvoiceRecord)
        {
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    string rt1Status, rt2Status, rt3Status, rt4Status, rt5Status = string.Empty;
                    //db.PostBillingInMBMAutomate();  
                    //var result = db.PostBillingInMBMAutomate_GetInvoiceRecords().ToList();
                    if (postBillingInvoiceRecord.InvoiceId > 0)
                    {
                        db.usp_mbm_CBADCRMData(postBillingInvoiceRecord.InvoiceId, null);
                        db.usp_mbm_GetMultiLocationSummaryData(postBillingInvoiceRecord.InvoiceId, null);
                        db.usp_mbm_CBADTaxData(postBillingInvoiceRecord.InvoiceId, null);
                        db.update_ProcessWorkFlowStatus(postBillingInvoiceRecord.InvoiceNumber, true, true, true, true, true, false, false);
                        UpdateMBMAutomateStatus(postBillingInvoiceRecord.InvoiceId.GetValueOrDefault(), "ImportCRMData");
                        int rt1 = db.mbm_RecordType1_Process(postBillingInvoiceRecord.InvoiceId, null, null);
                        rt1Status = rt1 == -1 ? "Completed" : "Failed";
                        db.Update_InvoiceRecordTypes(postBillingInvoiceRecord.InvoiceId, rt1Status, null, null, null, null, null);
                        int rt2 = db.mbm_RecordType2_Process(postBillingInvoiceRecord.InvoiceId, null, null);
                        rt2Status = rt2 == -1 ? "Completed" : "Failed";
                        db.Update_InvoiceRecordTypes(postBillingInvoiceRecord.InvoiceId, rt1Status, rt2Status, null, null, null, null);
                        int rt1summary = db.mbm_RT1Summary_Process(postBillingInvoiceRecord.InvoiceId);
                        int rt2summary = db.mbm_RT2Summary_Process(postBillingInvoiceRecord.InvoiceId);
                        int rt4 = db.mbm_RecordType4_Process(postBillingInvoiceRecord.InvoiceId, null, null);
                        rt4Status = rt4 == -1 ? "Completed" : "Failed";
                        db.Update_InvoiceRecordTypes(postBillingInvoiceRecord.InvoiceId, rt1Status, rt2Status, null, rt4Status, null, null);
                        int rt4summary = db.mbm_RT4Summary_Process(postBillingInvoiceRecord.InvoiceId);
                        int rt3 = db.mbm_RecordType3_Process(postBillingInvoiceRecord.InvoiceId, null, null);
                        rt3Status = rt3 == -1 ? "Completed" : "Failed";
                        db.Update_InvoiceRecordTypes(postBillingInvoiceRecord.InvoiceId, rt1Status, rt2Status, rt3Status, rt4Status, null, null);
                        int rt5 = db.mbm_RecordType5_Process(postBillingInvoiceRecord.InvoiceId, null, null);
                        rt5Status = rt5 == -1 ? "Completed" : "Failed";
                        db.Update_InvoiceRecordTypes(postBillingInvoiceRecord.InvoiceId, rt1Status, rt2Status, rt3Status, rt4Status, rt5Status, null);
                        db.update_ProcessWorkFlowStatus(postBillingInvoiceRecord.InvoiceNumber, true, true, true, true, true, true, false);
                        UpdateMBMAutomateStatus(postBillingInvoiceRecord.InvoiceId.GetValueOrDefault(), "ProcessInvoice");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
                throw ex;
            }
        }

        public List<PostBillingInMBMAutomate_GetInvoiceRecords_Result> GetPostBillingInvoiceRecords()
        {
            List<PostBillingInMBMAutomate_GetInvoiceRecords_Result> postBillingInvoiceRecords = new List<PostBillingInMBMAutomate_GetInvoiceRecords_Result>();
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    postBillingInvoiceRecords = db.PostBillingInMBMAutomate_GetInvoiceRecords().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
                throw ex;
            }

            return postBillingInvoiceRecords;
        }

        public MBMAutomateStatu GetMBMAutomateStatus(int? invoiceId)
        {
            MBMAutomateStatu automateStatus = new MBMAutomateStatu();
            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    automateStatus = db.MBMAutomateStatus.Where(x => x.InvoiceId == invoiceId).Single();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
            return automateStatus;
        }
    }
}
