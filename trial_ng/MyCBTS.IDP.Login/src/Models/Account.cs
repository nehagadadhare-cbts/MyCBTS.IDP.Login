namespace MyCBTS.IDP.Login.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string BillingSystem { get; set; }
        public string AccountStatus { get; set; }
        public string AccountNickName { get; set; }
        public string DefaultAccount { get; set; }
    }
}