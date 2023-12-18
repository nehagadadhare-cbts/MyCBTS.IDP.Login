using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Configuration
{
    public class AppConfiguration
    {
        public string DefaultBrand { get; set; }
        public string DefaultURI { get; set; }
        public string OnxURI { get; set; }
        public string ImpersonateLogout { get; set; }

        public string ZipInvCount { get; set; }

        public string ZipInvCookieExpiryDays { get; set; }

        public string ReCaptchaSecretKey { get; set; }
    }
}