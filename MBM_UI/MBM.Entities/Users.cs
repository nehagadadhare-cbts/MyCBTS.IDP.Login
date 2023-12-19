using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public enum UserRoleType
    {
        CustomerAdministrator = 1,
        Processor = 2,
        Approver = 3,
        SystemAdministrator = 4
    }

    public class Users
    {
        public const string INVALID_USER = "InvalidUser";
        public const string INACTIVE_USER = "InactiveUser";
        public const string CUSTOMER_ADMINISTRATOR_USER = "CustomerAdminUser";
        public const string PROCESSOR_USER = "ProcessUser";
        public const string APPROVER_USER = "ApproveUser";
        public const string SYSTEM_ADMINISTRATOR_USER = "SystemAdminUser";

        public int AssociatedUserId { get; set; }
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int UserRoleId { get; set; }
        public UserRoleType UserRole { get; set; }
        public bool IsActive { get; set; }        
    }

    public class ADUserInfo
    {
        public string CorpnetId { get; set; }
        public string DisplayName { get; set; }
        public string MailId { get; set; }
        public string FirstName { get; set; }
    }
}
