using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    [XmlRoot("PROGNOZIRANI_LOAD")]
    public class ListStavki
    {
        private List<Stavka> stavke;
        [XmlElement("STAVKA")]
        public List<Stavka> Stavke { get => stavke; set => stavke = value; }

        public ListStavki()
        {
            stavke = new List<Stavka>();
        }
    }
}
