using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DataAccess.Entities
{
    [Serializable]
    public class MBMComparisonResult
    {
        public int? SnapShotId { get; set; }
        public int? InvoiceId { get; set; }
        public string CRMAccountNumber { get; set; }
        public string AssetSearchCode1 { get; set; }
        public string AssetSearchCode2 { get; set; }
        public string ProfileName { get; set; }
        public string FeatureCode { get; set; }
        public int? GLCode { get; set; }
        public decimal? Charge { get; set; }
        public string ChargeType { get; set; }
        public string ActionType { get; set; }
        public int Action { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ItemType { get; set; }
        public string SubType { get; set; }
        public string SwitchId { get; set; }
        public string ItemId { get; set; }
        public string SequenceId { get; set; }
        public int? LoadId { get; set; }
        public string Processed { get; set; }
    }
}
