namespace MyCBTS.IDP.Login.Models.Api
{
    public class RefreshToken
    {
        public string Key { get; set; }
        public int TokenType { get; set; }
        public string ClientId { get; set; }
        public string SubjectId { get; set; }
        public System.DateTime CreationTime { get; set; }
        public int Lifetime { get; set; }
        public string GrantType { get; set; }
        public string JsonCode { get; set; }
    }
}