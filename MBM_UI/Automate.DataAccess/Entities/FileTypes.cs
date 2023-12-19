using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DataAccess.Entities
{
    public class FileTypes
    {
        public int FileTypeId { get; set; }
        public string FileType { get; set; }
        public string FileDescription { get; set; }
        public int InvoiceTypeId { get; set; }
        public int AssociatedFileId { get; set; }
        public string CreatedBy { get; set; }
    }
}
