using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    [DataContract]
    public class Stavka
    {
        private string reg;
        private int sat;
        private int load; //ucitana vrednost 
        private string fajlUcitavanja;
        private string vremeUcitavanja;

        public Stavka()
        {


        }

        public Stavka(string s, int i, int j)
        {
            reg = s;
            sat = i;
            load = j;
            fajlUcitavanja = "";
            vremeUcitavanja = "";

        }

        #region prop
        [DataMember]
        public int SAT
        {
            get { return sat; }
            set { sat = value; }
        }
        [DataMember]
        public int LOAD //ucitano vreme 
        {
            get { return load; } 
            set { load = value; }
        }
        [DataMember]
        public string OBLAST
        {
            get { return reg; }
            set { reg = value; }
        }
        [DataMember]
        public string FAJLUCITAVANJA //ucitano vreme 
        {
            get { return fajlUcitavanja; }
            set { fajlUcitavanja = value; }
        }
        [DataMember]
        public string VREMEUCITAVANJA
        {
            get { return vremeUcitavanja; }
            set { vremeUcitavanja = value; }
        }


        #endregion
    }
}
