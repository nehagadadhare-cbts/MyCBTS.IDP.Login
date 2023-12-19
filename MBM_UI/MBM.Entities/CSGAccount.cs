using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class CSGAccount
    {
        public long AccountNumber { get; set; }
        public int InvoiceTypeid { get; set; }
        public string AccountName { get; set; }
        public long? ParentAccountNumber { get; set; }
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
        public string AccountBillCycle { get; set; }
        public DateTime? AccountBillDate { get; set; }
        public DateTime? EffectiveBillDate { get; set; }   //ER8668
        public string EffectiveBillingDate { get; set; }   //ER8668
        public bool ChildPays { get; set; }
        public string FTP { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public bool EDI { get; set; }
        public int SubcriberNumber { get; set; }
        public string DisplayAccountNumber { get; set; }
    }

    public enum CSGValidationCode
    {
        Success=0,
        ParentAccountNotAvailableInMBM = 1,
        ParentChildBillCycyleNotMatched=2,
        DuplicationOfAccountNumber=3,
        DuplicationOfStringIdentifier =4
    }
}
