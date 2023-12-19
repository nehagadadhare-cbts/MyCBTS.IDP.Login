using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
    public class ConfigurationEntity
    {
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        
    }
}
