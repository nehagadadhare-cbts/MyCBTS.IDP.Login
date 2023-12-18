using System;

namespace MyCBTS.IDP.Login.Models
{
    public class LoginViewModel : LoginInputModel
    {
        public LoginViewModel()
        {
            this.CustomErrors = new CustomResponse();
        }
        public CustomResponse CustomErrors { get; set; }
        public Boolean AllowRememberLogin { get; set; }
        public string ReturnUrl { get; set; }
        public string ApplicationName { get; set; }        
    }
}