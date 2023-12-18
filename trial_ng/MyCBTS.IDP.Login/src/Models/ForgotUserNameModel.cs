using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCBTS.IDP.Login.Models
{
    public class ForgotUserNameModel
    {
        public ForgotUserNameModel()
        {
            this.CustomErrors = new CustomResponse();
        }
        public CustomResponse CustomErrors { get; set; }
        [Required(ErrorMessage = "Please enter your account number.")]
        [MaxLength(13)]
        public string AccountNumber { get; set; }
        public List<string> UserNameList { get; set; }
    }
}