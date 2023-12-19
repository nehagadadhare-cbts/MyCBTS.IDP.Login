using Automate.DataAccess;
using Automate.DataAccess.edmx;
using Automate.DataAccess.Entities;
using Automate.DataAccess.Log;
using AutomateMBM.BillingEngine;
using AutomateMBM.BusinessLogic;
using AutomateMBM.Entities;
using System;
using System.Collections.Generic;

namespace AutomateMBM
{
    class Program
    {
        static void Main(string[] args)
        {            
            Program program = new Program();
            try
            {
                //Selects customers for Pre or Post Billing  
                Console.WriteLine("Inserting data in MBMAutomate");
                program.InsertInvoiceInAutomation();

                //////creates invoice,attaches snapshot and Compares to CRM
                Console.WriteLine("called SP to create invoice");
               program.InvoiceCreateProcess();

                ////View changes and Sync CRM
                Console.WriteLine("Pre billing activities");
                program.PreBillingActivities();
                
                //Selects invoices that are eligible for post billing activities and performs import to CRM and Process Invoice
                Console.WriteLine("Selecting invoices for Post Billing");
                program.PostBillingStart();

                //Exports the invoices
                Console.WriteLine("Completing Postbilling activities");
               // program.PostBillingActivities();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
            
        }

        private void InsertInvoiceInAutomation()
        {
            try
            {
                AutomateDAL dal = new AutomateDAL();
                dal.InsertInvoiceInMBMAutomate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }
        }
        
        private void InvoiceCreateProcess()
        { 
            try
            {
                AutomateDAL dal = new AutomateDAL();
                //dal.InsertCreateProcess();CBE_9719
                dal.InsertCreateProcessById();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
            }            
        }

        private void PreBillingActivities()
        {
            AutomateDAL dal = new AutomateDAL();
            List<MBMAutomateStatu> preBillingInvoices = new List<MBMAutomateStatu>();
            preBillingInvoices = dal.GetInvoicesForPreBilling();           
            if (preBillingInvoices.Count > 0)
            {
                
                foreach (MBMAutomateStatu invoiceToProcess in preBillingInvoices)
                {
                    try
                    {
                        Invoice invoice = dal.GetInvoiceByID(invoiceToProcess.InvoiceId.GetValueOrDefault());
                        string customerName = dal.GetCustomerName(invoice.TypeOfBill);
                        ProcessWorkflow workFlow = new ProcessWorkflow();                                                   

                        if (invoiceToProcess.IsComparedToCRM && !invoiceToProcess.IsViwedChanged)
                        {
                            Console.WriteLine("View changes for invoice {0}", invoice.InvoiceNumber);                            
                            bool isViewed=workFlow.ViewCRMChanges(invoice,customerName);
                            if(isViewed)
                                invoiceToProcess.IsViwedChanged = true;
                        }

                        //Approval will happen through API or from MBM Website
                        //if (invoiceToProcess.IsComparedToCRM && invoiceToProcess.IsViwedChanged && invoiceToProcess.IsApprovedByMail && !invoiceToProcess.IsApprovedChange)
                        //{
                        //    Console.WriteLine("Approving changes for invoice {0}", invoice.InvoiceNumber);
                        //    bool isApproved=workFlow.ApproveChanges(invoice,customerName,invoice.BillingMonth,invoice.BillingYear);
                        //    if(isApproved)
                        //        invoiceToProcess.IsApprovedChange = true;
                        //}

                        if (invoiceToProcess.IsComparedToCRM && invoiceToProcess.IsViwedChanged && !invoiceToProcess.IsSyncedCRM)
                        {
                            Console.WriteLine("Sync CRM for invoice {0}", invoice.InvoiceNumber);
                            workFlow.SyncChanges(invoice, customerName, invoice.BillingMonth, invoice.BillingYear);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        ExceptionLogging.SendErrorTomail(ex);    
                    }
                }
            }
        }

        public void PostBillingStart() 
        {
            try
            {
                AutomateDAL dal = new AutomateDAL();
                List<PostBillingInMBMAutomate_GetInvoiceRecords_Result> postBillingInvoiceRecords = dal.GetPostBillingInvoiceRecords();

                foreach (var postBillingInvoiceRecord in postBillingInvoiceRecords)
                {
                    dal.PostBillingStart(postBillingInvoiceRecord);
                    Invoice invoice = dal.GetInvoiceByID(postBillingInvoiceRecord.InvoiceId.GetValueOrDefault());
                    string customerName = dal.GetCustomerName(invoice.TypeOfBill);
                    MBMAutomateStatu invoiceRecord = dal.GetMBMAutomateStatus(postBillingInvoiceRecord.InvoiceId);

                    if (invoiceRecord.IsImportCRM && invoiceRecord.IsProcessInvoice && !invoiceRecord.IsExportInvoice)
                    {
                        Console.WriteLine("Exporting Files for invoice {0}", invoice.InvoiceNumber);
                        ProcessWorkflow workFlow = new ProcessWorkflow();
                        workFlow.ExportInvoice(invoice, customerName, invoice.BillingMonth, invoice.BillingYear);
                    }  

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ExceptionLogging.SendErrorTomail(ex);
                Logging logger = new Logging();

                logger.LogExceptionToDatabase(new ApplicationLogEntity()
                {
                    //UserName = UserName,
                    LogType = ApplicationLogType.SystemRaised,
                    ExceptionDateTime = DateTime.Now,
                    CodeLocation = "PostBillingStart()",
                    Comment = "System Exception",
                    Exception = ex,
                });
                
            }
        }

        private void PostBillingActivities()
        {
            AutomateDAL dal = new AutomateDAL();
            List<MBMAutomateStatu> postBillingInvoices = new List<MBMAutomateStatu>();
            postBillingInvoices = dal.GetInvoicesForPostBilling();
            if (postBillingInvoices.Count > 0)
            {
                try
                {                    
                    foreach (MBMAutomateStatu postBillingProcess in postBillingInvoices)
                    {
                        Invoice invoice = dal.GetInvoiceByID(postBillingProcess.InvoiceId.GetValueOrDefault());
                        string customerName = dal.GetCustomerName(invoice.TypeOfBill);
                     
                        if (postBillingProcess.IsImportCRM && postBillingProcess.IsProcessInvoice && !postBillingProcess.IsExportInvoice)
                        {
                            Console.WriteLine("Exporting Files for invoice {0}", invoice.InvoiceNumber);
                            ProcessWorkflow workFlow = new ProcessWorkflow();
                            workFlow.ExportInvoice(invoice,customerName,invoice.BillingMonth,invoice.BillingYear);                                
                        }                                                    
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    ExceptionLogging.SendErrorTomail(ex);
                    Logging logger = new Logging();

                    logger.LogExceptionToDatabase(new ApplicationLogEntity()
                    {
                        //UserName = UserName,
                        LogType = ApplicationLogType.SystemRaised,
                        ExceptionDateTime = DateTime.Now,
                        CodeLocation = "PostBillingActivities()",
                        Comment = "System Exception",
                        Exception = ex,
                    });
                }
            }
        }
    }
}
