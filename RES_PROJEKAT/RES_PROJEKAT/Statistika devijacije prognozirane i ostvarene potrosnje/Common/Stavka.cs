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

        public Stavka()
        {

        }
        #region prop
        [DataMember]
        public int SAT
        {
            get { return sat; }
            set { sat = value; }
        }
        [DataMember]
        public int LOAD
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

        #endregion
    }
}
