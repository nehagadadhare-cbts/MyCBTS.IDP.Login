using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BillingEngine;
using MBM.Entities;
using System.Configuration;
using System.Security;

namespace MBM.Authenticate
{
    public class Authenticate
    {
        private MBM.BillingEngine.BillingEngine _billing;

        public Authenticate()
        {
        }

        public Users AuthenticateUser(string strConnectionString, string strLoggedUser)
        {
            //string strReturn = string.Empty;
            Users userDetails = new Users();
            UserBL objUserBL = new UserBL(strConnectionString);

            userDetails = objUserBL.AuthenticateUser(strLoggedUser);
                      
            return userDetails;
        }

        //public string Authenticate(string strConnectionString, string strLoggedUser)
        //{
        //    Users currentUser = new Users();
        //    string strLoginUser = string.Empty;
        //    //strLoginUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        //    //string strCon = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();

        //    if (!string.IsNullOrEmpty(strLoggedUser))
        //    {
        //        if (strLoggedUser.Contains("\\"))
        //        {
        //            int intIndex = strLoggedUser.IndexOf('\\');
        //            strLoginUser = strLoggedUser.Substring(0, intIndex) + "\\" + strLoggedUser.Substring(intIndex + 1, strLoggedUser.Length - intIndex - 1);

        //            _billing = new MBM.BillingEngine.BillingEngine(strConnectionString);
        //            currentUser = _billing.UserBL.GetUserDetails(strLoginUser);

        //            if (currentUser.IsActive)
        //            {
        //                //switch (currentUser.UserRole)
        //                //{
        //                //    case UserRoleType.Administrator:
        //                //        linkHome.Disabled = false;
        //                //        linkAbout.Disabled = false;
        //                //        linkAdmin.Disabled = false;
        //                //        break;

        //                //    case UserRoleType.Approver:
        //                //        linkHome.Disabled = false;
        //                //        linkAbout.Disabled = false;
        //                //        linkAdmin.Disabled = true;
        //                //        break;

        //                //    case UserRoleType.Processor:
        //                //        linkHome.Disabled = false;
        //                //        linkAbout.Disabled = false;
        //                //        linkAdmin.Disabled = true;
        //                //        break;
        //                //}

        //                //linkUserName.InnerText = currentUser.UserFirstName + " " + currentUser.UserLastName;
        //            }
        //            //else
        //            //{
        //            //    Server.Transfer("~/Admin/AccessDenied.aspx?Status=Inactive", true);
        //            //}
        //        }
        //    }
        //}
    }
}
