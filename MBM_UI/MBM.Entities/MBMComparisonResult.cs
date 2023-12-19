using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
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

    //SERO-1582

    public class MBMComparisonResultCSG
    {
        public int? SnapShotId { get; set; }
        public int? InvoiceId { get; set; }
        //public string CRMAccountNumber { get; set; }
        public string AssetSearchCode1 { get; set; }
        public string AssetSearchCode2 { get; set; }
        public string ProfileName { get; set; }
        //public string FeatureCode { get; set; }
        //public int? GLCode { get; set; }
        public string OfferExternalRef { get; set; }
        public string ProductExternalRef { get; set; }
        public string PricingPlanExternalRef { get; set; }
        public string MBMUniqueID { get; set; }
        public string SubscriberId { get; set; }//SERO-1582

        public decimal? Charge { get; set; }
        public string ChargeType { get; set; }
        public string ActionType { get; set; }
        public int Action { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ItemType { get; set; }
        public string SubType { get; set; }
        public string SwitchId { get; set; }
        public string ItemId { get; set; }
        public string SequenceId { get; set; }
        public int? LoadId { get; set; }
        public string Processed { get; set; }
        //Sero-3511 Start 
        public string ProcessFirst { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string GlDepartmentCode { get; set; }
        public string IndirectAgentRegion { get; set; }
        public string IndirectPartnerCode { get; set; }
        //Sero-3511 End 
    }

    //service Attrib
    public class MBMServiceAttributes
    {
        public string ServiceAttributeExternalRef { get; set; }
        public string AttributeType { get; set; }
        public string ServiceAttributeValue { get; set; }
    }

    //service Attrib
    public class MBMServiceFeatures
    {
        public string ProductExternalRef { get; set; }
        public string PricingPlanExternalRef { get; set; }
    }
}
