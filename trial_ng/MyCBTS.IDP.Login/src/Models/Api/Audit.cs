namespace MyCBTS.IDP.Login.Models.Api
{
    public class Audit
    {
        public int SubjectId { get; set; }
        public string ClientId { get; set; }
        public string Application { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
        public string Method { get; set; }
        public string Step { get; set; }
        public string TokenType { get; set; }
        public string TokenStorageType { get; set; }
        public string Result { get; set; }
        public string Token { get; set; }
        public string Comments { get; set; }
        public string Url { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
    }
}