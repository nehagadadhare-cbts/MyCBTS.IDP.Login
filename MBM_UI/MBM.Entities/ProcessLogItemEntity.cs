using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBM.Entities;

namespace MBM.Entities
{
    public class ProcessLogItemEntity
    {
		public Int32 ID { get; set; }
		public int InvoiceID { get; set; }
		public InvoiceTypeEnum InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public string ProcessName { get; set; }
        public string ProcessResult { get; set; }
        public DateTime? ProcessDateTime { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
    }
}
