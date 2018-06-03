using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    [DataContract]
    public class Trojke
    {
        [DataMember]
        public string reg { get; set; }
        [DataMember]
        public int sat { get; set; }
        [DataMember]
        public int prog { get; set; } //prognozirano
        [DataMember]
        public int izm { get; set; } //izmereno
        [DataMember]
        public double dev { get; set; } //devijacija 

        public Trojke(string Reg, int Sat, int Prog, int Izm)
        {
            reg = Reg;
            sat = Sat;
            prog = Prog;
            izm = Izm;
            dev = devijacija(Izm, Prog);

        }

        public Trojke()
        {
        }
        
        double devijacija(int izm, int prog)
        {
             
            return ((double)(izm - prog)/(double)prog)*100;
        }

      
    }
}
