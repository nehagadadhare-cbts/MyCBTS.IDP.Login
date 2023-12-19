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
    
    public partial class mbm_Invoice
    {
        public int iInvoiceId { get; set; }
        public int iInvoiceTypeId { get; set; }
        public string sInvoiceNumber { get; set; }
        public int iBillingMonth { get; set; }
        public int iBillingYear { get; set; }
        public int iDefaultImportCurrencyID { get; set; }
        public string sStatus { get; set; }
        public string sLastAction { get; set; }
        public string sRecordType1_Status { get; set; }
        public Nullable<System.DateTime> dtRecordType1_DateTime { get; set; }
        public string sRecordType2_Status { get; set; }
        public Nullable<System.DateTime> dtRecordType2_DateTime { get; set; }
        public string sRecordType3_Status { get; set; }
        public Nullable<System.DateTime> dtRecordType3_DateTime { get; set; }
        public string sRecordType4_Status { get; set; }
        public Nullable<System.DateTime> dtRecordType4_DateTime { get; set; }
        public string sRecordType5_Status { get; set; }
        public Nullable<System.DateTime> dtRecordType5_DateTime { get; set; }
        public string sBillingFileExport_Status { get; set; }
        public Nullable<System.DateTime> dtBillingFileExport_DateTime { get; set; }
        public string sBillingFileExport_Path { get; set; }
        public int sExportCurrencyID { get; set; }
        public System.DateTime dtCreatedDate { get; set; }
        public string sCreatedBy { get; set; }
        public System.DateTime dtLastUpdatedDate { get; set; }
        public string sLastUpdatedBy { get; set; }
        public Nullable<int> iServiceDeskSnapshot_id { get; set; }
        public Nullable<System.DateTime> dtInvoiceBillPeriodStart { get; set; }
        public Nullable<System.DateTime> dtInvoiceBillPeriodEnd { get; set; }
        public bool IsDeleted { get; set; }
        public string sBillingSystem { get; set; }
    
        public virtual mbm_InvoiceTypes mbm_InvoiceTypes { get; set; }
    }
}
