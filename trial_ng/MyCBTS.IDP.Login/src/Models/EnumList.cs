using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Models
{
    public class EnumList
    {
        public enum BillingSystem
        {
            CBAD,
            CBTS,
            CBTSCA,
            CBTSUK,
            CRIS,
            CABS,
            ASCENDON
        }

        public enum BrandName
        {
            ONX,
            CBTS
        }
    }
}

