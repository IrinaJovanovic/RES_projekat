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
        public List<string> ucitano;         //lista u kojoj se nalaze imena ucitanih xml-ova

        private ListStavki prognoziraneUBazi;
        private ListStavki ostvareneUBazi;

        private static Baza instance = new Baza();  

        public static Baza Instance { get { return instance; } }

     

        public void UpisiPrognozirano(List<Stavka> stavke, string xml)
        {
            prognoziraneUBazi.Stavke.AddRange(stavke);
            upisiuCSV(xml);
            Serializuj(prognoziraneUBazi, "prognozirane.xml");
        }

        public void UpisiOstvareno(List<Stavka> stavke, string xml)
        {
            ostvareneUBazi.Stavke.AddRange(stavke);
            upisiuCSV(xml);
            Serializuj(ostvareneUBazi, "ostvarene.xml");
        }

        public void upisiuCSV(string xml)
        {
            ucitano.Add(xml);
            var csv = new StringBuilder();
            for(int i=0; i< ucitano.Count;i++)
            {
                csv.AppendLine(ucitano[i]);

            }
            File.WriteAllText("ucitano.csv", csv.ToString());
        }
        private void Serializuj(ListStavki lista, String imeFajla)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListStavki));
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\BazaPodataka\bin\Debug\bazaPodataka_" + imeFajla));
            using (TextWriter textWriter = new StreamWriter(path))
            {
                serializer.Serialize(textWriter, lista);
            }
        }
        private ListStavki DeSerijalizuj(String imeFajla)
        {
            ListStavki retVal = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ListStavki));
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\BazaPodataka\bin\Debug\bazaPodataka_" + imeFajla));
                FileStream fileStream = new FileStream(path, FileMode.Open);
                retVal = (ListStavki)(serializer.Deserialize(fileStream));

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return retVal;

        }
      
       private Baza()
        {

            LoadData();


        }                
        private void LoadData()
        {
            ucitano = new List<string>();
            string currentPath = Directory.GetCurrentDirectory();
            using (var reader = new StreamReader("ucitano.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();


                    ucitano.Add(line);

                }
            }

            prognoziraneUBazi = DeSerijalizuj("prognozirane.xml");
            ostvareneUBazi = DeSerijalizuj("ostvarene.xml");





        }

        public List<DataStatistic> VratiFiltriranoPodatke(string ime, String oblast, int datumOd, int datumDo)
        {
            List<Stavka> Prog = new List<Stavka>();
            List<Stavka> Ostv = new List<Stavka>();




            foreach (var prog in prognoziraneUBazi.Stavke)
            {

                if (prog.FAJLUCITAVANJA.Substring(4) == ime.Substring(4))
                {
                    if (prog.OBLAST == oblast)
                    {
                        if (prog.SAT >= datumOd && prog.SAT <= datumDo)
                        {
                            Prog.Add(prog);
                        }
                    }
                }
            }

            foreach (var ostv in ostvareneUBazi.Stavke)
                {
                    if (ostv.FAJLUCITAVANJA.Substring(4) == ime.Substring(4))
                    {
                        if (ostv.OBLAST == oblast)
                        {
                            if (ostv.SAT >= datumOd && ostv.SAT <= datumDo)
                            {
                                Ostv.Add(ostv);
                            }
                        }
                    }
                }

            
            if (Prog.Count != Ostv.Count)
                return null;

            return potrebniPodaci(Prog, Ostv);
        }

        private List<DataStatistic> potrebniPodaci(List<Stavka> prog, List<Stavka>ostv)
        {
            List<DataStatistic> ret = new List<DataStatistic>();
            int i = 0;
            foreach (var x in prog)
            {
                ret.Add(new DataStatistic(x.OBLAST, x.SAT, x.LOAD, ostv[i].LOAD));
                i++;

            }
            return ret;
        }
    }
}
