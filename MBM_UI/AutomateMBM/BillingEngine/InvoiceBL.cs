//using Automate.DataAccess.edmx;
//using Automate.DataAccess.Log;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutomateMBM.BillingEngine
//{
//    public class InvoiceBL
//    {
//        public string GetNextGeneratedInvoiceNumber(int invoiceType, string prefix)                                   
//        {
//            string invoiceNumber = string.Empty;

//            try
//            {
//                AutomateEntities db = new AutomateEntities();
//                var invoices = db.get_InvoiceNumbersByTypeId(invoiceType);

//                // Get highest invoice number where prefix matches and remaining is a valid number.
//                Int64 highestInvoiceNumber = 10000;
                
//                foreach (var i in invoices)
//                {
//                    string selectedInvoice = i.sInvoiceNumber;

//                    if (selectedInvoice.IndexOf(prefix, 0, prefix.Length) != -1)
//                    {
//                        selectedInvoice = selectedInvoice.Replace(prefix, String.Empty);

//                        Int64 stripedInvoiceNumber;
//                        if (Int64.TryParse(selectedInvoice, out stripedInvoiceNumber))
//                        {
//                            if (stripedInvoiceNumber > highestInvoiceNumber)
//                            {
//                                highestInvoiceNumber = stripedInvoiceNumber;
//                            }
//                        }
//                    }
//                }

//                invoiceNumber = (highestInvoiceNumber + 1).ToString();
//            }
//            catch (Exception ex)
//            {
//                ExceptionLogging.SendErrorTomail(ex);
//            }

//            return invoiceNumber;
//        }

//    }
//}
