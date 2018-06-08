﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;

namespace Client
{
    public class cmbItems
    {
        public List<string> zem { get; set; }
        public List<int> sat  { get; set; }

        public List<string> datumi {get; set; }

        public cmbItems()
        {
            using (var reader = new StreamReader("zemlje.csv"))
            {
                zem = new List<string>();
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                   

                    zem.Add(line);
                    
                }
            }
            
            sat = new List<int>();
            for(int i = 1; i<=25; i++)
            {
                sat.Add(i);
            }

            
            BazaPodataka.Baza b = BazaPodataka.Baza.Instance;
            datumi = b.ucitano;
        }
    }
}
