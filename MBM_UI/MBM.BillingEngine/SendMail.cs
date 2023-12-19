using MBM.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BillingEngine
{
    public class SendMail
    {
        string connectionString;
        string smtpServer;

        public SendMail()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();
            smtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString();
        }


        public void SendMailMessage(MailMessage message)
        {
            //SmtpClient smtp = new SmtpClient("161.155.126.57");
            SmtpClient smtp = new SmtpClient();
            if (!string.IsNullOrEmpty(smtpServer))
            {
                smtp.Host = smtpServer;
                smtp.Send(message);
            }
        }
        /// <summary>
        /// mais for plain text
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendMailMessage(MailAddress from, System.Collections.Specialized.StringCollection to, string subject, string body)
        {
            //validations             
            if (from == null || string.IsNullOrEmpty(from.Address)) throw new ArgumentException("empty from address");
            if (null == to) throw new ArgumentNullException("empty from to");

            MailMessage message = new MailMessage();

            message.From = from;
            foreach (string address in to)
            {
                message.To.Add(new MailAddress(address));
            }
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SendMailMessage(message);
        }

        /// <summary>
        /// Sends an email using information in the given parameters.
        /// </summary>
        /// <param name="emailToAddress">Address(es), to which to send message; can contain comma delimited list of addresses.</param>
        /// <param name="emailSubject">The subject of email message</param>
        /// <param name="textBody">The body of message in plain text</param>
        /// <param name="htmlBody">The body of message in HTML format</param>
        /// <param name="emailFromAddress">The From email address. If empty, the default will be used.</param>
        /// <returns></returns>
        public bool SendEmail(System.Collections.Specialized.StringCollection emailToAddress, string emailSubject, 
            string textBody, string htmlBody, string emailFromAddress, Attachment attachment)
        {
            var isSuccessful = false;
            try
            {
                var mailMsg = new MailMessage();
                var smtpClient = new SmtpClient();
                //mailMsg.To.Add(emailToAddress);
                foreach (string address in emailToAddress)
                {
                    mailMsg.To.Add(new MailAddress(address));
                }

                if (!string.IsNullOrEmpty(textBody))
                {
                    mailMsg.IsBodyHtml = false;
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(textBody, null, "text/plain"));
                }
                if (!String.IsNullOrEmpty(htmlBody))
                {
                    mailMsg.IsBodyHtml = true;
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html"));
                }

                mailMsg.Subject = emailSubject;

                if (!String.IsNullOrEmpty(emailFromAddress))
                {
                    mailMsg.From = new MailAddress(emailFromAddress);
                    //else, this will use default address defined in web.config under <system.net />
                }

                if (attachment != null)
                    mailMsg.Attachments.Add(attachment);

                SendMailMessage(mailMsg);

                // if we made it this far, indicate success
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                string methodParams = String.Format("emailToAddress={0};emailSubject={1};textBody={2};htmlBody={3};emailFromAddress={4}",
                    emailToAddress, emailSubject, textBody, htmlBody, emailFromAddress);
                Logger _logger = new Logger(connectionString);
                _logger.Exception(ex, -188224);

                isSuccessful = false;
            }

            return isSuccessful;
        }
    }
}
