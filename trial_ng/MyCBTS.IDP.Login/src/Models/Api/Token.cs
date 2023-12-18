using System;
using System.Collections.Generic;

namespace MyCBTS.IDP.Login.Models.Api
{
    public class Token
    {
        public string Key { get; set; }
        public string TokenType { get; set; }
        public string ClientId { get; set; }
        public string SubjectId { get; set; }
        public System.DateTime Expiry { get; set; }
        public string JsonCode { get; set; }
        public string AuthCodeChallenge { get; set; }
        public string AuthCodeChallengeMethod { get; set; }
        public Nullable<bool> IsOpenId { get; set; }
        public string Nonce { get; set; }
        public string RedirectUri { get; set; }
        public string SessionId { get; set; }
        public Nullable<bool> WasConsentShown { get; set; }
        public int LifeTime { get; set; }

        public ICollection<string> AllowedSigningAlgorithms { get; set; }
    }
}