using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Models
{
    public class BillSummary
    {
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public System.DateTime BillDate { get; set; }
        public decimal TotalAmountDue { get; set; }
    }
}
