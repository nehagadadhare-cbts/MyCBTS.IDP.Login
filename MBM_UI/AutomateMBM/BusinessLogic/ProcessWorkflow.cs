using Automate.DataAccess;
using Automate.DataAccess.Entities;
using Automate.DataAccess.Log;
using AutomateMBM.BillingEngine;
using AutomateMBM.Entities;
using MBM.BillingEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace AutomateMBM.BusinessLogic
{
    class ProcessWorkflow
    {        
        internal bool ViewCRMChanges(Invoice invoice,string customerName)
        {
            string filePath = string.Empty;
            string fileNameForInsert = string.Empty;
            string fileNameForCancel = string.Empty;
            bool isViewed = false;

            try
            {
                ViewChangeCount count = new ViewChangeCount();                
                filePath = ConfigurationManager.AppSettings["ComaparisonFilePath"].ToString();

                fileNameForInsert = ExportComparisonDataToExcel("MBMComparisonInserts", invoice.ID, filePath, invoice.InvoiceNumber,ref count);
                fileNameForCancel = ExportComparisonDataToExcel("MBMComparisonCancels", invoice.ID, filePath, invoice.InvoiceNumber,ref count);
                Console.WriteLine(fileNameForInsert, fileNameForCancel);
                AutomateDAL dal = new AutomateDAL();

                if (!string.IsNullOrWhiteSpace(fileNameForInsert) || !string.IsNullOrWhiteSpace(fileNameForCancel))
                {
                    string insertFileLocation = string.Empty;
                    string cancelFileLocation = string.Empty;
                    insertFileLocation = (string.IsNullOrWhiteSpace(fileNameForInsert) ? null : filePath + fileNameForInsert);
                    cancelFileLocation = (string.IsNullOrWhiteSpace(fileNameForCancel) ? null : filePath + fileNameForCancel);

                    Guid guid = Guid.NewGuid();
                    dal.InsertTokenHistory(guid, invoice.InvoiceNumber);
                    SendMails.ComparisonResultMailSend(insertFileLocation, cancelFileLocation, guid, invoice.InvoiceNumber, invoice.TypeOfBill, invoice.ID, ref count,customerName,invoice.BillingMonth,invoice.BillingYear);

                    dal.UpdateMBMAutomateStatus(invoice.ID, "ViewCRMChanges");
                    //dal.UpdateMBMStatus(invoice.InvoiceNumber, "ViewCRMChanges");
                    isViewed = true;
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
                    CodeLocation = "ViewCRMChanges()",
                    Comment = "System Exception",
                    Exception = ex,
                });
                
                
            }
            return isViewed;
        }

        //internal bool ApproveChanges(Invoice invoice,string customerName,int month,int year)
        //{
        //    List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();
        //    AutomateDAL dal = new AutomateDAL();
        //    bool isApproved = false;
            
        //    try
        //    {
        //        listMBMComparisonResult = dal.GetUnprocessedMBMComparisonResult(invoice.ID);
        //        if (listMBMComparisonResult.Count > 0)
        //        {
        //            dal.UpdateMBMAutomateStatus(invoice.ID, "ApproveChanges");
        //            dal.UpdateMBMStatus(invoice.InvoiceNumber, "ApproveChanges");

        //            //string subject = string.Format("{0} ({1}/{2}) - Approve Changes", customerName, month, year);
        //            //SendMails.Emails(subject, string.Format("Changes has been successfully approved for invoice {0}.", invoice.InvoiceNumber));
        //            isApproved = true;
        //        }
        //        else
        //        {
        //            string subject = string.Format("{0} ({1}/{2}) - Approve Changes", customerName, month, year);
        //            SendMails.Emails(subject, string.Format("There is no comparison file data to approve for invoice {0} ({1}/{2}).", invoice.InvoiceNumber,month,year));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        ExceptionLogging.SendErrorTomail(ex);
        //    }
        //    return isApproved;
        //}

        internal void SyncChanges(Invoice invoice,string customerName,int month,int year)
        {
            string status = string.Empty;
            
            try
            {
                AutomateDAL dal = new AutomateDAL();
                var BillingSystem = dal.GetMBMBillingSystem(invoice.ID);
                string subject=string.Empty;
                if (BillingSystem == "CRM")
                {
                    SyncCRMLogic sync = new SyncCRMLogic();
                    status = sync.SyncCRM(invoice.ID, invoice.CreatedBy, invoice.InvoiceNumber);
                    subject = string.Format("{0} ({1}/{2}) - Sync CRM", customerName, month, year);
                    dal.UpdateMBMAutomateStatus(invoice.ID, "SyncCRM");

                }
                else
                {
                    SyncCSGLogic syncCSG = new SyncCSGLogic();
                    status = syncCSG.SyncCSG(invoice.ID, invoice.CreatedBy, invoice.InvoiceNumber);
                    subject = string.Format("{0} ({1}/{2}) - Sync CSG", customerName, month, year);
                    dal.UpdateMBMAutomateStatus(invoice.ID, "SyncCSG");
                }
                
                if (status == "Synced")
                {                   
                    SendMails.Emails(subject, string.Format("Biller has been successfully synced for invoice number {0} ({1}/{2}).", invoice.InvoiceNumber, month, year));
                }
                else
                {
                    SendMails.Emails(subject, string.Format("Sync biller FAILED for invoice number {0} ({1}/{2}).", invoice.InvoiceNumber, month, year));
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
                    CodeLocation = "SyncChanges()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }            
        }        

        internal void ExportInvoice(Invoice invoice,string customerName,int month,int year)
        {
            try
            {
                ExportInvoiceLogic export = new ExportInvoiceLogic();
                bool exportStatus = export.ExportInvoice(invoice.ID, invoice.InvoiceNumber, invoice.TypeOfBill, invoice.CreatedBy,false);
                string subject = string.Format("{0} ({1}/{2}) - Export Invoice", customerName, month, year);

                if (exportStatus)
                {
                    AutomateDAL dal = new AutomateDAL();
                    dal.UpdateMBMAutomateStatus(invoice.ID, "ExportInvoice");
                    dal.UpdateMBMStatus(invoice.InvoiceNumber, "ExportInvoice");
                    
                    string body = "File exported to FTP sucessfully for Invoice "+ invoice.InvoiceNumber+" ("+month+"/"+year+")" ;
                    SendMails.Emails(subject, body);                    
                }
                else
                {
                    SendMails.Emails(subject, string.Format("Export Invoice FAILED for {0}", invoice.InvoiceNumber));
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
                    CodeLocation = "ExportInvoice()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }
        }       

        public string ExportComparisonDataToExcel(string strInsertsOrCancels, int invoiceId, string filePath, string invoiceNumber, ref ViewChangeCount counts)
        {
            string fileName = string.Empty;
            try
            {
                AutomateDAL dal = new AutomateDAL();
                List<MBMComparisonResult> listMBMComparisonResult = new List<MBMComparisonResult>();
                 List<MBMComparisonResultCSG> listMBMComparisonResultCSG = new List<MBMComparisonResultCSG >();
                 var comparisonResult = new List<MBMComparisonResult>();
                 var comparisonResultCSG = new List<MBMComparisonResultCSG>();

                var BillingSystem = dal.GetMBMBillingSystem(invoiceId);
                if (BillingSystem=="CRM")
                {
                    listMBMComparisonResult = dal.GetMBMComparisonResult(invoiceId);
                   
                    switch (strInsertsOrCancels)
                    {
                        case "MBMComparisonInserts":
                            comparisonResult = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddBillItem
                                                                                   || i.Action == (int)ActionTypes.AddCharge
                                                                                   || i.Action == (int)ActionTypes.UpdateCharge
                                                                                   || i.Action == (int)ActionTypes.AddTelephone
                                                                                   || i.Action == (int)ActionTypes.ChangeTelephone).ToList();

                            counts.AddedBillItems = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddBillItem).Count();

                            counts.AddedCharge = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddCharge).Count();

                            counts.AddedTelephone = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.AddTelephone).Count();

                            counts.ChangedTelephone = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.ChangeTelephone).Count();

                            counts.UpdatedCharge = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.UpdateCharge).Count();
                            break;

                        case "MBMComparisonCancels":
                            comparisonResult = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteBillItem
                                                                                     || i.Action == (int)ActionTypes.DeleteCharge
                                                                                     || i.Action == (int)ActionTypes.DeleteTelephone).ToList();

                            counts.DeletedCharge = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteCharge).Count();

                            counts.DeletedBillItems = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteBillItem).Count();

                            counts.DeletedTelephone = listMBMComparisonResult.Where(i => i.Action == (int)ActionTypes.DeleteTelephone).Count();
                            break;
                    }

                    if (comparisonResult.Count > 0)
                    {
                        var comparisonResults = comparisonResult.Select(r => new
                        {
                            r.SnapShotId,
                            r.InvoiceId,
                            r.CRMAccountNumber,
                            r.AssetSearchCode1,
                            r.AssetSearchCode2,
                            r.ProfileName,
                            r.FeatureCode,
                            r.GLCode,
                            r.Charge,
                            r.ChargeType,
                            r.ActionType,
                            r.StartDate,
                            r.EndDate,
                            r.ItemType,
                            r.SubType,
                            r.SwitchId,
                            r.ItemId,
                            r.SequenceId,
                            r.LoadId,
                            r.Processed
                        }).ToList();
                    }
                }
                else
                {
                    listMBMComparisonResultCSG = dal.GetMBMComparisonResultCSG(invoiceId);
                    if (listMBMComparisonResultCSG.Count > 0)
                    {
                        switch (strInsertsOrCancels)
                        {
                            case "MBMComparisonInserts":
                                comparisonResultCSG = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.AddBillItem
                                                                                       || i.Action == (int)ActionTypes.AddCharge
                                                                                       || i.Action == (int)ActionTypes.UpdateCharge
                                                                                       || i.Action == (int)ActionTypes.AddTelephone
                                                                                       || i.Action == (int)ActionTypes.ChangeTelephone).ToList();

                                counts.AddedBillItems = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.AddBillItem).Count();

                                counts.AddedCharge = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.AddCharge).Count();

                                counts.AddedTelephone = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.AddTelephone).Count();

                                counts.ChangedTelephone = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.ChangeTelephone).Count();

                                counts.UpdatedCharge = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.UpdateCharge).Count();
                                break;

                            case "MBMComparisonCancels":
                                comparisonResultCSG = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.DeleteBillItem
                                                                                         || i.Action == (int)ActionTypes.DeleteCharge
                                                                                         || i.Action == (int)ActionTypes.DeleteTelephone).ToList();

                                counts.DeletedCharge = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.DeleteCharge).Count();

                                counts.DeletedBillItems = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.DeleteBillItem).Count();

                                counts.DeletedTelephone = listMBMComparisonResultCSG.Where(i => i.Action == (int)ActionTypes.DeleteTelephone).Count();
                                break;
                        }
                    }

                    if (comparisonResultCSG.Count > 0)
                    {
                        var comparisonResultsCSG = comparisonResultCSG.Select(r => new
                        {
                            r.SnapShotId,
                            r.InvoiceId, 
                            r.AssetSearchCode1,
                            r.AssetSearchCode2,
                            r.ProfileName,
                            r.Charge,
                            r.ChargeType,
                            r.ActionType,
                            r.StartDate,
                            r.EndDate,
                            r.ItemType,
                            r.SubType,
                            r.SwitchId,
                            r.ItemId,
                            r.SequenceId,
                            r.LoadId,
                            r.Processed
                        }).ToList();
                    }
                }

                if (comparisonResult.Count > 0 || comparisonResultCSG.Count > 0)
                {
                    GridView grdExport = new GridView();

                    if (comparisonResult.Count > 0)
                    {
                        grdExport.DataSource = comparisonResult;
                        grdExport.DataBind();
                    }
                    else if (comparisonResultCSG.Count > 0)
                    {
                        grdExport.DataSource = comparisonResultCSG;
                        grdExport.DataBind();
                    }

                    for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
                    {
                        grdExport.HeaderRow.Cells[j].Style.Add("background-color", "#E48D0F");

                        if (grdExport.HeaderRow.Cells[j].Text.Equals("Action"))
                        {
                            grdExport.HeaderRow.Cells[j].Visible = false;
                        }
                    }

                    for (int i = 0; i < grdExport.Rows.Count; i++)
                    {
                        GridViewRow row = grdExport.Rows[i];

                        //Hide the column "Action"
                        row.Cells[11].Visible = false;

                        //Change Color back to white
                        row.BackColor = System.Drawing.Color.White;

                        //Apply text style to each Row
                        row.Attributes.Add("class", "textmode");

                        //Apply style to Individual Cells of Alternating Row
                        if (i % 2 != 0)
                        {
                            for (int j = 0; j < grdExport.HeaderRow.Cells.Count; j++)
                            {
                                row.Cells[j].Style.Add("background-color", "#C2D69B");
                            }
                        }
                    }

                    fileName = (strInsertsOrCancels + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xls");

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);

                    grdExport.RenderControl(htmlTextWriter);                                        

                    using (FileStream fs = new FileStream(filePath + fileName, FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, System.Text.Encoding.UTF8))
                        {
                            w.WriteLine(stringWriter.ToString());
                        }
                    }                     
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                ExceptionLogging.SendErrorTomail(ex);
                Logging logger = new Logging();

                logger.LogExceptionToDatabase(new ApplicationLogEntity()
                {
                    //UserName = UserName,
                    LogType = ApplicationLogType.SystemRaised,
                    ExceptionDateTime = DateTime.Now,
                    CodeLocation = "ExportComparisonDataToExcel()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }
            return fileName;
        }
       
    }

}
