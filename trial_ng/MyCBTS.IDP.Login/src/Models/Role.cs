using Microsoft.AspNetCore.Identity;

namespace MyCBTS.IDP.Login.Models
{
    public class Role : IdentityRole
    {
        public string Adminster { get; set; }
    }
}