//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Automate.DataAccess.edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class mbm_InvoiceTypes
    {
        public mbm_InvoiceTypes()
        {
            this.mbm_Invoice = new HashSet<mbm_Invoice>();
            this.MBMAutomateStatus = new HashSet<MBMAutomateStatu>();
        }
    
        public int iInvoiceTypeId { get; set; }
        public string sInvoiceTypeName { get; set; }
        public string sPrefix { get; set; }
        public string sBAN { get; set; }
        public string sVendorName { get; set; }
        public string sImportCurrencyDefault { get; set; }
        public string sExportCurrencyDefault { get; set; }
        public string sDefaultFTP { get; set; }
        public string sFTPUsername { get; set; }
        public string sFTPPassword { get; set; }
        public Nullable<System.DateTime> dtBillCycleStartDate { get; set; }
        public Nullable<System.DateTime> dtBillCycleEndDate { get; set; }
        public string sCreatedBy { get; set; }
        public Nullable<System.DateTime> dtCreatedDate { get; set; }
        public string sUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdatedDate { get; set; }
        public Nullable<bool> bIsAutoPreBill { get; set; }
        public Nullable<int> iDaysBeforeBillCycle { get; set; }
        public Nullable<bool> bIsAutoPostBill { get; set; }
        public Nullable<int> iDaysAfterBillCycle { get; set; }
        public Nullable<int> iOutputFileFormat { get; set; }
       
        public string iAutomationFrequency { get; set; }
        public string sBillingSystem { get; set; } //SERO-1582
    
        public virtual ICollection<mbm_Invoice> mbm_Invoice { get; set; }
        public virtual ICollection<MBMAutomateStatu> MBMAutomateStatus { get; set; }
    }
}
