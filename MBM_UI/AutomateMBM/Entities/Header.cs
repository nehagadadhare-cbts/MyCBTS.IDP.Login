using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateMBM.Entities
{
    public class HeaderDisplay : System.Attribute
    {
        private string name;

        public HeaderDisplay(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
    } 
}
