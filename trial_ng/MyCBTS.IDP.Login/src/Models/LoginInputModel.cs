using System.ComponentModel.DataAnnotations;

namespace MyCBTS.IDP.Login.Models
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}