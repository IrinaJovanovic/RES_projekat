using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Trojke
    {
        public int sat { get; set; }
        public int prog { get; set; } //prognozirano
        public int izm { get; set; } //izmereno

        public double dev { get; set; } //devijacija 

        public Trojke(int Sat, int Prog, int Izm)
        {
            sat = Sat;
            prog = Prog;
            izm = Izm;
            dev = devijacija(Izm, Prog);

        }

        //sredi devijaciju 
        double devijacija(int izm, int prog)
        {

            return ((izm - prog)/prog)*100;
        }
    }
}
