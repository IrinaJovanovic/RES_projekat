using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class cmbItems
    {
        public List<string> zem { get; set; }
        public List<int> sat { get; set; }

        public cmbItems()
        {
            zem = new List<string>()
            {
                "SRB",
                "BiH",
                "CRO"
            };

            sat = new List<int>();
            for(int i = 1; i<=25; i++)
            {
                sat.Add(i);
            }
        }
    }
}
