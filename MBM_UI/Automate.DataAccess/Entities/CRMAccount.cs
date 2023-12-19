using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DataAccess.Entities
{
    public class CRMAccount
    {
        public int AccountNumber { get; set; }
        public int InvoiceTypeid { get; set; }
        public string AccountName { get; set; }
        public int? ParentAccountNumber { get; set; }
        public string ParentAccountName { get; set; }
        public string StringIdentifier { get; set; }
        public int Division { get; set; }
        public int SubDivision { get; set; }
        public string AccountStatus { get; set; }
        public bool? CreateAccount { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string InvoiceTypeName { get; set; }
        public string Prefix { get; set; }
        public int? AccountBillCycle { get; set; }
        public DateTime? AccountBillDate { get; set; }
    }
}
