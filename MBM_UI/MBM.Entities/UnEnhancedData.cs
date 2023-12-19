using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class UnEnhancedData
    {
        public string Customer { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SnapshotId { get; set; }
        public string SubIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryUniqueIdentifier { get; set; }
        public string SecondaryUniqueIdentifier { get; set; }        
        public string ServiceProfile { get; set; }
        public string LegalEntity { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        //ER 8953
        public string e164_mask { get; set; }
        public string ActiveCharge { get; set; }
        public DateTime? EffectiveBillingDate { get; set; }
    }
}
