using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyCBTS.IDP.Login.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string BrandName { get; set; }
        public List<Account> Accounts { get; set; }  
    }
}