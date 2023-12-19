using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automate.DataAccess.Entities;

namespace AutomateMBM.Entities
{
    public class ExceptionLogEntity
    {
        public int ID { get; set; }
        public int? InvoiceID { get; set; }
        public InvoiceTypeEnum? InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public string StackTrace { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
