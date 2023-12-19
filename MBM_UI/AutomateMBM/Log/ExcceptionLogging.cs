using System;  
using System.Net.Mail;  
using System.Configuration;  
using System.Net;  
using context = System.Web.HttpContext;  
  
/// <summary>  
/// Summary description for ExceptionLogging  
/// </summary>  
internal static class ExceptionLogging  
{  
  
    private static String StackTrace,InnerException, Errormsg, ErrorLocation, extype, FromMail, ToUser,ToAMTeam, Subject, HostAdd, EmailHead, EmailSignature;
    private delegate void MailDelegate(string from,string to,string subject,string body);

    internal static void SendErrorTomail(Exception exmail)  
    {  
  
        try  
        {  
            var newline = "<br/>";  
            //ErrorlineNo = exmail.StackTrace.Substring(exmail.StackTrace.Length - 7, 7);  
            StackTrace = exmail.StackTrace.ToString();
            InnerException = (exmail.InnerException==null)?"":(exmail.InnerException.ToString());
            Errormsg = exmail.GetType().Name.ToString();  
            extype = exmail.GetType().ToString();  
            //exurl = context.Current.Request.Url.ToString();  
            ErrorLocation = exmail.Message.ToString();
            EmailHead = "<b>Dear Team,</b>" + newline + newline + "An exception occurred in a MBM Automation Application With following Details" + "<br/>" + "<br/>";
            EmailSignature = newline + "Thanks and Regards" + newline + "    " + "     " + "<b>Application Admin </b>" + "</br>";
            Subject = "Exception occurred" + " " + "in MBM Automation";  
            HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            string errorToAMMail = EmailHead + "<b>Log Written Date: </b>" + " " + DateTime.Now.ToString() + newline + "<b>Error Message:</b>" + " " + Errormsg + newline + "<b>Exception Type:</b>" + " " + extype + newline + "<b>Inner Exception:</b>" + " " + InnerException + newline + "<b> Error Details :</b>" + " " + ErrorLocation + newline +newline+ "<b>Stack Trace:</b>" + " " + StackTrace + newline + newline + newline + EmailSignature;

            string errorToUserMail = "<b>Dear User,</b>" + "<br/>" +newline+newline+ "An exception occurred in a MBM Automation Application at " + DateTime.Now.ToString()+"." + newline + "AM Team has been notified." + newline + newline  + EmailSignature;

            FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
            ToUser = ConfigurationManager.AppSettings["ToUser"].ToString();
            ToAMTeam = ConfigurationManager.AppSettings["ToAMTeam"].ToString();
           
            MailDelegate del = new MailDelegate(ExceptionLogging.mailContent);

            del.Invoke(FromMail, ToUser, Subject, errorToUserMail);
            del.Invoke(FromMail, ToAMTeam, Subject, errorToAMMail);

            //mailContent(FromMail, ToUser, Subject, errorToUserMail);

            //mailContent(FromMail, ToAMTeam, Subject, errorToAMMail);                    
           
        }  
        catch (Exception em)  
        {  
            em.ToString();  
  
        }  
    }
    private static void mailSend(MailMessage message)
    {
        using (SmtpClient smtp = new SmtpClient(HostAdd, 25))
        {
            smtp.Credentials = new NetworkCredential();
            smtp.Send(message);
        } 
    }

    private static void mailContent(string From,string To,string Subject,string Body)
    {
        using (MailMessage mailMessage = new MailMessage(From,To,Subject,Body))
        {           
            mailMessage.IsBodyHtml = true;                        
            mailSend(mailMessage);

        }  
    }
}  