using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyCBTS.IDP.Login.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            this.CustomErrors = new CustomResponse();
        }
        public int UserId { get; set; }

        [MaxLength(13)]
        [Required(ErrorMessage = "Please enter your account number.")]
        public string AccountNumber { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your account nickname.")]
        public string AccountNickName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(20, ErrorMessage = "The Password you entered is too short. Please enter a password that is at least 8 characters long. ", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please re-enter your password.")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The Reentered Password is too short. Please enter a password that is at least 8 characters long.", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Re-enter Password field should match with Password")]
        public string ReEnterPassword { get; set; }

        public string PhoneNumber { get
            {

                return strPhoneNumber != null ? strPhoneNumber.Replace("-", "") : strPhoneNumber;
            }
            set 
            {
                this.strPhoneNumber = this.PhoneNumber != null ? Regex.Replace(this.PhoneNumber, @"(\w{3})(\w{3})(\w{4})",@"$1-$2-$3") : this.PhoneNumber;
            }
        }

        [MaxLength(20)]
        public string InvoiceNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal TotalAmountDue { get; set; }

        [MaxLength(12)]
        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string strPhoneNumber { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your title.")]
        public string Title { get; set; }
        public string BrandName { get; set; }
        public string LastBill { get; set; }
        public string BillingSystem { get; set; }
        public string AccountStatus { get; set; }
        public RegisterFlow RegisterStep { get; set; }

        public enum RegisterFlow
        {
            RegistrationStep1 = 0,
            RegistrationStep2 = 1,
            RegistrationContinue = 2
        }      
        public CustomResponse CustomErrors { get; set; }

        public enum BillSystemEnum
        {
            Cris = 0,
            Macrocell = 1,
            Cabs = 2,
            Cbad = 3,
            Cbts = 4,
			AScendon = 5,
            None = 6,
        }

        public enum TypeOfCustomer
        {
            Residential = 0,
            Business = 1,
        }
    }
}