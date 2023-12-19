using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DataAccess.Entities
{
    public static class Constants
    {
        public static string UNKNOWN { get { return "none"; } }
        public static string COMPLETED { get { return "Completed"; } }
        public static string FAILED { get { return "Failed"; } }
        public static string PROCESSING { get { return "In-Process"; } }
    }
}
