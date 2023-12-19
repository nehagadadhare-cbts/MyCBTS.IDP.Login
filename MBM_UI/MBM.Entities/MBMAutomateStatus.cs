using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{   
    public class MBMAutomateStatus
    {
        public int ID { get; set; }
        public int InvoiceTypeId { get; set; }
        public int ImportCurrencyId { get; set; }
        public int ExportCurrenyId { get; set; }
        public int BillingMonth { get; set; }
        public int BillingYear { get; set; }
        public string InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public bool IsNewInvoice { get; set; }
        public bool IsInvoiceCreated { get; set; }
        public bool IsManualFileUpload { get; set; }
        public bool IsFileTypeUploaded { get; set; }
        public bool IsComparedToCRM { get; set; }
        public bool IsViwedChanged { get; set; }
        public bool IsApprovalMailSent { get; set; }
        public bool IsApprovedChange { get; set; }
        public bool IsProcessed { get; set; }
        public bool isPreBillingCompleted { get; set; }
        public bool IsApprovedByMail { get; set; }
        public bool IsSyncedCRM { get; set; }
        public bool IsImportCRM { get; set; }
        public bool IsProcessInvoice { get; set; }
        public bool IsExportInvoice { get; set; }
        public string LastAction { get; set; }
        public bool? isPreBillingOverriden { get; set; }
    }
}
