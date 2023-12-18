namespace MyCBTS.IDP.Login.Models.Request
{
    public class UserLoginRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ApplicationName { get; set; }
    }
}