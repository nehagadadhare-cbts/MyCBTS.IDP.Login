using Automate.DataAccess;
using Automate.DataAccess.edmx;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace APIAutomateMBM.Controllers
{
    public class InvoiceController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ApproveInvoice(string invoiceNumber, Guid guid)
        {
            string message = string.Empty;

            AutomateDAL dal = new AutomateDAL();
            TokenHistory tokenHistory = new TokenHistory();

            tokenHistory = dal.GetTokenHistory(invoiceNumber, guid);

            if (tokenHistory != null)
            {
                if (!tokenHistory.IsApproved)
                {
                    int linkExpire = Convert.ToInt32(ConfigurationManager.AppSettings["APIExpiry"].ToString());
                    if (DateTime.Now.Subtract(tokenHistory.InsertedDate).Days < linkExpire)
                    {
                        int count = 0;
                        int invoiceId = dal.GetInvoiceIdByNumber(invoiceNumber);
                        count = dal.GetUnprocessedMBMComparisonResultCount(invoiceId);

                        if (count > 0)
                        {                            
                            dal.UpdateTokenHistory(invoiceNumber, guid, ApprovedBy());
                            dal.UpdateMBMAutomateStatus(invoiceId, "ApproveChanges");
                            dal.UpdateMBMStatus(invoiceNumber, "ApproveChanges");

                            message = string.Format("The changes have been approved for invoice number {0}", invoiceNumber);
                        }
                        else
                            message = string.Format("There is no comaprision data to approve for invoice number {0}", invoiceNumber);
                    }
                    else
                    {
                        message = "This link has expired";
                    }
                }
                else
                {
                    message = string.Format("Invoice {0} is already approved on {1}", invoiceNumber, tokenHistory.ApprovedDate);
                }
            }
            else
            {
                message = "This link is incorrect";
            }

            var response = new HttpResponseMessage();

            response.Content = new StringContent(ResponseMessage(message));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        private string ResponseMessage(string message)
        {
            string body1 = @"
                        <!DOCTYPE html>
                        <html lang=""en"">
                            <body  style='background: #fff'>
                                <div id=""body"">
                            <section class=""featured"">
                                <div class=""content-wrapper"">
                                    <hgroup class=""title"">
                                        <h1>";
            string body2 = @"</h1>
                        </hgroup>            
                    </div>
                </section>    
            </div>            
                </body>
            </html>
            <style>
                body {
                color: #000;
                }                
            </style>";

            string body = body1 + message + body2;

            return body;
        }

        private string ApprovedBy()
        {
            string name = string.Empty;
            try
            {                
                name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;                
            }
            catch
            { }
            return name;
        }
    }
}
