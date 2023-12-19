using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateMBM.Entities
{
    public class ViewChangeCount
    {
        public int AddedBillItems { get; set; }

        public int AddedCharge { get; set; }

        public int UpdatedCharge { get; set; }

        public int AddedTelephone { get; set; }

        public int ChangedTelephone { get; set; }

        public int DeletedBillItems { get; set; }

        public int DeletedCharge { get; set; }

        public int DeletedTelephone { get; set; }

        //public int insert { get; set; }
        
        //public int update { get; set; }

        //public int change { get; set; }

        //public int cancel { get; set; }
    }
}
