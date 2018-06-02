using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BazaPodataka
{
    public class Baza
    {
        public List<string> ucitano;    //lista u kojoj su imena ucitanih xmlova... po

        public void upis(List<Stavka> lista, string xml)                 //upisuje u bazu podataka ucitani fajl, tako da ce ucitani xml u bazi izgledati
        {                                                                // "bazaPodataka_prog_yyyy_mm_dd.xml"
            

            List<Stavka> stavke = new List<Stavka>();
            stavke = lista;

            ucitano.Add(xml);

            XmlSerializer serializer = new XmlSerializer(typeof(ListStavki));
            using (TextWriter textWriter = new StreamWriter("bazaPodataka_" + xml))
            {
                serializer.Serialize(textWriter, stavke);
            }

           
        }

        public void start()
        {
             ucitano = new List<string>();
            using (var reader = new StreamReader("ucitano.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();


                    ucitano.Add(line);

                }
            }
        }                //ucitava u listu vec ucitane xml-ove da ne bismo ucitali opet isti.. 

    }
}
