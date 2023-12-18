using System.ComponentModel.DataAnnotations;

namespace MyCBTS.IDP.Login.Models
{
    public class ForgotPasswordModel
    {
        public ForgotPasswordModel()
        {
            this.CustomErrors = new CustomResponse();
        }
        public CustomResponse CustomErrors { get; set; }

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(20, ErrorMessage = "The Password you entered is too short. Please enter a password that is at least 8 characters long. ", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please re-enter your password.")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The Reentered Password is too short. Please enter a password that is at least 8 characters long.", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Re-enter Password field should match with Password")]
        public string ReEnterPassword { get; set; }

        public bool VerfiedResetPwdEmail { get; set; }
        public bool PasswordReset { get; set; }
        public int UserId { get; set; }
        public string BrandName { get; set; }
        public string token { get; set; }
    }
}