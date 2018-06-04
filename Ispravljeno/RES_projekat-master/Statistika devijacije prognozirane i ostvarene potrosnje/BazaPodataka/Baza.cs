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
    public class Baza //Ovo mora da budes Singleton
    {
        public List<string> ucitano;    //lista u kojoj su imena ucitanih xmlova... po

        private ListStavki prognoziraneUBazi;
        private ListStavki ostvareneUBazi;

        private static Baza instance = new Baza(); //bilo private 

        public static Baza Instance { get { return instance; } }

        /*public void upis(List<Stavka> lista, string xml)                 //upisuje u bazu podataka ucitani fajl, tako da ce ucitani xml u bazi izgledati
        {                                                                // "bazaPodataka_prog_yyyy_mm_dd.xml"
            

            ListStavki stavke = new ListStavki();
            stavke.Stavke = lista;

            ucitano.Add(xml);

            XmlSerializer serializer = new XmlSerializer(typeof(ListStavki));
            using (TextWriter textWriter = new StreamWriter("bazaPodataka_" + xml))
            {
                serializer.Serialize(textWriter, stavke);
            }

           
        }*/

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
            using (TextWriter textWriter = new StreamWriter("bazaPodataka_" + imeFajla))
            {
                serializer.Serialize(textWriter, lista);
            }
        }
        private void DeSerijalizuj(ListStavki lista, String imeFajla)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ListStavki));
                string path = Directory.GetCurrentDirectory() + "bazaPodataka_" + imeFajla;
                FileStream fileStream = new FileStream(path, FileMode.Open);
                lista = (ListStavki)(serializer.Deserialize(fileStream));
            }catch(Exception ex)
            {
             
            }

        }
       // private Baza()
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
            }//ucitava u listu vec ucitane xml-ove da ne bismo ucitali opet isti.. 

            prognoziraneUBazi = new ListStavki();
            
            DeSerijalizuj(prognoziraneUBazi, "prognozirane.xml");
            ostvareneUBazi = new ListStavki();
            DeSerijalizuj(ostvareneUBazi, "ostvarene.xml");




        }

        public List<Trojke> VratiFiltriranoPodatke(string ime, String oblast, int datumOd, int datumDo)
        {
            List<Stavka> Prog = new List<Stavka>();
            List<Stavka> Ostv = new List<Stavka>();




            foreach (var prog in prognoziraneUBazi.Stavke)
            {

                string s = prog.FAJLUCITAVANJA.Substring(4);
                string a = ime.Substring(4);

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
                //String[] splits = s.Split('_');
                //item.FAJLUCITAVANJA
                //TODO
                /*
                 * ako je datum ok
                 * ako je oblast ok
                 * 
                 * 
                 * */



                //retVal.Add(napraviTrojkuOdStavke(item));

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

            return napraviTrojkuOdStavke(Prog, Ostv);
        }

        private List<Trojke> napraviTrojkuOdStavke(List<Stavka> prog, List<Stavka>ostv)
        {
            List<Trojke> ret = new List<Trojke>();
            int i = 0;
            foreach (var x in prog)
            {
                ret.Add(new Trojke(x.OBLAST, x.SAT, x.LOAD, ostv[i].LOAD));
                i++;

            }
            return ret;
        }
    }
}
