using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class MBMUploadFile
    {
        public int? InvoiceId { get; set; }
        public int? InvoiceTypeId { get; set; }
        public int? FileTypeId { get; set; }
        public string FileName { get; set; }
        public int? SnapshotId { get; set; }
        public int? UploadedFileId { get; set; }
        public string UploadedStatus { get; set; }
        public DateTime UploadedDate { get; set; }
        public string UploadedBy { get; set; }
    }
}
