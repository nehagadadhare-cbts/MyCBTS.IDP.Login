using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    /// <summary>
    /// Profile details
    /// </summary>
    public class Profile
    {

        /// <summary>
        /// Profile Identifier
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Profile Name
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Invoice Type
        /// </summary>
        public int InvoiceType { get; set; }

        /// <summary>
        /// Profile Description
        /// </summary>
        public string Description { get; set; }

        //SERO-1582

        public string ChargeStringIdentifier { get; set; }

        public int CatalogItemId { get; set; }


    }

    /// <summary>
    /// Profile charges
    /// </summary>
    public class ProfileCharge
    {
        /// <summary>
        /// Profile Id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// profile charge identifier
        /// </summary>
        public int ChargeId { get; set; }

        /// <summary>
        /// Profile name
        /// </summary>
        public string ProfileName { get; set; }

        public int? GLCode { get; set; }

        public string Feature { get; set; }
        /// <summary>
        /// profile charge description
        /// </summary>
        public string ChargeDescription { get; set; }

        /// <summary>
        /// Profile Charge Amount
        /// </summary>
        public decimal? ChargeAmount { get; set; }




        /// <summary>
        /// Invoice type id
        /// </summary>
        public int InvoiceTypeId { get; set; }

        //SERO-1582
        /// <summary>
        /// Profile Identifier
        /// </summary>
        public string ChargeStringIdentifier { get; set; }
        public int ProductId { get; set; }
        public string BillerRuleConfigurationId { get; set; }
        public string PricingPlanId { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        //PC
        public int CatalogItemId { get; set; }

        //public DateTime? EffectiveStartDate { get; set; }
        public bool? bIsQuorumSynced { get; set; }



    }

    public class ManualCharge
    {
        public string Subidentifier { get; set; }
        public string LegalEntity { get; set; }
        public string AssetSearch { get; set; }
        public string ServiceProfile { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DirectoryNumber { get; set; }
        public string ServiceProfileUId { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }

    }
    //for excel
    public class ManualCharge_Stage
    {
        public string Subidentifier { get; set; }
        [HeaderDisplay("ProvisioningIdentifier (LegalEntity)*")]
        public string LegalEntity { get; set; }
        [HeaderDisplay("AssetSearch (UserID)*")]
        public string AssetSearch { get; set; }
        [HeaderDisplay("ServiceProfile ID (ChargeDescription)*")]
        public string ServiceProfile { get; set; }
        [HeaderDisplay("StartDate*")]
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DirectoryNumber { get; set; }
        public string ServiceProfileUId { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }

    }
    //for excel
    public class ManualCharge_Success
    {
        public string Subidentifier { get; set; }
        [HeaderDisplay("ProvisioningIdentifier (LegalEntity)*")]
        public string LegalEntity { get; set; }
        [HeaderDisplay("AssetSearch (UserID)*")]
        public string AssetSearch { get; set; }
        [HeaderDisplay("ServiceProfile ID (ChargeDescription)*")]
        public string ServiceProfile { get; set; }
        [HeaderDisplay("StartDate*")]
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DirectoryNumber { get; set; }
        public string ServiceProfileUId { get; set; }
    }

    public class ManualChargeFileDetails
    {
        public int MCharge_FileId { get; set; }
        public int InvoiceTypeId { get; set; }
        public string MCharge_FileName { get; set; }
        public string MCharge_filepath { get; set; }
        public int? MCharge_file_RecordCount { get; set; }
        public string MCharge_FileStatus { get; set; }
        public System.DateTime MCharge_Imported { get; set; }
        public string MCharge_ImportedBy { get; set; }
    }

    public enum DownloadFileType
    {
        SuccessRecords,
        RecordsByFileid
    }

    public class ManualChargeFileSummary
    {
        public int FileId { get; set; }
        public int SuccessRecords { get; set; }
        public int FailedRecords { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.Class |
                   System.AttributeTargets.Struct |
                   System.AttributeTargets.Property)]
    public class HeaderDisplay : System.Attribute
    {
        private string name;

        public HeaderDisplay(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
