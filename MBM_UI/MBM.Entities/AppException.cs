using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
   public class AppException
    {
        public string MethodName { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string LoggedInUser { get; set; }
        
    }
}
