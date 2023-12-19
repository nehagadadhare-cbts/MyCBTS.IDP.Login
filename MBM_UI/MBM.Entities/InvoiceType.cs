using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
	/// <summary>
	/// Sync the enum with the datastore for ease of use
	/// </summary>
	public enum InvoiceTypeEnum
	{
        MSU = 1,
        SOI = 2,
		US = 3,
		Canada = 4,
        GELD = 5, //CRQ:CRQ700001260001
        VD = 6,  //CRQ:CRQ700001314705
        Energy = 7
	}

    public class InvoiceType
	{        
		/// <summary>
		/// Identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Friendly name of the invoice type
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Default ISO 4217 Currency Code for importing files
		/// </summary>
		public string ImportCurrencyDefault { get; set; }

		/// <summary>
		/// Default ISO 4217 Currency Code for exporting the bill
		/// </summary>
		public string ExportCurrencyDefault { get; set; }

		/// <summary>
		/// Prefix used on invoice
		/// </summary>
		public string Prefix { get; set; }

		/// <summary>
		/// Billing Account Number (BAN) used on invoice
		/// </summary>
		public string BAN { get; set; }

		/// <summary>
		/// Name of the Billing Vendor ("CBTS" for US, "CBTS CA" for Canada)
		/// </summary>
		public string VendorName { get; set; }

        /// <summary>
        /// Stores Default FTP values
        /// </summary>
        public string DefaultFTP { get; set; }

        /// <summary>
        /// Stores Default FTP username
        /// </summary>
        public string FTPUserName { get; set; }

        /// <summary>
        /// Stores Default FTP password
        /// </summary>
        public string FTPPassword { get; set; }

        /// <summary>
        /// Stores CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Stores UpdatedBy
        /// </summary>
        public string UpdatedBy { get; set; }

        //Added for Automation

        /// <summary>
        /// Is Pre Billing Activated
        /// </summary>
        public bool IsAutoPreBilling { get; set; }

        /// <summary>
        /// When Automation Process Runs for Pre Biliing
        /// </summary>
        public int DaysBeforeBillCycle { get; set; }

        /// <summary>
        /// Is Post Billing Activated
        /// </summary>
        public bool IsAutoPostBilling { get; set; }

        /// <summary>
        /// Stores When Automation Process Runs for Post Biliing
        /// </summary>
        public int DaysAfterBillCycle { get; set; }

        /// <summary>
        /// Default 5 files will be exported
        /// </summary>
        public int OutputFileFormat { get; set; } //CBE_8967

        /// <summary>
        /// Identifies SOO accounts
        /// </summary>
        public int IsSOO { get; set; } //CBE_8967


        public string EmailAddress { get; set; }

        public bool EDI { get; set; }

        ///// <summary>
        ///// Stores Default SFTP values
        ///// </summary>
        //public string DefaultSFTP { get; set; }

        ///// <summary>
        ///// Stores Default SFTP UserName
        ///// </summary>
        //public string SFTPUserName { get; set; }

        ///// <summary>
        ///// Stores Default SFTP Password
        ///// </summary>
        //public string SFTPPassword { get; set; }



        /// <summary>
        /// Determines the billing system
        /// </summary>
        public string BillingSystem { get; set; } //SERO_1582

        public string invoiceId { get; set; } //SERO_1582

        public string iAutomationFrequency { get; set; } //SERO_1582

        public string ContractNumber { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string IndirectPartnerOrRepCode { get; set; }
        public string GLDepartmentCode { get; set; }
        public string IndirectAgentRegion { get; set; } 
	}

    //SERO-1582 STARTS

    public class Subscriber
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public int Id { get; set; }
        public string HierarchyId { get; set; }

      public SubscriberTypeDetails SubscriberTypeDetails { get; set; }
       
        
    }

    public class SubscriberTypeDetails
    {
        public int AccountingMethod { get; set; }
        public string BillCycle { get; set; }
        public int BillCycleDay { get; set; }
        public string InvoiceDisplay { get; set; }
        public Boolean IsReadOnly { get; set; }
        public int PaymentDueDays { get; set; }
        public string PostpaidAccountNumber { get; set; }
    }


    public class Hierarchy
    {
        public string HierarchyName { get; set; }
        public List<ParentHierarchyNode> HierarchyNodes { get; set; }

    }

    public class HierarchyNode 
    {
        public int Status { get; set; }
        public string Id { get; set; }
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
        public string NodeName { get; set; }
    }

    public class ParentHierarchyNode : HierarchyNode
    {
        public List<ChildHierarchyNode> HierarchyNodes { get; set; }

    }
    public class ChildHierarchyNode : HierarchyNode
    {
        public int ParentSubscriberId { get; set; }
        public Subscriber ParentSubscriber { get; set; }
     }



    public class PricingPlanIdentifier
    {
        public string Value { get; set; }
    }

    public class PricingPlanExternalReference
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class RateSheetItemAmount
    {
        public double NegotiatedRate { get; set; }
    }

    public class Items
    {
        public int Id { get; set; }
        public int ItemType { get; set; }
        public PricingPlanIdentifier PricingPlanIdentifier { get; set; }
        public List<PricingPlanExternalReference> PricingPlanExternalReferences { get; set; }
        public int ProductId { get; set; }
        public bool IsEditable { get; set; }
        public string BillerRuleConfigurationId { get; set; }
        public List<RateSheetItemAmount> RateSheetItemAmounts { get; set; }
        public DateTime StartDate { get; set; }
    }

    //public class Root
    //{
    //    public List<Item> Items { get; set; }
    //}



    //SERO-1582 ends

   #region Start Sero3511
    public class AscendonGLDepartmentCodes
    {
        public int    GLDepartmentID      { get; set; }
        public string GLDepartmentCode { get; set; }
        public string GLDepartmentName { get; set; }
        public string GLDepartmentValue { get; set; }
    }
    #endregion End Sero3511
}
