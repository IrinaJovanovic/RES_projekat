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
    public class Service : IServer
    {
     //   public List<string> ucitano = new List<string>();
        public byte[] vratiTrojku(string ime , string zem, int od, int to)        //OVO TREBA DA URADITE
        {
            ListTrojke temp = new ListTrojke();
            List<Trojke> ret = new List<Trojke>();

            BazaPodataka.Baza b = BazaPodataka.Baza.Instance;

            ret = b.VratiFiltriranoPodatke(ime, zem, od, to);

            if (ret == null)
                return null;

            //XmlSerializer deserializer = new XmlSerializer(typeof(ListTrojke));
            //using (TextReader reader = new StreamReader("bazaPodataka_"+ime))
            //{
            //    object obj = deserializer.Deserialize(reader);
            //    temp = (ListTrojke)obj;
            //    Console.WriteLine();
            //} 

            //for (int i = 0; i < temp.Trojke.Count(); i++)
            //{
            //    if (temp.Trojke[i].reg == zem)
            //    {
            //        if (temp.Trojke[i].sat >= od && temp.Trojke[i].sat <= to)
            //        {
            //            ret.Add(new Trojke(temp.Trojke[i].reg, temp.Trojke[i].sat, temp.Trojke[i].prog, temp.Trojke[i].izm));
            //        }
            //    }
            //}   
                        
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, ret);

            return mStream.ToArray();

            
        }


        public bool upisuBazu(string xml)
        {
            BazaPodataka.Baza b = BazaPodataka.Baza.Instance; //Ne moze se kreirati nova instanca baze svaki put, mora biti singleton
            int i = xml.Length - 19;
            string s = xml.Substring(i);          //Ovo ovako ne moze, moras da seces string po znaku "/" i onda gledas poslednji desni odsecak.


                                                    //ako ti ne radi tu podesi broj tako da ti s bude tipa "ostv_2018_05_07.xml"
                                                    //zato sto se xmlovi nalaze u folderu "XMLovi" u projektu
                                                    //meni je 123

            

            if (provera(s))                             
            {
                List<Stavka> stavke = ucitajXML(s);
                if(stavke==null)
                {
                    //Ovde je potrebno jos dodatnih obavestenja da su podaci u fajlu nekorektni
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

        private bool prongozirana(string s)
        {
            String[] splits = s.Split('_');
            if(splits[0].Equals("prog"))
            {
                return true;
            }
            return false;
        }

        bool provera(string xml)             //proveravamo da li se xml vec nalazi u bazi. ako ne onda vracamo true i ubacujemo ga u listu
        {                                    //a ako se nalazi vracamo false
            BazaPodataka.Baza b = BazaPodataka.Baza.Instance;//Ne moze se kreirati nova instanca baze svaki put, mora biti singleton


            foreach (string s in b.ucitano)
                {
                    if (s.Equals(xml))
                    {

                        return false;
                    }
                }

            b.upisiuCSV(xml);
            return true;
        }



        List<Stavka> ucitajXML(string xml)               //ucitavanje xml-a
        {
            string vreme = DateTime.Now.ToString("dd:MM:yyyy");


            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();
            //string path = "\\XMLovi\\" + xml;

            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader(xml)) //Ovde ce mozda pucati zbog pronalazenja fajla, ne znam zasigurno dok se ne proba #HB
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

        private bool stavkeNisuKorektne(ListStavki listStavki)
        {
            

            Dictionary<String, List<int>> stavkePoLokacijama = new Dictionary<string, List<int>>();

            if (listStavki.Stavke.Count < 23 || listStavki.Stavke.Count > 25)
                return false;

            foreach (var item in listStavki.Stavke)
            {
               
                List<int> stavkeNaLokaciji = new List<int>();
                if (stavkePoLokacijama.TryGetValue(item.OBLAST, out stavkeNaLokaciji))//Kupimo sve unete sate za lokaciju
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
                    //  stavkePoLokacijama.Add(item.OBLAST, novaLista); // U slucaju da oblast uopste nije postojala u Dictionary do sada dodajemo je
                }
            }

            return true;


         

        }





        //public List<Stavka> vratiPrognozirano()
        //{
        //    List<Stavka> ret = new List<Stavka>();
        //    ListStavki listStavki = new ListStavki();

        //    XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
        //    using (TextReader reader = new StreamReader("prog_2018_05_07.xml"))
        //    {
        //        object obj = deserializer.Deserialize(reader);
        //        listStavki = (ListStavki)obj;
        //        Console.WriteLine();
        //    } 

        //    for (int i = 0; i < listStavki.Stavke.Count; i++)
        //    {

        //            ret.Add((Stavka)listStavki.Stavke[i]); 
        //    }
        //    return ret;
        //}

        //public List<Stavka> vratiIzmereno()
        //{

        //    List<Stavka> ret = new List<Stavka>();
        //    ListStavki listStavki = new ListStavki();

        //    XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
        //    using (TextReader reader = new StreamReader("ostv_2018_05_07.xml"))
        //    {
        //        object obj = deserializer.Deserialize(reader);
        //        listStavki = (ListStavki)obj;
        //        Console.WriteLine();
        //    }


        //    for (int i = 0; i < listStavki.Stavke.Count; i++)
        //    {

        //            ret.Add((Stavka)listStavki.Stavke[i]);

        //    }
        //    return ret;
        //}     
    }
}
