using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Configuration
{
    public class CBTSServiceConfiguration
    {
        public string BaseURI { get; set; }
        public string Application { get; set; }
        public string AuthUser { get; set; }
        public string AuthPassword { get; set; }
    }
}
