//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MBM.BillingEngine
//{
//    class SyncCSGLogic
//    {
//    }
//}


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
    public class SyncCSGLogic
    {
        string strConnectionString;

        public SyncCSGLogic()
        {
            strConnectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString(); ;
        }

        public long InvoiceSnapshotId { get; set; }

        enum CSGActionTypes
        {
            AddService = 1,
            AddServiceFeature = 2,
            RemoveServiceFeature = 3,
            RemoveService = 4
        };

        //public string SyncCSG(int invoiceId, string submittedBy, string invoiceNumber)
        //{
        //    string syncCSGStatus = string.Empty;
        //    //return syncCSGStatus;
        //    try
        //    {
        //        // btnSyncCRM.Enabled = false;
        //        string ehcsItemType = ConfigurationManager.AppSettings["EHCSItemType"];
        //        string ehcsSubType = ConfigurationManager.AppSettings["EHCSSubType"];
        //        CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
        //        List<CSGAddService> addServiceLineItemList = new List<CSGAddService>();
        //        List<CSGAddServiceFeature> addServiceFeatureLineItemList = new List<CSGAddServiceFeature>();
        //        List<CSGRemoveServiceFeature> removeServiceFeatureLineItemList = new List<CSGRemoveServiceFeature>();
        //        List<CSGRemoveService> removeServiceLineItemList = new List<CSGRemoveService>();
        //        List<MBMComparisonResultCSG> listMBMComparisonResultCSG = new List<MBMComparisonResultCSG>();
        //        List<MBMComparisonResultCSG> addListMBMComparisonResultCSG = new List<MBMComparisonResultCSG>();
        //        List<MBMServiceAttributes> listMBMServiceAttributeResultCSG = new List<MBMServiceAttributes>();
        //        List<MBMServiceFeatures> listMBMServiceFeatureLDResultCSG = new List<MBMServiceFeatures>();
        //        List<MBMServiceFeatures> listMBMServiceFeatureCSGResult = new List<MBMServiceFeatures>();
        //        CSGAccountConfiguration accountConfiguration = new CSGAccountConfiguration();
        //        List<CSGAccountConfiguration> accountConfigurationList = new List<CSGAccountConfiguration>();
        //        MBMServiceFeatureOrderSnapshotRequest accountConfigRequest = new MBMServiceFeatureOrderSnapshotRequest();
        //        // List<CRMDeactivateTCPAUTH> deactivateTCPAuthList = new List<CRMDeactivateTCPAUTH>();
        //        List<CSGNewServiceItems> newserviceItemList = new List<CSGNewServiceItems>();
        //       // List<CSGServiceFeatures> serviceCSGLDUsagePlanFeaturesList = new List<CSGServiceFeatures>();
        //        List<CSGServiceFeatures> AddserviceFeaturesList = new List<CSGServiceFeatures>();
        //        List<CSGServiceFeatures> RemoveserviceFeaturesList = new List<CSGServiceFeatures>();
        //        List<CSGServiceAttributes> serviceAttributesList = new List<CSGServiceAttributes>();
        //        //addservicef
        //        List<CSGServiceIdentifiers> addServiceIdentifiersList = new List<CSGServiceIdentifiers>();
        //        List<CSGServiceIdentifiers> addServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
        //        List<CSGServiceFeatures> serviceFeaturesList = new List<CSGServiceFeatures>(); 
        //        List<CSGServiceIdentifiers> removeServiceFeatureIdentifiersList = new List<CSGServiceIdentifiers>();
        //        List<CSGServiceIdentifiers> removeServiceFeatureIdentifiersListTemp = new List<CSGServiceIdentifiers>();
        //        List<CSGServiceIdentifiers> removeServiceIdentifiersList = new List<CSGServiceIdentifiers>();
        //        List<CSGServiceIdentifiers> removeServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
        //     //   List<CSGAddLDPlans> AddLDPlanLineItems = new List<CSGAddLDPlans>();
        //        //end addsevFt                
        //        CSGAddService addServ = new CSGAddService();
        //        List<string> listCSGAccounts = new List<string>();
        //        listMBMComparisonResultCSG = crmAccount.GetUnprocessedMBMComparisonResultCSG(invoiceId);
        //        //to get service attributes
        //        listMBMServiceAttributeResultCSG = crmAccount.GetAllCSGServiceAttributes();
        //        //to get service features
        //        listMBMServiceFeatureLDResultCSG = crmAccount.GetAllCSGServiceFeatures();
        //        listCSGAccounts = listMBMComparisonResultCSG.AsEnumerable().Select(x => x.SubscriberId).Distinct().ToList<string>();

        //        if (listMBMComparisonResultCSG.Count() > 0)
        //        {
        //            long snapshotId = listMBMComparisonResultCSG.FirstOrDefault().SnapShotId.Value;
        //            InvoiceSnapshotId = snapshotId;

        //            foreach (var servAtt in listMBMServiceAttributeResultCSG)
        //            {
        //                if (servAtt.ServiceAttributeExternalRef == "Provisioning_Due_Date" && String.IsNullOrEmpty(servAtt.ServiceAttributeValue))
        //                {
        //                    servAtt.ServiceAttributeValue = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy");
        //                }
        //                serviceAttributesList.Add(new CSGServiceAttributes()
        //                {
        //                    AttributeId = servAtt.ServiceAttributeExternalRef,//CBTS_Identifier	Telephone_Number	11002200343
        //                    AttributeType = "CBTS_Identifier",
        //                    AttributeValue = servAtt.ServiceAttributeValue
        //                });
        //            }

        //            //foreach (var servFt in listMBMServiceFeatureResultCSG)
        //            //{
        //            //   AddLDPlanLineItems.Add(new CSGAddLDPlans()
        //            //    {
        //            //        ProductIdentifierType = "CBTS_Identifier",
        //            //        ProductIdentifierValue = servFt.ProductExternalRef,
        //            //        PricingPlanIdentifierType = "CBTS_Identifier",
        //            //        PricingPlanIdentifierValue = servFt.PricingPlanExternalRef
        //            //    });
        //            //}

        //            foreach (var servFt in listMBMServiceFeatureLDResultCSG)
        //            {
        //                serviceFeaturesList.Add(new CSGServiceFeatures()
        //                {
        //                    ProductIdentifierType = "CBTS_Identifier",
        //                    ProductIdentifierValue = servFt.ProductExternalRef,
        //                    PricingPlanIdentifierType = "CBTS_Identifier",
        //                    PricingPlanIdentifierValue = servFt.PricingPlanExternalRef
        //                });
        //            }

        //            //Add csg Bill/Sub Bill items to be added/update/deleted.
        //            foreach (var account in listCSGAccounts)
        //            {
        //                //reset list for per subscribers
        //                AddserviceFeaturesList = new List<CSGServiceFeatures>();
        //                addServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
        //                RemoveserviceFeaturesList = new List<CSGServiceFeatures>();
        //                newserviceItemList = new List<CSGNewServiceItems>();
        //                addServiceLineItemList = new List<CSGAddService>();

        //                var addServiceList = listMBMComparisonResultCSG.Where(i => i.SubscriberId == account && i.Action == 1).Select(x => x).ToList();

        //                 foreach (var item in addServiceList)
        //                 {
        //                 var dataAval= addServiceLineItemList.Where(z=>                                                                     
        //                                                            z.OfferringIdentifierValue == item.OfferExternalRef  &&                                                            
        //                                                            z.PricingPlanIdentifierValue == item.PricingPlanExternalRef &&                                                                   
        //                                                            z.ProductIdentifierValue == item.ProductExternalRef &&   
        //                                                            z.BillingEffectiveDate == item.StartDate &&   
        //                                                            z.ContractNumber == item.ContractNumber &&
        //                                                            z.ContractStartDate == item.ContractStartDate &&                                                                   
        //                                                            z.ContractEndDate == item.ContractEndDate &&                                                                 
        //                                                            z.GlDepartmentCode ==item.GlDepartmentCode &&
        //                                                            z.IndirectAgentRegion == item.IndirectAgentRegion &&
        //                                                            z.IndirectPartnerCode == item.IndirectPartnerCode
        //                                                            ).Select(q=>q).ToList();

        //                         if (dataAval.Count==0)
        //                        {
        //                            newserviceItemList = addServiceList.Where(y => y.StartDate == item.StartDate).Select(x => new CSGNewServiceItems()
        //                            {
        //                                ServiceIdentifierAttributeId = "Telephone_Number",
        //                                ServiceIdentifierAttributeType = "CBTS_Identifier",
        //                                ServiceIdentifierAttributeValue = x.MBMUniqueID,
        //                                CSGServiceFeatures = serviceFeaturesList.ToArray(),
        //                                CSGServiceAttributes = serviceAttributesList.ToArray()
        //                            }).ToList();

        //                             addServiceLineItemList.Add(new CSGAddService()
        //                                        {
        //                                            OfferringIdentifierType = "CBTS_Identifier" ,
        //                                            OfferringIdentifierValue = item.OfferExternalRef ,
        //                                            PricingPlanIdentifierType = "CBTS_Identifier" ,
        //                                            PricingPlanIdentifierValue = item.PricingPlanExternalRef ,
        //                                            ProductIdentifierType = "CBTS_Identifier" ,
        //                                            ProductIdentifierValue = item.ProductExternalRef ,
        //                                            BillingEffectiveDate = item.StartDate ,
        //                                            ContractNumber = item.ContractNumber ,
        //                                            ContractStartDate = item.ContractStartDate ,
        //                                            ContractStartDateSpecified = true ,
        //                                            ContractEndDate = item.ContractEndDate ,
        //                                            ContractEndDateSpecified = true ,
        //                                            GlDepartmentCode = item.GlDepartmentCode ,
        //                                            IndirectAgentRegion = item.IndirectAgentRegion ,
        //                                            IndirectPartnerCode = item.IndirectPartnerCode ,
        //                                            Biller = newserviceItemList.ToArray()
        //                                        });
        //                         }
        //                 }

        //               foreach (var item in listMBMComparisonResultCSG.Where(i => i.SubscriberId == account))
        //                {
        //                    int action = item.Action;

        //                    switch (action)
        //                    {                                
        //                        case (int)CSGActionTypes.AddServiceFeature:
        //                            AddserviceFeaturesList = new List<CSGServiceFeatures>();
        //                                AddserviceFeaturesList = listMBMComparisonResultCSG.Where(i => i.SubscriberId == account &&  i.Action == 2 && i.MBMUniqueID == item.MBMUniqueID).
        //                                                                                       Select(x => new CSGServiceFeatures
        //                                                                                               {
        //                                                                                                   ProductIdentifierType = "CBTS_Identifier",
        //                                                                                                   ProductIdentifierValue = x.ProductExternalRef,
        //                                                                                                   PricingPlanIdentifierType = "CBTS_Identifier",
        //                                                                                                   PricingPlanIdentifierValue = x.PricingPlanExternalRef
        //                                                                                               }).Select(x => x).ToList();

        //                                addServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
        //                                var idata = addServiceFeatureLineItemList.AsEnumerable().Where(z => z.CSGServiceIdentifiers.Any(x => x.ServiceIdentifier == item.MBMUniqueID)).ToList();
        //                                if (idata.Count == 0)
        //                                {                                            
        //                                    //if (item.ProcessFirst == "NO")
        //                                    //{
        //                                        addServiceIdentifiersListTemp.Add(new CSGServiceIdentifiers()
        //                                        {
        //                                            ServiceIdentifier = item.MBMUniqueID
        //                                        });
        //                                   // }
        //                                }
        //                                if (AddserviceFeaturesList.Count > 0 && addServiceIdentifiersListTemp.Count > 0 )
        //                                {
        //                                    //if (item.ProcessFirst == "NO")
        //                                    //{
        //                                        addServiceFeatureLineItemList.Add(new CSGAddServiceFeature()
        //                                        {
        //                                            BillingEffectiveDate = item.StartDate,
        //                                            BillingEffectiveDateIntention = 1,
        //                                            CSGServiceIdentifiers = addServiceIdentifiersListTemp.ToArray(),
        //                                            CSGServiceFeatures = AddserviceFeaturesList.ToArray()
        //                                        });
        //                                   // }
        //                                }                                                             
        //                            break;

        //                        case (int)CSGActionTypes.RemoveServiceFeature:

        //                            RemoveserviceFeaturesList = new List<CSGServiceFeatures>();
        //                            RemoveserviceFeaturesList = listMBMComparisonResultCSG.Where(i => i.SubscriberId == account && i.Action == 3 && i.MBMUniqueID == item.MBMUniqueID).
        //                                                                                Select(x => new CSGServiceFeatures
        //                                                                                        {
        //                                                                                            ProductIdentifierType = "CBTS_Identifier",
        //                                                                                            ProductIdentifierValue = x.ProductExternalRef,
        //                                                                                            PricingPlanIdentifierType = "CBTS_Identifier",
        //                                                                                            PricingPlanIdentifierValue = x.PricingPlanExternalRef
        //                                                                                        }).Select(x => x).ToList();

        //                            removeServiceFeatureIdentifiersListTemp = new List<CSGServiceIdentifiers>();
        //                            var idataAval = removeServiceFeatureLineItemList.AsEnumerable().Where(z => z.CSGServiceIdentifiers.Any(x => x.ServiceIdentifier == item.MBMUniqueID)).ToList();
        //                                if (idataAval.Count == 0)
        //                                {                                            
        //                                    //if (item.ProcessFirst == "NO")
        //                                    //{
        //                                        removeServiceFeatureIdentifiersListTemp.Add(new CSGServiceIdentifiers()
        //                                        {
        //                                            ServiceIdentifier = item.MBMUniqueID
        //                                        });
        //                                   // }
        //                                }

        //                                if (RemoveserviceFeaturesList.Count > 0 && removeServiceFeatureIdentifiersListTemp.Count > 0)
        //                                {
        //                                    //if (item.ProcessFirst == "YES")
        //                                    //{
        //                                        foreach (var servLD in serviceFeaturesList) //AddLDPlanLineItems)
        //                                        {
        //                                            RemoveserviceFeaturesList.Add(new CSGServiceFeatures()
        //                                            {
        //                                                ProductIdentifierType = "CBTS_Identifier",
        //                                                ProductIdentifierValue = servLD.ProductIdentifierValue,
        //                                                PricingPlanIdentifierType = "CBTS_Identifier",
        //                                                PricingPlanIdentifierValue = servLD.PricingPlanIdentifierValue
        //                                            });
        //                                        }

        //                                        removeServiceFeatureLineItemList.Add(new CSGRemoveServiceFeature()
        //                                            {
        //                                                BillingEffectiveDate = item.StartDate,
        //                                                BillingEffectiveDateIntention = 1,
        //                                                CSGServiceIdentifiers = removeServiceFeatureIdentifiersListTemp.ToArray(),
        //                                                CSGServiceFeatures = RemoveserviceFeaturesList.ToArray()
        //                                            });
        //                                    //}
        //                                }
        //                            break;

        //                        case (int)CSGActionTypes.RemoveService:
        //                            List<CSGServiceIdentifiers> removeServiceIdentifiersListAsPerSubscriber = listMBMComparisonResultCSG.Where(i => i.SubscriberId == account && i.Action == 4).Select(x => x.MBMUniqueID).Distinct().Select(x => new CSGServiceIdentifiers { ServiceIdentifier = x.ToString() }).ToList();
        //                            var data = removeServiceIdentifiersListTemp.Where(x => removeServiceIdentifiersListAsPerSubscriber.Any(y => y.ServiceIdentifier == x.ServiceIdentifier)).ToList();
        //                            if (data.Count == 0)
        //                            {
        //                                removeServiceIdentifiersListTemp = removeServiceIdentifiersListAsPerSubscriber;                                     
        //                                removeServiceLineItemList.Add(new CSGRemoveService()
        //                                {
        //                                    BillingEffectiveDate = DateTime.Now.AddDays(-1),
        //                                    BillingEffectiveDateIntention = 1,
        //                                    CSGServiceIdentifiers = removeServiceIdentifiersListTemp.ToArray()
        //                                });
        //                            }
        //                            break;
        //                    }
        //                }

        //                //Populate Service input object
        //                accountConfiguration.AddServiceLineItems = addServiceLineItemList.ToArray();
        //                accountConfiguration.AddServiceFeatureLineItems = addServiceFeatureLineItemList.ToArray();
        //                accountConfiguration.RemoveServiceFeatureLineItems = removeServiceFeatureLineItemList.ToArray();
        //                accountConfiguration.RemoveServiceLineItems = removeServiceLineItemList.ToArray();
        //                accountConfigurationList.Add(accountConfiguration);
        //                accountConfigRequest.LineItems = accountConfigurationList.ToArray();
        //                //accountConfigRequest.SubscriberId = Convert.ToInt32(account);
        //                accountConfigRequest.SubscriberId = Convert.ToInt32(account);
        //                accountConfigRequest.SubscriberIdSpecified = true;
        //                accountConfigRequest.SnapshotId = (long)listMBMComparisonResultCSG.FirstOrDefault().SnapShotId;
        //                accountConfigRequest.SnapshotIdSpecified = true;
        //                accountConfigRequest.SubmittedUser = submittedBy;
        //                //CBTService Call
        //                CBTSService.CBTSService cbtsService = new CBTSService.CBTSService();
        //                cbtsService.ProcessMBMServiceFeatureOrderSnapshot(accountConfigRequest);
        //                //Update Processed Flag.
        //                crmAccount.UpdateMBMComparisonResult(invoiceId);
        //                //Clear List items.
        //                addServiceLineItemList.Clear();
        //                addServiceFeatureLineItemList.Clear();
        //                removeServiceFeatureLineItemList.Clear();
        //                removeServiceLineItemList.Clear();
        //                accountConfigurationList.Clear();
        //            }

        //            //Logic to enable next button in the work-flow.
        //            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.SyncCRM);

        //            ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
        //            objStatus.InvoiceNumber = invoiceNumber;
        //            objStatus.CompareToCRM = true;
        //            objStatus.ViewChange = true;
        //            objStatus.ApproveChange = true;
        //            objStatus.SyncCRM = true;
        //            objStatus.ImportCRMData = false;
        //            objStatus.ProcessInvoice = false;
        //            objStatus.ExportInvoiceFile = false;

        //            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
        //            ProcessWorkflowStatusBL processWorkflowStatusBL = new ProcessWorkflowStatusBL(strConnectionString);
        //            processWorkflowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(objStatus);

        //            InvoiceBL invoiceBl = new InvoiceBL(strConnectionString);
        //            MBMAutomateStatus mbmAutomateStatus = invoiceBl.GetMBMAutomateStatus(invoiceId);
        //            if (mbmAutomateStatus.InvoiceId != null)
        //            {
        //                BillingEngine billingEngine = new BillingEngine(strConnectionString);
        //                billingEngine.ProcessWorkFlowStatusBL.UpdateAutomationWorkFlowStatusByInvoiceId(invoiceId, true, "SyncCRM");
        //            }

        //            syncCSGStatus = "Synced";
        //        }
        //        else
        //        {
        //            ProcessWorkFlowStatus objStatus = new ProcessWorkFlowStatus();
        //            objStatus.InvoiceNumber = invoiceNumber;
        //            objStatus.CompareToCRM = true;
        //            objStatus.ViewChange = true;
        //            objStatus.ApproveChange = true;
        //            objStatus.SyncCRM = true;
        //            objStatus.ImportCRMData = false;
        //            objStatus.ProcessInvoice = false;
        //            objStatus.ExportInvoiceFile = false;

        //            //UpdateProcessWorkFlowStatus(ProcessWorkFlowButtons.CompareToCRM);
        //            ProcessWorkflowStatusBL processWorkflowStatusBL = new ProcessWorkflowStatusBL(strConnectionString);
        //            processWorkflowStatusBL.UpdateProcessWorkFlowStatusByInvoiceNumber(objStatus);

        //            syncCSGStatus = "NoRecords";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return syncCSGStatus;
        //}



        public string SyncCSG(int invoiceId, string submittedBy, string invoiceNumber)
        {
            string syncCSGStatus = string.Empty;

            try
            {
                // btnSyncCRM.Enabled = false;

                CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);
                List<MBMComparisonResultCSG> listMBMComparisonResultCSG = new List<MBMComparisonResultCSG>();

                listMBMComparisonResultCSG = crmAccount.GetUnprocessedMBMComparisonResultCSG(invoiceId);

                if (listMBMComparisonResultCSG.Count() > 0)
                {
                    long snapshotId = listMBMComparisonResultCSG.FirstOrDefault().SnapShotId.Value;
                    InvoiceSnapshotId = snapshotId;

                    //TODO: Add if condition to check if List listMBMComparisonResultCSG has any record with ProcessFirst="YES" and call following method with just the records with ProcessFirst="YES"
                    var ListsProcessFirstYes = listMBMComparisonResultCSG.Where(x => x.ProcessFirst == "YES").Select(x => x).ToList();
                    if (ListsProcessFirstYes.Count > 0)
                    {
                        SyncCSGAccounts(invoiceId, submittedBy, invoiceNumber, "YES", ListsProcessFirstYes);
                    }

                    //TODO: Add if condition to check if List listMBMComparisonResultCSG has any record with ProcessFirst="No" and call following method with just the records with ProcessFirst="YES"
                    var ListsProcessFirstNo = listMBMComparisonResultCSG.Where(x => x.ProcessFirst == "NO").Select(x => x).ToList();
                    if (ListsProcessFirstNo.Count > 0)
                    {
                        SyncCSGAccounts(invoiceId, submittedBy, invoiceNumber, "NO", ListsProcessFirstNo);
                    }
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
                        BillingEngine billingEngine = new BillingEngine(strConnectionString);
                        billingEngine.ProcessWorkFlowStatusBL.UpdateAutomationWorkFlowStatusByInvoiceId(invoiceId, true, "SyncCRM");
                    }

                    syncCSGStatus = "Synced";
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

                    syncCSGStatus = "NoRecords";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return syncCSGStatus;
        }


        public void SyncCSGAccounts(int invoiceId, string submittedBy, string invoiceNumber, string sProcessFirst, List<MBMComparisonResultCSG> mbmComparisonResultCSGList)
        {
            try
            {
                long snapshotId = mbmComparisonResultCSGList.FirstOrDefault().SnapShotId.Value;
                InvoiceSnapshotId = snapshotId;

                CRMAccountBL crmAccount = new CRMAccountBL(strConnectionString);

                //Pull the list of Service Attributes from database table "[dbo].[CSGServiceAttributeList]" like Telephone_Number, Type, International_Block, User_Id, etc...
                List<MBMServiceAttributes> mbmServiceAttributesList = new List<MBMServiceAttributes>();
                List<CSGServiceAttributes> csgServiceAttributesList = new List<CSGServiceAttributes>();

                mbmServiceAttributesList = crmAccount.GetAllCSGServiceAttributes();

                foreach (var servAtt in mbmServiceAttributesList)
                {
                    //if (servAtt.ServiceAttributeExternalRef == "Provisioning_Due_Date" && String.IsNullOrEmpty(servAtt.ServiceAttributeValue))
                    //{
                    //    servAtt.ServiceAttributeValue = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("MM/dd/yyyy");
                    //} Bug31930

                    csgServiceAttributesList.Add(new CSGServiceAttributes()
                    {
                        AttributeId = servAtt.ServiceAttributeExternalRef,  //CBTS_Identifier       Telephone_Number     11002200343
                        AttributeType = "CBTS_Identifier",
                        AttributeValue = servAtt.ServiceAttributeValue
                    });
                }

                //Pull the list of LD Service Features from database table "[dbo].[CSGLDUsagePlan]" like OutBound_Domestic_USD, InBound_Domestic_USD, etc...
                List<MBMServiceFeatures> mbmServiceFeaturesList = new List<MBMServiceFeatures>();


                mbmServiceFeaturesList = crmAccount.GetAllCSGServiceFeatures(invoiceId);

                //TODO: Why we are adding the LD Service Features Twice???
                //List<CSGServiceFeatures> csgServiceFeaturesList = new List<CSGServiceFeatures>();
                List<CSGAddLDPlans> AddLDPlanLineItemsList = new List<CSGAddLDPlans>();
                foreach (var servFt in mbmServiceFeaturesList)
                {
                    AddLDPlanLineItemsList.Add(new CSGAddLDPlans()
                    {
                        ProductIdentifierType = "CBTS_Identifier",
                        ProductIdentifierValue = servFt.ProductExternalRef,
                        PricingPlanIdentifierType = "CBTS_Identifier",
                        PricingPlanIdentifierValue = servFt.PricingPlanExternalRef
                    });
                }

                //Add csg Bill/Sub Bill items to be added/update/deleted.
                List<string> csgAccountsList = new List<string>();
                csgAccountsList = mbmComparisonResultCSGList.Where(i => i.ProcessFirst == sProcessFirst).AsEnumerable().Select(x => x.SubscriberId).Distinct().ToList<string>();

                foreach (var account in csgAccountsList)
                {
                    List<CSGNewServiceItems> csgNewServiceItemsList = new List<CSGNewServiceItems>();
                    //Add Service (Service Identifiers with related Service Attributes and LD Usage Product/Pricing Plans)
                    List<CSGAddService> csgAddServiceList = new List<CSGAddService>();
                    var addServiceList = mbmComparisonResultCSGList.Where(i => i.SubscriberId == account && i.ProcessFirst == sProcessFirst && i.Action == 1).Select(x => x).ToList();

                    foreach (var item in addServiceList)
                    {
                        var dataAval = csgAddServiceList.Where(z =>
                                                                   z.OfferringIdentifierValue == item.OfferExternalRef &&
                                                                   z.PricingPlanIdentifierValue == item.PricingPlanExternalRef &&
                                                                   z.ProductIdentifierValue == item.ProductExternalRef &&
                                                                   z.BillingEffectiveDate == item.StartDate &&
                                                                   z.ContractNumber == item.ContractNumber &&
                                                                   z.ContractStartDate == item.ContractStartDate &&
                                                                   z.ContractEndDate == item.ContractEndDate &&
                                                                   z.GlDepartmentCode == item.GlDepartmentCode &&
                                                                   z.IndirectAgentRegion == item.IndirectAgentRegion &&
                                                                   z.IndirectPartnerCode == item.IndirectPartnerCode
                                                                   ).Select(q => q).ToList();

                        if (dataAval.Count == 0)
                        {
                            csgNewServiceItemsList = addServiceList.Where(y => y.StartDate == item.StartDate).Select(x => new CSGNewServiceItems()
                            {
                                ServiceIdentifierAttributeId = "Telephone_Number",
                                ServiceIdentifierAttributeType = "CBTS_Identifier",
                                ServiceIdentifierAttributeValue = x.MBMUniqueID,
                               //CSGServiceFeatures/ = csgServiceFeaturesList.ToArray(),
                               // CSGServiceAttributes = csgServiceAttributesList.ToArray()
                            }).ToList();

                            csgAddServiceList.Add(new CSGAddService()
                            {
                                OfferringIdentifierType = "CBTS_Identifier",
                                OfferringIdentifierValue = item.OfferExternalRef,
                                PricingPlanIdentifierType = "CBTS_Identifier",
                                PricingPlanIdentifierValue = item.PricingPlanExternalRef,
                                ProductIdentifierType = "CBTS_Identifier",
                                ProductIdentifierValue = item.ProductExternalRef,
                                BillingEffectiveDate = item.StartDate,
                                ContractNumber = item.ContractNumber,
                                ContractStartDate = item.ContractStartDate,
                                ContractStartDateSpecified = true,
                                ContractEndDate = item.ContractEndDate,
                                ContractEndDateSpecified = true,
                                GlDepartmentCode = item.GlDepartmentCode,
                                IndirectAgentRegion = item.IndirectAgentRegion,
                                IndirectPartnerCode = item.IndirectPartnerCode,
                                Biller = csgNewServiceItemsList.ToArray()
                            });
                        }
                    }


                    //Add Service Features
                    List<CSGServiceFeatures> AddserviceFeaturesList = new List<CSGServiceFeatures>();
                    List<CSGServiceIdentifiers> addServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
                    List<CSGAddServiceFeature> addServiceFeatureLineItemList = new List<CSGAddServiceFeature>();

                    //Remove Service Features
                    List<CSGRemoveServiceFeature> removeServiceFeatureLineItemList = new List<CSGRemoveServiceFeature>();
                    List<CSGServiceIdentifiers> removeServiceFeatureIdentifiersListTemp = new List<CSGServiceIdentifiers>();
                    List<CSGServiceFeatures> RemoveserviceFeaturesList = new List<CSGServiceFeatures>();

                    //Remove Service
                    List<CSGServiceIdentifiers> removeServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
                    List<CSGRemoveService> removeServiceLineItemList = new List<CSGRemoveService>();

                    foreach (var item in mbmComparisonResultCSGList.Where(i => i.SubscriberId == account && i.ProcessFirst == sProcessFirst))
                    {
                        int action = item.Action;

                        switch (action)
                        {
                            case (int)CSGActionTypes.AddServiceFeature:

                                addServiceIdentifiersListTemp = new List<CSGServiceIdentifiers>();
                                var MBMUniqueIDnotExists = addServiceFeatureLineItemList.AsEnumerable()
                                    .Where(x => x.BillingEffectiveDate == item.StartDate)
                                    .Where(z => z.CSGServiceIdentifiers
                                        .Any(x => x.ServiceIdentifier == item.MBMUniqueID)).ToList();
                                if (MBMUniqueIDnotExists.Count == 0)
                                {
                                    addServiceIdentifiersListTemp.Add(new CSGServiceIdentifiers()
                                    {
                                        ServiceIdentifier = item.MBMUniqueID//123 basic may 1st 
                                        //cisco 4481 may 5 
                                    });
                                }
                                AddserviceFeaturesList = new List<CSGServiceFeatures>();
                                var notExists = addServiceFeatureLineItemList.AsEnumerable()
                                             .Where(x => x.BillingEffectiveDate == item.StartDate)
                                             .Where(z => z.CSGServiceIdentifiers
                                             .Any(x => x.ServiceIdentifier == item.MBMUniqueID))
                                             .Where(q => q.CSGServiceFeatures
                                             .Any(s => s.PricingPlanIdentifierValue == item.PricingPlanExternalRef)).ToList();

                                if (notExists.Count == 0)
                                {
                                    AddserviceFeaturesList.Add(new CSGServiceFeatures()
                                    {
                                        ProductIdentifierType = "CBTS_Identifier",
                                        ProductIdentifierValue = item.ProductExternalRef,
                                        PricingPlanIdentifierType = "CBTS_Identifier",
                                        PricingPlanIdentifierValue = item.PricingPlanExternalRef
                                    });
                                }
                                var iExists = addServiceFeatureLineItemList.AsEnumerable()
                                         .Where(x => x.BillingEffectiveDate == item.StartDate)
                                         .Where(z => z.CSGServiceIdentifiers
                                         .Any(x => x.ServiceIdentifier == item.MBMUniqueID))
                                         .Where(q => q.CSGServiceFeatures
                                         .Any(s => s.PricingPlanIdentifierValue != item.PricingPlanExternalRef)).ToList();

                                if (iExists.Count > 0) //&& AddserviceFeaturesList.Where(x => x.PricingPlanIdentifierValue == item.PricingPlanExternalRef).Select(x => x).ToList().Count == 0)
                                {
                                    for (int i = 0; i < addServiceFeatureLineItemList.Count; i++)
                                    {
                                        var result = addServiceFeatureLineItemList[i].CSGServiceIdentifiers.Any(x => x.ServiceIdentifier == item.MBMUniqueID);
                                        if (result)
                                        {
                                            // var index = addServiceFeatureLineItemList[i].CSGServiceFeatures.Count();
                                            AddserviceFeaturesList = new List<CSGServiceFeatures>();
                                            var CSGServiceFeatureslistTemp = addServiceFeatureLineItemList[i].CSGServiceFeatures.ToList();
                                            CSGServiceFeatureslistTemp.Add(new CSGServiceFeatures()
                                            {
                                                ProductIdentifierType = "CBTS_Identifier",
                                                ProductIdentifierValue = item.ProductExternalRef,
                                                PricingPlanIdentifierType = "CBTS_Identifier",
                                                PricingPlanIdentifierValue = item.PricingPlanExternalRef
                                            });
                                            addServiceFeatureLineItemList[i].CSGServiceFeatures = CSGServiceFeatureslistTemp.ToArray();
                                        }
                                    }
                                }
                                //add logic based on start date 



                                //AddserviceFeaturesList = new List<CSGServiceFeatures>();
                                //AddserviceFeaturesList.Add(new CSGServiceFeatures()
                                //{
                                //    ProductIdentifierType = "CBTS_Identifier",
                                //    ProductIdentifierValue = item.ProductExternalRef,
                                //    PricingPlanIdentifierType = "CBTS_Identifier",
                                //    PricingPlanIdentifierValue = item.PricingPlanExternalRef
                                //});

                                if (AddserviceFeaturesList.Count > 0 && addServiceIdentifiersListTemp.Count > 0)
                                {
                                    addServiceFeatureLineItemList.Add(new CSGAddServiceFeature()
                                    {
                                        BillingEffectiveDate = item.StartDate,
                                        BillingEffectiveDateIntention = 1,
                                        CSGServiceIdentifiers = addServiceIdentifiersListTemp.ToArray(),
                                        CSGServiceFeatures = AddserviceFeaturesList.ToArray()
                                    });
                                }
                                break;

                            case (int)CSGActionTypes.RemoveServiceFeature:

                                removeServiceFeatureIdentifiersListTemp = new List<CSGServiceIdentifiers>();
                                var idataAval = removeServiceFeatureLineItemList.AsEnumerable().Where(x => x.BillingEffectiveDate == item.EndDate.GetValueOrDefault()).Where(z => z.CSGServiceIdentifiers.Any(x => x.ServiceIdentifier == item.MBMUniqueID)).ToList();
                                if (idataAval.Count == 0)
                                {
                                    removeServiceFeatureIdentifiersListTemp.Add(new CSGServiceIdentifiers()
                                    {
                                        ServiceIdentifier = item.MBMUniqueID
                                    });
                                }

                                RemoveserviceFeaturesList = new List<CSGServiceFeatures>();
                                var notExistsforRemoveSF = removeServiceFeatureLineItemList.AsEnumerable()
                                             .Where(x => x.BillingEffectiveDate == item.EndDate)
                                             .Where(z => z.CSGServiceIdentifiers
                                             .Any(x => x.ServiceIdentifier == item.MBMUniqueID))
                                             .Where(q => q.CSGServiceFeatures
                                             .Any(s => s.PricingPlanIdentifierValue == item.PricingPlanExternalRef)).ToList();


                                //same logic as as start date time need to check end date time

                                //RemoveserviceFeaturesList.Add(new CSGServiceFeatures()
                                //{
                                //    ProductIdentifierType = "CBTS_Identifier",
                                //    ProductIdentifierValue = item.ProductExternalRef,
                                //    PricingPlanIdentifierType = "CBTS_Identifier",
                                //    PricingPlanIdentifierValue = item.PricingPlanExternalRef
                                //});

                                if (notExistsforRemoveSF.Count == 0)
                                {
                                    RemoveserviceFeaturesList.Add(new CSGServiceFeatures()
                                    {
                                        ProductIdentifierType = "CBTS_Identifier",
                                        ProductIdentifierValue = item.ProductExternalRef,
                                        PricingPlanIdentifierType = "CBTS_Identifier",
                                        PricingPlanIdentifierValue = item.PricingPlanExternalRef
                                    });
                                }
                                var iExistsForRmoveSF = removeServiceFeatureLineItemList.AsEnumerable()
                                             .Where(x => x.BillingEffectiveDate == item.EndDate)
                                             .Where(z => z.CSGServiceIdentifiers
                                             .Any(x => x.ServiceIdentifier == item.MBMUniqueID))
                                             .Where(q => q.CSGServiceFeatures
                                             .Any(s => s.PricingPlanIdentifierValue != item.PricingPlanExternalRef)).ToList();

                                if (iExistsForRmoveSF.Count > 0) //&& AddserviceFeaturesList.Where(x => x.PricingPlanIdentifierValue == item.PricingPlanExternalRef).Select(x => x).ToList().Count == 0)
                                {
                                    for (int i = 0; i < removeServiceFeatureLineItemList.Count; i++)
                                    {
                                        var result = removeServiceFeatureLineItemList[i].CSGServiceIdentifiers.Any(x => x.ServiceIdentifier == item.MBMUniqueID);
                                        if (result)
                                        {
                                            // var index = addServiceFeatureLineItemList[i].CSGServiceFeatures.Count();
                                            RemoveserviceFeaturesList = new List<CSGServiceFeatures>();
                                            var CSGServiceFeatureslistTemp = removeServiceFeatureLineItemList[i].CSGServiceFeatures.ToList();
                                            CSGServiceFeatureslistTemp.Add(new CSGServiceFeatures()
                                            {
                                                ProductIdentifierType = "CBTS_Identifier",
                                                ProductIdentifierValue = item.ProductExternalRef,
                                                PricingPlanIdentifierType = "CBTS_Identifier",
                                                PricingPlanIdentifierValue = item.PricingPlanExternalRef
                                            });
                                            removeServiceFeatureLineItemList[i].CSGServiceFeatures = CSGServiceFeatureslistTemp.ToArray();
                                        }
                                    }
                                }


                                if (RemoveserviceFeaturesList.Count > 0 && removeServiceFeatureIdentifiersListTemp.Count > 0)
                                {
                                    removeServiceFeatureLineItemList.Add(new CSGRemoveServiceFeature()
                                    {
                                        BillingEffectiveDate = item.EndDate.GetValueOrDefault(),
                                        BillingEffectiveDateIntention = 1,
                                        CSGServiceIdentifiers = removeServiceFeatureIdentifiersListTemp.ToArray(),
                                        CSGServiceFeatures = RemoveserviceFeaturesList.ToArray()
                                    });
                                }
                                break;

                            case (int)CSGActionTypes.RemoveService:

                                List<CSGServiceIdentifiers> removeServiceIdentifiersListAsPerSubscriber = mbmComparisonResultCSGList.Where(i => i.SubscriberId == account && i.Action == 4).Select(x => x.MBMUniqueID).Distinct().Select(x => new CSGServiceIdentifiers { ServiceIdentifier = x.ToString() }).ToList();

                                var data = removeServiceIdentifiersListTemp.Where(x => removeServiceIdentifiersListAsPerSubscriber.Any(y => y.ServiceIdentifier == x.ServiceIdentifier)).ToList();

                                if (data.Count == 0)
                                {
                                    removeServiceIdentifiersListTemp = removeServiceIdentifiersListAsPerSubscriber;

                                    removeServiceLineItemList.Add(new CSGRemoveService()
                                    {
                                        //BillingEffectiveDate = DateTime.Now.AddDays(-1),
                                        BillingEffectiveDate = item.EndDate.GetValueOrDefault(),
                                        BillingEffectiveDateIntention = 1,
                                        CSGServiceIdentifiers = removeServiceIdentifiersListTemp.ToArray()
                                    });
                                }
                                break;
                        }
                    }

                    MBMServiceFeatureOrderSnapshotRequest accountConfigRequest = new MBMServiceFeatureOrderSnapshotRequest();
                    CSGAccountConfiguration accountConfiguration = new CSGAccountConfiguration();
                    List<CSGAccountConfiguration> accountConfigurationList = new List<CSGAccountConfiguration>();

                    //Populate Service input object
                    accountConfiguration.AddServiceLineItems = csgAddServiceList.ToArray();
                    accountConfiguration.AddLDPlanLineItems = AddLDPlanLineItemsList.ToArray();
                    accountConfiguration.CSGServiceAttributes = csgServiceAttributesList.ToArray();
                    accountConfiguration.AddServiceFeatureLineItems = addServiceFeatureLineItemList.ToArray();
                    accountConfiguration.RemoveServiceFeatureLineItems = removeServiceFeatureLineItemList.ToArray();
                    accountConfiguration.RemoveServiceLineItems = removeServiceLineItemList.ToArray();

                    accountConfigurationList.Add(accountConfiguration);
                    accountConfigRequest.LineItems = accountConfigurationList.ToArray();

                    accountConfigRequest.SubscriberId = Convert.ToInt32(account);
                    accountConfigRequest.SubscriberIdSpecified = true;
                    accountConfigRequest.SnapshotId = (long)mbmComparisonResultCSGList.FirstOrDefault().SnapShotId;
                    accountConfigRequest.SnapshotIdSpecified = true;
                    accountConfigRequest.SubmittedUser = submittedBy;

                    //CBTService Call
                    CBTSService.CBTSService cbtsService = new CBTSService.CBTSService();
                    cbtsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["QuorumCBTSServiceCallTimeout"]); //5400000;
                    cbtsService.ProcessMBMServiceFeatureOrderSnapshot(accountConfigRequest);

                    //Update Processed Flag.
                    var loadId = mbmComparisonResultCSGList.Where(i => i.SubscriberId == account && i.ProcessFirst == sProcessFirst).Select(x => x.LoadId).FirstOrDefault();
                    crmAccount.UpdateMBMComparisonResultCSG(invoiceId, snapshotId, loadId, account, sProcessFirst == "NO" ? false : true); //TODO: Replace with a call to stored procedure '[dbo].[Update_MBM_ComparisonResult_CSG]'
                    //Clear List items.

                    csgAddServiceList.Clear();
                    addServiceFeatureLineItemList.Clear();
                    removeServiceFeatureLineItemList.Clear();
                    removeServiceLineItemList.Clear();
                    accountConfigurationList.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
