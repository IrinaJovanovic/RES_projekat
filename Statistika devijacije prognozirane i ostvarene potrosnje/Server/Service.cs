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

            XmlSerializer deserializer = new XmlSerializer(typeof(ListTrojke));
            using (TextReader reader = new StreamReader("bazaPodataka_"+ime))
            {
                object obj = deserializer.Deserialize(reader);
                temp = (ListTrojke)obj;
                Console.WriteLine();
            } 

            for (int i = 0; i < temp.Trojke.Count(); i++)
            {
                if (temp.Trojke[i].reg == zem)
                {
                    if (temp.Trojke[i].sat >= od && temp.Trojke[i].sat <= to)
                    {
                        ret.Add(new Trojke(temp.Trojke[i].reg, temp.Trojke[i].sat, temp.Trojke[i].prog, temp.Trojke[i].izm));
                    }
                }
            }   
                        
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, ret);

            return mStream.ToArray();

            
        }


        public bool upisuBazu(string xml)
        {
            BazaPodataka.Baza b = new BazaPodataka.Baza();
            string s = xml.Substring(123);          //ako ti ne radi tu podesi broj tako da ti s bude tipa "ostv_2018_05_07.xml"
                                                    //zato sto se xmlovi nalaze u folderu "XMLovi" u projektu
            if(provera(s))                          //meni je 123
            {
                b.upis(ucitajXML(s), s);
                return true;
            }


            return false;
        }

        bool provera(string xml)             //proveravamo da li se xml vec nalazi u bazi. ako ne onda vracamo true i ubacujemo ga u listu
        {                                    //a ako se nalazi vracamo false
            BazaPodataka.Baza b = new BazaPodataka.Baza();
           

                foreach (string s in b.ucitano)
                {
                    if (s.Equals(xml))
                    {

                        return false;
                    }
                }

            b.ucitano.Add(xml);
            return true;
        }


        List<Stavka> ucitajXML(string xml)               //ucitavanje xml-a
        {
            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader(xml))
            {
                object obj = deserializer.Deserialize(reader);
                listStavki = (ListStavki)obj;
                Console.WriteLine();
            }

            for (int i = 0; i < listStavki.Stavke.Count; i++)
            {

                ret.Add((Stavka)listStavki.Stavke[i]);
            }
            return ret;
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
