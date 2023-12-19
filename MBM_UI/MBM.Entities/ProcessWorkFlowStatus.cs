using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class ProcessWorkFlowStatus
    {
        public string InvoiceNumber { get; set; }
        public Boolean CompareToCRM { get; set; }
        public Boolean ViewChange { get; set; }
        public Boolean ApproveChange { get; set; }
        public Boolean SyncCRM { get; set; }
        public Boolean ImportCRMData { get; set; }
        public Boolean ProcessInvoice { get; set; }
        public Boolean ExportInvoiceFile { get; set; }        
    }

    public enum ProcessWorkFlowButtons
    {
        CompareToCRM = 1,
        ViewChange,
        ApproveChange,
        SyncCRM,
        ImportCRMData,
        ProcessInvoice,
        ExportInvoiceFile        
    }
}
