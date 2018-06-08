using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    [XmlRoot("TROJKE_LOAD")]
    public class ListDataStatistic
    {
        
        private List<DataStatistic> data;
        [XmlElement("Trojke")]
        public List<DataStatistic> Trojke
        {
            get { return data; }
            set { data = value; }
        }

        public ListDataStatistic()
        {
            data = new List<DataStatistic>();
        }

        public void Add(DataStatistic data)
        {
            this.data.Add(data);
        }

    }
}
