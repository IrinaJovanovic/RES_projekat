using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    [XmlRoot("TROJKE_LOAD")]
    public class ListTrojke
    {
        
        private List<Trojke> trojke;
        [XmlElement("Trojke")]
        public List<Trojke> Trojke
        {
            get { return trojke; }
            set { trojke = value; }
        }

        public ListTrojke()
        {
            trojke = new List<Trojke>();
        }
<<<<<<< HEAD
        public void Add(Trojke trojke)
        {
            this.trojke.Add(trojke);
        }
=======
>>>>>>> e4a235354e6b0b927783313a3a1d44cc13795741
    }
}
