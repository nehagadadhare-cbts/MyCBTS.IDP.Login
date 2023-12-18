using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Configuration
{
    public class ApiURIConfiguration
    {
        public string BaseURI { get; set; }
        public string Application { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }      
}
