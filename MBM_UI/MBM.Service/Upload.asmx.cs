using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Transactions;
using System.Data;

namespace MBM.Service
{
    /// <summary>
    /// Summary description for UploadTest
    /// </summary>
    [WebService(Namespace = "http://gcom.cbts.cinbell.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Upload : System.Web.Services.WebService
    {
        [WebMethod]
        public string Transfer(string Cost_Center,
                        string Gold_ID,
                        string Industry_focus_group,
                        string Business_Segment,
                        string SSO_ID,
                        string Basket_Number,
                        string Lastname,
                        string Firstname,
                        string BAN,
                        string Asset_ID,
                        string Bundle,
                        string Start_date_of_Profile,
                        string End_date_of_Profile,
                        string Deliver_Address,
                        string User_Email_Adress,
                        string End_User_Contact,
                        string Order_Type,
                        string Order_Status,
                        string Address_Mode,
                        string Long_Description,
                        string Short_Description,
                        string Business_Unit_Views,
                        string Quantity,
                        string Price,
                        string Currency,
                        string GE_Purchase_Order,
                        string Telephone_Number,
                        string Telephone_MAC_Address,
                        string UOM,
                        string AreaCodeSelection,
                        string SelectedAreaCode,
                        string SiteID)
        {
            string body = string.Empty;
            bool testViprError = false;
            bool serviceDeskError = false;
            try
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["ServiceDeskLogicSwitch"]))
                    {
                        try
                        {
                            using (System.Transactions.TransactionScope sdScope = new System.Transactions.TransactionScope())
                            {
                                using (cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["gcom"].ConnectionString))
                                {
                                    cmd.Connection.Open();
                                    cmd.Parameters.Add("@Cost_Center", SqlDbType.NText).Value = Cost_Center;
                                    cmd.Parameters.Add("@Gold_ID", SqlDbType.NText).Value = Gold_ID;
                                    cmd.Parameters.Add("@Industry_focus_group", SqlDbType.NText).Value = Industry_focus_group;
                                    cmd.Parameters.Add("@Business_Segment", SqlDbType.NText).Value = Business_Segment;
                                    cmd.Parameters.Add("@SSO_ID", SqlDbType.NText).Value = SSO_ID;
                                    cmd.Parameters.Add("@Basket_Number", SqlDbType.NText).Value = Basket_Number;
                                    cmd.Parameters.Add("@Lastname", SqlDbType.NText).Value = Lastname;
                                    cmd.Parameters.Add("@Firstname", SqlDbType.NText).Value = Firstname;
                                    cmd.Parameters.Add("@BAN", SqlDbType.NText).Value = BAN;
                                    cmd.Parameters.Add("@Asset_ID", SqlDbType.NText).Value = Asset_ID;
                                    cmd.Parameters.Add("@Bundle", SqlDbType.NText).Value = Bundle;
                                    cmd.Parameters.Add("@Start_date_of_Profile", SqlDbType.NText).Value = Start_date_of_Profile;
                                    cmd.Parameters.Add("@End_date_of_Profile", SqlDbType.NText).Value = End_date_of_Profile;
                                    cmd.Parameters.Add("@Deliver_Address", SqlDbType.NText).Value = Deliver_Address;
                                    cmd.Parameters.Add("@User_Email_Adress", SqlDbType.NText).Value = User_Email_Adress;
                                    cmd.Parameters.Add("@End_User_Contact", SqlDbType.NText).Value = End_User_Contact;
                                    cmd.Parameters.Add("@Order_Type", SqlDbType.NText).Value = Order_Type;
                                    cmd.Parameters.Add("@Order_Status", SqlDbType.NText).Value = Order_Status;
                                    cmd.Parameters.Add("@Address_Mode", SqlDbType.NText).Value = Address_Mode;
                                    cmd.Parameters.Add("@Long_Description", SqlDbType.NText).Value = Long_Description;
                                    cmd.Parameters.Add("@Short_Description", SqlDbType.NText).Value = Short_Description;
                                    cmd.Parameters.Add("@Business_Unit_Views", SqlDbType.NText).Value = Business_Unit_Views;
                                    cmd.Parameters.Add("@Quantity", SqlDbType.NText).Value = Quantity;
                                    cmd.Parameters.Add("@Price", SqlDbType.NText).Value = Price;
                                    cmd.Parameters.Add("@Currency", SqlDbType.NText).Value = Currency;
                                    cmd.Parameters.Add("@GE_Purchase_Order", SqlDbType.NText).Value = GE_Purchase_Order;
                                    cmd.Parameters.Add("@Telephone_Number", SqlDbType.NText).Value = Telephone_Number;
                                    cmd.Parameters.Add("@Telephone_MAC_Address", SqlDbType.NText).Value = Telephone_MAC_Address;
                                    cmd.Parameters.Add("@UOM", SqlDbType.NText).Value = UOM;
                                    cmd.Parameters.Add("@AreaCodeSelection", SqlDbType.NText).Value = AreaCodeSelection;
                                    cmd.Parameters.Add("@SelectedAreaCode", SqlDbType.NText).Value = SelectedAreaCode;
                                    cmd.Parameters.Add("@SiteID", SqlDbType.NText).Value = SiteID;
                                    cmd.CommandText = "dbo.UploadTextContents";
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.ExecuteNonQuery();

                                    using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                                    {
                                        message.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["mailFrom"]);
                                        message.To.Add(ConfigurationManager.AppSettings["mailTo"]);
                                        message.Subject = "NEW";
                                        body = serviceOrderPrettyString(Cost_Center,
                                                                                 Gold_ID,
                                                                                 Industry_focus_group,
                                                                                 Business_Segment,
                                                                                 SSO_ID,
                                                                                 Basket_Number,
                                                                                 Lastname,
                                                                                 Firstname,
                                                                                 BAN,
                                                                                 Asset_ID,
                                                                                 Bundle,
                                                                                 Start_date_of_Profile,
                                                                                 End_date_of_Profile,
                                                                                 Deliver_Address,
                                                                                 User_Email_Adress,
                                                                                 End_User_Contact,
                                                                                 Order_Type,
                                                                                 Order_Status,
                                                                                 Address_Mode,
                                                                                 Long_Description,
                                                                                 Short_Description,
                                                                                 Business_Unit_Views,
                                                                                 Quantity,
                                                                                 Price,
                                                                                 Currency,
                                                                                 GE_Purchase_Order,
                                                                                 Telephone_Number,
                                                                                 Telephone_MAC_Address,
                                                                                 UOM,
                                                                                 AreaCodeSelection,
                                                                                 SelectedAreaCode,
                                                                                 SiteID);
                                        message.Body = body;

                                        using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["mailHost"]))
                                        {
                                            smtp.Send(message);
                                        }

                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "dbo.LogMailSending";
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@Sent", SqlDbType.DateTime).Value = DateTime.Now;
                                        cmd.Parameters.Add("@Contents", SqlDbType.NText).Value = message.Body;
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                sdScope.Complete();
                            }
                        }
                        catch (Exception e)
                        {
                            serviceDeskError = true;
                            throw e;
                        }
                    }
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["VIPRLogicSwitch"]))
                    {
                        try
                        {

                            using (System.Transactions.TransactionScope viprScope = new System.Transactions.TransactionScope())
                            {

                                using (cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VIPR"].ConnectionString))
                                {
                                    cmd.Connection.Open();
                                    cmd.Parameters.Clear(); //due to having the Service Desk insert logic on at the same time as the VIPR Logic
                                    cmd.Parameters.Add("@Cost_Center", SqlDbType.NText).Value = Cost_Center;
                                    cmd.Parameters.Add("@Gold_ID", SqlDbType.NText).Value = Gold_ID;
                                    cmd.Parameters.Add("@Industry_focus_group", SqlDbType.NText).Value = Industry_focus_group;
                                    cmd.Parameters.Add("@Business_Segment", SqlDbType.NText).Value = Business_Segment;
                                    cmd.Parameters.Add("@SSO_ID", SqlDbType.NText).Value = SSO_ID;
                                    cmd.Parameters.Add("@Basket_Number", SqlDbType.NText).Value = Basket_Number;
                                    cmd.Parameters.Add("@Lastname", SqlDbType.NText).Value = Lastname;
                                    cmd.Parameters.Add("@Firstname", SqlDbType.NText).Value = Firstname;
                                    cmd.Parameters.Add("@BAN", SqlDbType.NText).Value = BAN;
                                    cmd.Parameters.Add("@Asset_ID", SqlDbType.NText).Value = Asset_ID;
                                    cmd.Parameters.Add("@Bundle", SqlDbType.NText).Value = Bundle;
                                    cmd.Parameters.Add("@Start_date_of_Profile", SqlDbType.NText).Value = Start_date_of_Profile;
                                    cmd.Parameters.Add("@End_date_of_Profile", SqlDbType.NText).Value = End_date_of_Profile;
                                    cmd.Parameters.Add("@Deliver_Address", SqlDbType.NText).Value = Deliver_Address;
                                    cmd.Parameters.Add("@User_Email_Adress", SqlDbType.NText).Value = User_Email_Adress;
                                    cmd.Parameters.Add("@End_User_Contact", SqlDbType.NText).Value = End_User_Contact;
                                    cmd.Parameters.Add("@Order_Type", SqlDbType.NText).Value = Order_Type;
                                    cmd.Parameters.Add("@Order_Status", SqlDbType.NText).Value = Order_Status;
                                    cmd.Parameters.Add("@Address_Mode", SqlDbType.NText).Value = Address_Mode;
                                    cmd.Parameters.Add("@Long_Description", SqlDbType.NText).Value = Long_Description;
                                    cmd.Parameters.Add("@Short_Description", SqlDbType.NText).Value = Short_Description;
                                    cmd.Parameters.Add("@Business_Unit_Views", SqlDbType.NText).Value = Business_Unit_Views;
                                    cmd.Parameters.Add("@Quantity", SqlDbType.NText).Value = Quantity;
                                    cmd.Parameters.Add("@Price", SqlDbType.NText).Value = Price;
                                    cmd.Parameters.Add("@Currency", SqlDbType.NText).Value = Currency;
                                    cmd.Parameters.Add("@GE_Purchase_Order", SqlDbType.NText).Value = GE_Purchase_Order;
                                    cmd.Parameters.Add("@Telephone_Number", SqlDbType.NText).Value = Telephone_Number;
                                    cmd.Parameters.Add("@Telephone_MAC_Address", SqlDbType.NText).Value = Telephone_MAC_Address;
                                    cmd.Parameters.Add("@UOM", SqlDbType.NText).Value = UOM;
                                    cmd.Parameters.Add("@AreaCodeSelection", SqlDbType.NText).Value = AreaCodeSelection;
                                    cmd.Parameters.Add("@SelectedAreaCode", SqlDbType.NText).Value = SelectedAreaCode;
                                    cmd.Parameters.Add("@SiteID", SqlDbType.NText).Value = SiteID;
                                    cmd.CommandText = "dbo.UploadTextContents";
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.ExecuteNonQuery();
                                }


                                viprScope.Complete();
                            }

                            //START:ERO:2197
                            //Mail Sending and mail Logging implemented for VIRP (same as service desk) 

                            using (System.Transactions.TransactionScope viprLogScope = new System.Transactions.TransactionScope())
                            {
                                using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                                {
                                    message.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["mailFrom"]);
                                    message.To.Add(ConfigurationManager.AppSettings["mailTo"]);
                                    message.Subject = "NEW";
                                    body = serviceOrderPrettyString(Cost_Center,
                                                                             Gold_ID,
                                                                             Industry_focus_group,
                                                                             Business_Segment,
                                                                             SSO_ID,
                                                                             Basket_Number,
                                                                             Lastname,
                                                                             Firstname,
                                                                             BAN,
                                                                             Asset_ID,
                                                                             Bundle,
                                                                             Start_date_of_Profile,
                                                                             End_date_of_Profile,
                                                                             Deliver_Address,
                                                                             User_Email_Adress,
                                                                             End_User_Contact,
                                                                             Order_Type,
                                                                             Order_Status,
                                                                             Address_Mode,
                                                                             Long_Description,
                                                                             Short_Description,
                                                                             Business_Unit_Views,
                                                                             Quantity,
                                                                             Price,
                                                                             Currency,
                                                                             GE_Purchase_Order,
                                                                             Telephone_Number,
                                                                             Telephone_MAC_Address,
                                                                             UOM,
                                                                             AreaCodeSelection,
                                                                             SelectedAreaCode,
                                                                             SiteID);
                                    message.Body = body;

                                    using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["mailHost"]))
                                    {
                                        smtp.Send(message);
                                    }

                                    using (cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["gcom"].ConnectionString))
                                    {
                                        cmd.Connection.Open();
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "dbo.LogMailSending";
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@Sent", SqlDbType.DateTime).Value = DateTime.Now;
                                        cmd.Parameters.Add("@Contents", SqlDbType.NText).Value = message.Body;
                                        cmd.ExecuteNonQuery();
                                    }
                                    //END ERO:2197
                                }

                                viprLogScope.Complete();
                            }
                        }
                        catch (Exception e)
                        {
                            testViprError = true;
                            throw e;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;

                //START:ERO:2197
                //Capture "Source" and "Stack Trace" from the exception.
                string StackTrace = ex.StackTrace;
                string Source = ex.Source;
                //END:ERO:2197

                try
                {
                    if (serviceDeskError)
                    {
                        string messageBody = "GCom Error" + errorMsg.ToString()
                                        + "\n\nGCom Upload Webservice Parameters" + "\n\n"
                                        + body;
                        SendEmail(ConfigurationManager.AppSettings["supportEmail"].ToString(), "GCom Service Desk Error", messageBody);


                    }
                    if (testViprError)
                    {
                        string messageBody = "GCom Error" + errorMsg.ToString()
                                        + "\n\nGCom Upload Webservice Parameters" + "\n\n"
                                        + body;
                        //  SendEmail(ConfigurationManager.AppSettings["debugEmail"].ToString(), "GCom Vipr Error", messageBody);

                        string ToEmails = ConfigurationManager.AppSettings["supportEmail"].ToString() + "," + ConfigurationManager.AppSettings["DebugEmail"].ToString();
                        SendEmail(ToEmails, "GCom Vipr Error", messageBody);

                    }
                }
                catch (Exception ex2)
                {

                    errorMsg += "\n\n" + ex2.Message;

                    //START:ERO:2197
                    //Capture "Source" and "Stack Trace" from the exception.
                    StackTrace += "\n\n" + ex2.StackTrace;
                    Source += "\n\n" + ex2.Source;
                    //END:ERO:2197
                }

                try
                {
                    // Log Error in Database

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["VIPRLogicSwitch"]) && testViprError == true)
                        {
                            using (System.Transactions.TransactionScope scope1 = new System.Transactions.TransactionScope())
                            {
                                using (cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VIPR"].ConnectionString))
                                {

                                    cmd.Connection.Open();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "dbo.usp_ErrorLogEntry";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@ExceptionMessage", SqlDbType.NText).Value = errorMsg;
                                    cmd.Parameters.Add("@UploadWebServiceParameters", SqlDbType.NText).Value = body;
                                    cmd.ExecuteNonQuery();

                                }
                                scope1.Complete();
                            }

                            //START:ERO:2197
                            //Logging error information into "GCOM" dtabase using "usp_ErrorLogEntry" stored prcoedure.
                            using (System.Transactions.TransactionScope scope3 = new System.Transactions.TransactionScope())
                            {

                                using (cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["gcom"].ConnectionString))
                                {
                                    cmd.Connection.Open();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "dbo.usp_ErrorLogEntry";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@ExceptionMessage", SqlDbType.NText).Value = errorMsg;
                                    cmd.Parameters.Add("@UploadWebServiceParameters", SqlDbType.NText).Value = body;
                                    cmd.Parameters.Add("@StackTrace", SqlDbType.NText).Value = StackTrace;
                                    cmd.Parameters.Add("@Source", SqlDbType.NText).Value = Source;

                                    cmd.ExecuteNonQuery();
                                }
                                scope3.Complete();
                            }
                            //END:ERO:2197

                        }

                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["ServiceDeskLogicSwitch"]) && serviceDeskError == true)
                        {
                            using (System.Transactions.TransactionScope scope2 = new System.Transactions.TransactionScope())
                            {
                                using (cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["gcom"].ConnectionString))
                                {
                                    cmd.Connection.Open();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "dbo.usp_ErrorLogEntry";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@ExceptionMessage", SqlDbType.NText).Value = errorMsg;
                                    cmd.Parameters.Add("@UploadWebServiceParameters", SqlDbType.NText).Value = body;

                                    //START:ERO:2197
                                    //Logging error "StackTrace" and "Source" information GCOM" dtabase using "usp_ErrorLogEntry" stored prcoedure.
                                    cmd.Parameters.Add("@StackTrace", SqlDbType.NText).Value = StackTrace;
                                    cmd.Parameters.Add("@Source", SqlDbType.NText).Value = Source;
                                    //END:ERO:2197

                                    cmd.ExecuteNonQuery();
                                }
                                scope2.Complete();
                            }
                        }

                    }

                }

                catch
                {
                }
                if ((serviceDeskError && Convert.ToBoolean(ConfigurationManager.AppSettings["ServiceDeskLogicSwitch"])) || (testViprError && !Convert.ToBoolean(ConfigurationManager.AppSettings["ServiceDeskLogicSwitch"])))
                {
                    return "<Response><Status code='1' text='ERROR' /></Response>";
                }
                else if (testViprError && Convert.ToBoolean(ConfigurationManager.AppSettings["ServiceDeskLogicSwitch"]))
                {
                    return "<Response><Status code='0' text='OK' /></Response>";
                }
                else
                {
                    return "<Response><Status code='1' text='ERROR' /></Response>";
                }
            }
            return "<Response><Status code='0' text='OK' /></Response>";
            //return body;
            //	return (int)StatusCode.Normal;
        }
        // you may want to delete this method if you are suing some other APi but it may be 
        // helpfull for logging in the future.
        private string serviceOrderPrettyString(string Cost_Center,
                        string Gold_ID,
                        string Industry_focus_group,
                        string Business_Segment,
                        string SSO_ID,
                        string Basket_Number,
                        string Lastname,
                        string Firstname,
                        string BAN,
                        string Asset_ID,
                        string Bundle,
                        string Start_date_of_Profile,
                        string End_date_of_Profile,
                        string Deliver_Address,
                        string User_Email_Adress,
                        string End_User_Contact,
                        string Order_Type,
                        string Order_Status,
                        string Address_Mode,
                        string Long_Description,
                        string Short_Description,
                        string Business_Unit_Views,
                        string Quantity,
                        string Price,
                        string Currency,
                        string GE_Purchase_Order,
                        string Telephone_Number,
                        string Telephone_MAC_Address,
                        string UOM,
                        string AreaCodeSelection,
                        string SelectedAreaCode,
                        string SiteID)
        {

            string orderRequest = " Cost_Center	              - " + Cost_Center;
            orderRequest = orderRequest + "\r\n	Gold_ID	              - " + Gold_ID;
            orderRequest = orderRequest + "\r\n	Industry_focus_group  - " + Industry_focus_group;
            orderRequest = orderRequest + "\r\n	Business_Segment      - " + Business_Segment;
            orderRequest = orderRequest + "\r\n	SSO_ID	              - " + SSO_ID;
            orderRequest = orderRequest + "\r\n	Basket_Number	      - " + Basket_Number;
            orderRequest = orderRequest + "\r\n	Lastname	          - " + Lastname;
            orderRequest = orderRequest + "\r\n	Firstname	          - " + Firstname;
            orderRequest = orderRequest + "\r\n	BAN	                  - " + BAN;
            orderRequest = orderRequest + "\r\n	Asset_ID        	  - " + Asset_ID;
            orderRequest = orderRequest + "\r\n	Bundle	              - " + Bundle;
            orderRequest = orderRequest + "\r\n	Start_date_of_Profile - " + Start_date_of_Profile;
            orderRequest = orderRequest + "\r\n	End_date_of_Profile	  - " + End_date_of_Profile;
            orderRequest = orderRequest + "\r\n	Deliver_Address	      - " + Deliver_Address;
            orderRequest = orderRequest + "\r\n	User_Email_Adress	  - " + User_Email_Adress;
            orderRequest = orderRequest + "\r\n	End_User_Contact	  - " + End_User_Contact;
            orderRequest = orderRequest + "\r\n	Order_Type	          - " + Order_Type;
            orderRequest = orderRequest + "\r\n	Order_Status	      - " + Order_Status;
            orderRequest = orderRequest + "\r\n	Address_Mode	      - " + Address_Mode;
            orderRequest = orderRequest + "\r\n	Long_Description	  - " + Long_Description;
            orderRequest = orderRequest + "\r\n	Short_Description	  - " + Short_Description;
            orderRequest = orderRequest + "\r\n	Business_Unit_Views	  - " + Business_Unit_Views;
            orderRequest = orderRequest + "\r\n	Quantity	          - " + Quantity;
            orderRequest = orderRequest + "\r\n	Price	              - " + Price;
            orderRequest = orderRequest + "\r\n	Currency	          - " + Currency;
            orderRequest = orderRequest + "\r\n	GE_Purchase_Order	  - " + GE_Purchase_Order;
            orderRequest = orderRequest + "\r\n	Telephone_Number	  - " + Telephone_Number;
            orderRequest = orderRequest + "\r\n	Telephone_MAC_Address - " + Telephone_MAC_Address;
            orderRequest = orderRequest + "\r\n	UOM	                  - " + UOM;
            orderRequest = orderRequest + "\r\n	AreaCodeSelection	                  - " + AreaCodeSelection;
            orderRequest = orderRequest + "\r\n	SelectedAreaCode	                  - " + SelectedAreaCode;
            orderRequest = orderRequest + "\r\n	SiteID	                  - " + SiteID;
            return orderRequest;
        }
        private bool SendEmail(string sTo, string sSubject, string sBody)
        {
            //string sLocation = "";
            try
            {
                System.Net.Mail.MailMessage insMail = new System.Net.Mail.MailMessage();
                insMail.IsBodyHtml = true;
                insMail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["PRFAppsEmail"].ToString());
                insMail.To.Add(sTo.Trim());
                insMail.Subject = sSubject.Trim();
                sBody = "<p>THIS MESSAGE IS PART OF AN AUTOMATED EMAIL. PLEASE DO NOT REPLY TO THIS EMAIL</p>" + sBody;
                insMail.Body = sBody.Trim();

                System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient();
                try
                {
                    //SmtpMail.Host = "172.16.216.251";
                    SmtpMail.Host = ConfigurationManager.AppSettings["MailServer1"].ToString();
                    SmtpMail.Send(insMail);
                    return true;
                }
                catch 
                {
                    try
                    {
                        SmtpMail.Host = ConfigurationManager.AppSettings["MailServer2"].ToString();
                        SmtpMail.Send(insMail);
                        return true;
                    }
                    catch
                    {
                        try
                        {
                            SmtpMail.Host = ConfigurationManager.AppSettings["MailServer3"].ToString();
                            SmtpMail.Send(insMail);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
