using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBM.Entities;

namespace MBM.Entities
{
    public class ApplicationLogEntity
    {
        public ApplicationLogType LogType { get; set; }
        public DateTime? ExceptionDateTime { get; set; }

		public int? InvoiceID { get; set; }
		public InvoiceTypeEnum? InvoiceType { get; set; }
		public string InvoiceNumber { get; set; }
        public string UserName { get; set; }
        public string CodeLocation { get; set; }
        public string Comment { get; set; }

        public Exception Exception
        {
            set
            {
                Message = value.Message;
                Source = value.Source;
                TargetSite = value.TargetSite.ToString();
                StackTrace = value.StackTrace;
            }
        }

        public string Message { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public string StackTrace { get; set; }

    }

    public enum ApplicationLogType
    {
        SystemRaised = 1,
        ApplicationRaised = 2,
		StoredProcedureRaised = 3,
    }
}
