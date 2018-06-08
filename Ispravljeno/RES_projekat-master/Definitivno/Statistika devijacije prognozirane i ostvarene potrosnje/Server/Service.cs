using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; 
using System.Data;
using System.Data.SqlClient;


namespace Server
{
    public class Service : IService
    {
     
        public byte[] returnStatistic(string ime , string zem, int od, int to)       
        {
            if(ime == null || zem == null)
                throw new ArgumentNullException();

            if(od <= 0 || to >= 25)
                throw new ArgumentOutOfRangeException();

            if (od > to)
                throw new ArgumentException();


            ListDataStatistic temp = new ListDataStatistic();
            List<DataStatistic> ret = new List<DataStatistic>();

            BazaPodataka.Baza b = BazaPodataka.Baza.Instance;

            ret = b.VratiFiltriranoPodatke(ime, zem, od, to);

            if (ret == null)
                return null;

                        
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, ret);

            return mStream.ToArray();

            
        }


        public bool upisuBazu(string xml)
        {
            BazaPodataka.Baza b = BazaPodataka.Baza.Instance; 
            int i = xml.Length - 19;
            string s = xml.Substring(i);         


                                                

            

            if (provera(s))                             
            {
                List<Stavka> stavke = ucitajXML(s);
                if(stavke==null)
                {
                   
                    return false;
                }
                if (prongozirana(s))
                {
                   
                    b.UpisiPrognozirano(stavke, s);

                }else
                {
                    b.UpisiOstvareno(stavke, s);
                }
                return true;
            }


            return false;
        }

        public bool prongozirana(string s)
        {
            if (s == null)
                throw  new ArgumentNullException();
            String[] splits = s.Split('_');
            if(splits[0].Equals("prog"))
            {
                return true;
            }
            return false;
        }

        public bool provera(string xml)                             //proveravamo da li se xml vec nalazi u bazi. ako ne onda vracamo true i ubacujemo ga u listu

        {                                                  
            BazaPodataka.Baza b = BazaPodataka.Baza.Instance;


            foreach (string s in b.ucitano)
                {
                    if (s.Equals(xml))
                    {

                        return false;
                    }
                }

           // b.upisiuCSV(xml);
            return true;
        }



        public List<Stavka> ucitajXML(string xml)               
        {
            string vreme = DateTime.Now.ToString("dd:MM:yyyy");


            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();
            

            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader(xml)) 
            {
                object obj = deserializer.Deserialize(reader);
                listStavki = (ListStavki)obj;
                Console.WriteLine();
            }
            if(!stavkeNisuKorektne(listStavki))
            {
                return null;
            }

            for (int i = 0; i < listStavki.Stavke.Count; i++)
            {
                Stavka stavka = listStavki.Stavke[i];
                stavka.FAJLUCITAVANJA = xml;
                stavka.VREMEUCITAVANJA = vreme;
                ret.Add(stavka);
            }
            return ret;
        }

       public bool stavkeNisuKorektne(ListStavki listStavki)
        {
            if(listStavki == null)
                throw  new ArgumentNullException();

            Dictionary<String, List<int>> stavkePoLokacijama = new Dictionary<string, List<int>>();

            if (listStavki.Stavke.Count < 23 || listStavki.Stavke.Count > 25)
                return false;

            foreach (var item in listStavki.Stavke)
            {
               
                List<int> stavkeNaLokaciji = new List<int>();
                if (stavkePoLokacijama.TryGetValue(item.OBLAST, out stavkeNaLokaciji))//
                {
                    if (stavkeNaLokaciji.Contains(item.SAT)) //Ako je vec unet sat za datu lokaciju proglasavamo da je fajl nekorektan
                    {
                        return false;
                    }
                    stavkeNaLokaciji.Add(item.SAT); //Ako je sve ok dodajemo novi sat u listu sati za tu oblast
                }
                else
                {
                    List<int> novaLista = new List<int>();
                    novaLista.Add(item.SAT);
                   
                }
            }

            return true;
       

        }      
    }
}
