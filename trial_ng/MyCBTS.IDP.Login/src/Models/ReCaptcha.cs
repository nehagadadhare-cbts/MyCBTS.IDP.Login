using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Models
{
    public class ReCaptcha
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
