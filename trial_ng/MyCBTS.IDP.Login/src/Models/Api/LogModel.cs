namespace MyCBTS.IDP.Login.Models.Api
{
    public class LogModel
    {
        public string Application { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ServerName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string ServerAddress { get; set; } = string.Empty;
        public string RemoteAddress { get; set; } = string.Empty;
        public string Logger { get; set; } = string.Empty;
        public string Callsite { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
        public string BTN { get; set; } = string.Empty;
        public string UserId { get; set; } = "0";
        public string Client { get; set; } = string.Empty;
        public string Custom1 { get; set; } = string.Empty;
        public string Custom2 { get; set; } = string.Empty;
        public string Custom3 { get; set; } = string.Empty;
        public string Custom4 { get; set; } = string.Empty;
    }
}