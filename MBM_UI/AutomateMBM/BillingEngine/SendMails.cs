using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AutomateMBM.Entities;

namespace AutomateMBM.BillingEngine
{
    internal class SendMails
    {
        static string smtpAddress = ConfigurationManager.AppSettings["Host"].ToString();            
        static int portNumber = 25;
        static string emailFromAddress = ConfigurationManager.AppSettings["FromMail"].ToString();         
        static string emailToAddress = ConfigurationManager.AppSettings["ToUser"].ToString();
        static string ApllicationServer = ConfigurationManager.AppSettings["UIServer"].ToString();
        static string ApiServer = ConfigurationManager.AppSettings["ApiServer"].ToString();
        
        static string MailBody(Guid guid, string invoiceNumber, int invoiceTypeId, int invoiceId, ViewChangeCount count,int month,int year)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("Please find the attached MBM comparison result for Invoice Number {0} ({1}/{2}).<br/><br/>", invoiceNumber,month,year));          

            builder.Append("<table  border='2' cellpadding='2' cellspacing='0' width='50%' style = 'border-collapse: collapse;'>");
            builder.AppendFormat("<tr><th>Action Type</th><th>Count</th></tr>");
            builder.AppendFormat("<tr><td>Added Bill Items</td><td>{0}</td></tr>", count.AddedBillItems);
            builder.AppendFormat("<tr><td>Added Charge</td><td>{0}</td></tr>", count.AddedCharge);
            builder.AppendFormat("<tr><td>Added Telephone</td><td>{0}</td></tr>", count.AddedTelephone);
            builder.AppendFormat("<tr><td>Changed Telephone</td><td>{0}</td></tr>", count.ChangedTelephone);
            builder.AppendFormat("<tr><td>Updated Charge</td><td>{0}</td></tr>", count.UpdatedCharge);
            builder.AppendFormat("<tr><td>Deleted Telephone</td><td>{0}</td></tr>", count.DeletedTelephone);
            builder.AppendFormat("<tr><td>Deleted Charge</td><td>{0}</td></tr>", count.DeletedCharge);
            builder.AppendFormat("<tr><td>Deleted Bill Item</td><td>{0}</td></tr>", count.DeletedBillItems);           
            builder.Append("</table><br/>");

            builder.Append("Click on the link to approve by email <a href=" + string.Format("{0}/api/Invoice/ApproveInvoice/{1}/{2}", ApiServer, invoiceNumber, guid) + ">Approve</a><br/>");  //<small style='color: red;'> (Do not click from Mobile)</small>
            builder.Append("Click <a href=" + string.Format("{0}/Default?customer={1}&invoiceId={2}",ApllicationServer, invoiceTypeId, invoiceId) + ">HERE</a> to navigate MBM website.");
            string innerString = builder.ToString();

            string body = MailBody(innerString);
            return body;
        }

        private static string MailBody(string messageBody)
        {            
            var newline = "<br/>";
            string EmailSignature = newline + "Thanks and Regards" + newline + "    " + "     " + "<b>Application Admin </b>" + "</br>";
            string body = "<b>Dear User,</b>" + "<br/>"  +newline + newline+string.Format("{0}", messageBody) + newline + newline + EmailSignature;
            return body;
        }


        internal static void ComparisonResultMailSend(string attachmentForInsert, string attachmentForCancel, Guid guid, string invoiceNumber, int invoiceTypeId, int invoiceId,ref ViewChangeCount counts,string customerName,int month,int year)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                //mail.Subject = string.Format("{0}- View\\Approve Changes For Invoice {1} ({2}/{3})",customerName, invoiceNumber,month,year);
                mail.Subject = string.Format("{0} ({1}/{2}) - View\\Approve Changes", customerName, month, year);
                mail.Body = MailBody(guid, invoiceNumber, invoiceTypeId, invoiceId, counts, month, year);
                mail.IsBodyHtml = true;

                if (!string.IsNullOrWhiteSpace(attachmentForInsert))
                {
                    mail.Attachments.Add(new Attachment(attachmentForInsert));
                }
                if (!string.IsNullOrWhiteSpace(attachmentForCancel))
                {
                    mail.Attachments.Add(new Attachment(attachmentForCancel));
                }
                SmtpSendMail(mail);
            }
            
        }

        internal static void Emails(string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = MailBody(body);
                mail.IsBodyHtml = true;
               
                SmtpSendMail(mail);
            }
        }

        private static void SmtpSendMail(MailMessage mail)
        {
            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            {
                smtp.Credentials = new NetworkCredential();
                smtp.Send(mail);
            }
        }
    }
}
