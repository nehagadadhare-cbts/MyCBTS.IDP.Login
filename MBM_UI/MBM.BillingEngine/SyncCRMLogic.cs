using MBM.BillingEngine.CBTSService;
using MBM.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MBM.BillingEngine
{
    public class SyncCRMLogic
    {
        string strConnectionString;
        public SyncCRMLogic()
        {
            strConnectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString(); ;
        }
        
        public long InvoiceSnapshotId { get; set; }
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

        public string SyncCRM(int invoiceId,string submittedBy,string invoiceNumber)
        {
            string syncCRMStatus = string.Empty;
            try
            {                
               // btnSyncCRM.Enabled = false;

                string ehcsItemType = ConfigurationManager.AppSettings["EHCSItemType"];
                string ehcsSubType = ConfigurationManager.AppSettings["EHCSSubType"];

                CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);

                List<CRMActivateBillItem> activateBillItemList = new List<CRMActivateBillItem>();
                List<CRMDeactivateBillItem> deactivateBillItemList = new List<CRMDeactivateBillItem>();
                List<CRMAddSubBillItem> addSubBillItemList = new List<CRMAddSubBillItem>();
                List<CRMUpdateSubBillItem> updateSubBillItemList = new List<CRMUpdateSubBillItem>();
                List<CRMDeleteSubBillItem> deleteSubBillItemList = new List<CRMDeleteSubBillItem>();
                List<CRMActivateTCPAUTH> activateTCPAuthList = new List<CRMActivateTCPAUTH>();
                List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();                

                listMBMComparisonResult = crmAccount.GetUnprocessedMBMComparisonResult(invoiceId);

                CRMAccountConfiguration accountConfiguration = new CRMAccountConfiguration();
                List<CRMAccountConfiguration> accountConfigurationList = new List<CRMAccountConfiguration>();
                CRMAccountConfigurationRequest accountConfigRequest = new CRMAccountConfigurationRequest();
                List<CRMDeactivateTCPAUTH> deactivateTCPAuthList = new List<CRMDeactivateTCPAUTH>();

                List<string> listCRMAccounts = new List<string>();

                foreach (var result in listMBMComparisonResult)
                {
                    if (!listCRMAccounts.Contains(result.CRMAccountNumber))
                        listCRMAccounts.Add(result.CRMAccountNumber);
                }

                if (listMBMComparisonResult.Count() > 0)
                {
                    long snapshotId = listMBMComparisonResult.FirstOrDefault().SnapShotId.Value;
                    InvoiceSnapshotId = snapshotId;

                    //Add CRM Bill/Sub Bill items to be added/update/deleted.
                    foreach (var account in listCRMAccounts)
                    {
                        foreach (var item in listMBMComparisonResult.Where(i => i.CRMAccountNumber == account))
                        {
                            int action = item.Action;

                            switch (action)
                            {
                                case (int)ActionTypes.DeleteBillItem:

                                    deactivateBillItemList.Add(new CRMDeactivateBillItem()
                                    {
                                        UserId = item.AssetSearchCode1,
                                        DeactivationDate = item.EndDate.Value,
                                        ItemType = item.ItemType,
                                        SubType = item.SubType,
                                        BillItemId = item.ItemId,
                                        SwitchId = item.SwitchId
                                    });

                                    break;

                                case (int)ActionTypes.AddBillItem:

                                    if (item.ItemType == ehcsItemType && item.SubType == ehcsSubType)
                                    {
                                        activateBillItemList.Add(new CRMActivateBillItem()
                                        {
                                            UserId = item.AssetSearchCode1,
                                            ActivationDate = item.StartDate.Value
                                        });
                                    }
                                    break;

                                case (int)ActionTypes.DeleteCharge:

                                    if (item.ItemType == ehcsItemType && item.SubType == ehcsSubType)
                                    {
                                        deleteSubBillItemList.Add(new CRMDeleteSubBillItem()
                                        {
                                            UserId = item.AssetSearchCode1,
                                            FeatureCode = item.FeatureCode,
                                            EndDate = item.EndDate.Value,
                                            Charge = item.Charge.Value,
                                            StartDate = item.StartDate.Value
                                            //ERO - 4668
                                            //StartDate = item.StartDate.Value
                                            //ERO - 4668
                                        });
                                    }
                                    break;

                                case (int)ActionTypes.AddCharge:

                                    if (item.ItemType == ehcsItemType && item.SubType == ehcsSubType)
                                    {
                                        addSubBillItemList.Add(new CRMAddSubBillItem()
                                        {
                                            UserId = item.AssetSearchCode1,
                                            Charge = item.Charge.Value,
                                            StartDate = item.StartDate.Value,
                                            FeatureCode = item.FeatureCode
                                        });
                                    }
                                    break;

                                case (int)ActionTypes.UpdateCharge:

                                    //if (item.ItemType == ehcsItemType && item.SubType == ehcsSubType)
                                    //{
                                    //    updateSubBillItemList.Add(new CRMUpdateSubBillItem()
                                    //    {
                                    //        UserId = item.AssetSearchCode1,
                                    //        Charge = item.Charge.Value,
                                    //        StartDate = item.StartDate.Value,
                                    //        FeatureCode = item.FeatureCode
                                    //    });
                                    //}
                                    //else
                                    //{
                                    updateSubBillItemList.Add(new CRMUpdateSubBillItem()
                                    {
                                        UserId = item.AssetSearchCode1,
                                        Charge = item.Charge.Value,
                                        StartDate = item.StartDate.Value,
                                        FeatureCode = item.FeatureCode,
                                        ItemType = item.ItemType,
                                        SubType = item.SubType,
                                        BillItemId = item.ItemId,
                                        ItemSequence = item.SequenceId,
                                        EndDate = item.EndDate,
                                        EndDateSpecified = true,
                                        SwitchId = item.SwitchId
                                    });
                                    //}
                                    break;

                                case (int)ActionTypes.AddTelephone:

                                    activateTCPAuthList.Add(new CRMActivateTCPAUTH()
                                    {
                                        TelephoneNumber = item.AssetSearchCode2,
                                        Action = "New"
                                    });
                                    break;

                                case (int)ActionTypes.ChangeTelephone:

                                    activateTCPAuthList.Add(new CRMActivateTCPAUTH()
                                    {
                                        TelephoneNumber = item.AssetSearchCode2,
                                        Action = "Change"
                                    });
                                    break;

                                case (int)ActionTypes.DeleteTelephone:

                                    deactivateTCPAuthList.Add(new CRMDeactivateTCPAUTH()
                                    {
                                        TelephoneNumber = item.AssetSearchCode2,
                                    });
                                    break;
                            }
                        }

                        //Populate Service input object
                        accountConfiguration.ActivateBillItems = activateBillItemList.ToArray();
                        accountConfiguration.DeactivateBillItems = deactivateBillItemList.ToArray();
                        accountConfiguration.AddSubBillItems = addSubBillItemList.ToArray();
                        accountConfiguration.DeleteSubBillItems = deleteSubBillItemList.ToArray();
                        accountConfiguration.UpdateSubBillItems = updateSubBillItemList.ToArray();
                        accountConfiguration.ActivateTCPAUTHs = activateTCPAuthList.ToArray();
                        accountConfiguration.DeactivateTCPAUTHs = deactivateTCPAuthList.ToArray();

                        accountConfigurationList.Add(accountConfiguration);
                        accountConfigRequest.BillAndSubBillItems = accountConfigurationList.ToArray();

                        accountConfigRequest.CRMAccountNumber = Convert.ToInt32(account);
                        accountConfigRequest.CRMAccountNumberSpecified = true;

                        accountConfigRequest.SnapshotID = (long)listMBMComparisonResult.FirstOrDefault().SnapShotId;
                        accountConfigRequest.SnapshotIDSpecified = true;

                        accountConfigRequest.SubmittedUser = submittedBy;

                        //CBTService Call
                        CBTSService.CBTSService cbtsService = new CBTSService.CBTSService();
                        cbtsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["QuorumCBTSServiceCallTimeout"]); //600000;
                        cbtsService.CRMAccountConfiguration(accountConfigRequest);

                        //Update Processed Flag.
                        crmAccount.UpdateMBMComparisonResult(invoiceId);

                        //Clear List items.
                        activateBillItemList.Clear();
                        deactivateBillItemList.Clear();
                        addSubBillItemList.Clear();
                        deleteSubBillItemList.Clear();
                        updateSubBillItemList.Clear();
                        activateTCPAuthList.Clear();
                        accountConfigurationList.Clear();
                        deactivateTCPAuthList.Clear();
                    }

                    //Logic to enable next button in the work-flow.
                    //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.SyncCRM);

                    ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
                    objStatus.InvoiceNumber = invoiceNumber;
                    objStatus.CompareToCRM = true;
                    objStatus.ViewChange = true;
                    objStatus.ApproveChange = true;
                    objStatus.SyncCRM = true;
                    objStatus.ImportCRMData = false;
                    objStatus.ProcessInvoice = false;
                    objStatus.ExportInvoiceFile = false;

                    //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
                    ProcessWorkflowStatusBL processWorkflowStatusBL = new ProcessWorkflowStatusBL(strConnectionString);
                    processWorkflowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(objStatus);

                    InvoiceBL invoiceBl = new InvoiceBL(strConnectionString);
                    MBMAutomateStatus mbmAutomateStatus = invoiceBl.GetMBMAutomateStatus(invoiceId);
                    if (mbmAutomateStatus.InvoiceId != null)
                    {                        
                        BillingEngine billingEngine=new BillingEngine(strConnectionString);
                        billingEngine.ProcessWorkFlowStatusBL.UpdateAutomationWorkFlowStatusByInvoiceId(invoiceId, true, "SyncCRM");
                    }
                    
                    syncCRMStatus= "Synced";
                }
                else
                {
                    ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
                    objStatus.InvoiceNumber = invoiceNumber;
                    objStatus.CompareToCRM = true;
                    objStatus.ViewChange = true;
                    objStatus.ApproveChange = true;
                    objStatus.SyncCRM = true;
                    objStatus.ImportCRMData = false;
                    objStatus.ProcessInvoice = false;
                    objStatus.ExportInvoiceFile = false;

                    //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
                    ProcessWorkflowStatusBL processWorkflowStatusBL = new ProcessWorkflowStatusBL(strConnectionString);
                    processWorkflowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(objStatus);

                    syncCRMStatus= "NoRecords";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return syncCRMStatus;
        }
    }
}
