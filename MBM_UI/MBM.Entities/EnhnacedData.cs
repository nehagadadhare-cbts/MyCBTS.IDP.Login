using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class EnhancedData
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
        public string FeatureCode { get; set; }
        public string ChargeCode { get; set; }
        public string Charge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //ER 8953
        public string e164_mask { get; set; }
        public string ActiveCharge { get; set; }
        public DateTime? EffectiveBillingDate { get; set; }
    }
}
