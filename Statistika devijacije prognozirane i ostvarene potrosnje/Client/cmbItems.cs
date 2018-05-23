using System;
using System.Collections.Generic;
using System.IO;
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
            using (var reader = new StreamReader("zemlje.csv"))
            {
                zem = new List<string>();
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                   // var values = line.Split(';');

                    zem.Add(line);
                    
                }
            }
            
            sat = new List<int>();
            for(int i = 1; i<=25; i++)
            {
                sat.Add(i);
            }
        }
    }
}
