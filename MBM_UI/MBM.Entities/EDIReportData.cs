using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class EDIReportData
    {
        public long AccountNumber { get; set; }
        public string FTPSite { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EDI { get; set; }
    }
}
