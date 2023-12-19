using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class SOOReportData
    {
        public long? CRM_ACCOUNT { get; set; }
        public string TN { get; set; }
        public string PROFILE_TYPE { get; set; }
        public decimal? AMOUNT { get; set; }
        public string START_DTT { get; set; }
        public string END_DTT { get; set; }
        public string CUSTOMER_DEPARTMENT { get; set; }
        public string CUSTOMER_COST_CENTER { get; set; }
        public string CUSTOMER_ALI_CODE { get; set; }
        public string SUPERVISOR_NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public int QUANTITY { get; set; }
        public decimal? UNIT_PRICE { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
