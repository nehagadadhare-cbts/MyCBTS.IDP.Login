using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class CUCDMData
    {
        public int Id { get; set; }
        public int SnapshotId { get; set; }
        public string SubIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AssetSearch { get; set; }
        public string ServiceProfileID { get; set; }
        public string LegalEntity { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }        
        public int? PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string DirectoryNumber { get; set; }
        public string MacAddress { get; set; }        
    }
}
