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
        public byte[] vratiTrojku(string zem, int od, int to)
        {
            ListTrojke temp = new ListTrojke();
            List<Trojke> ret = new List<Trojke>();
            upisiuBazu();
            XmlSerializer deserializer = new XmlSerializer(typeof(ListTrojke));
            using (TextReader reader = new StreamReader("bazaPodataka.xml"))
            {
                object obj = deserializer.Deserialize(reader);
                temp = (ListTrojke)obj;
                Console.WriteLine();
            }


            for (int i = 0; i < temp.Trojke.Count(); i++)
            {
                  if(temp.Trojke[i].sat >= od && temp.Trojke[i].sat <= to)
                {
                    ret.Add(new Trojke(temp.Trojke[i].sat, temp.Trojke[i].prog, temp.Trojke[i].izm));
                }
            }
          

            
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, ret);

            return mStream.ToArray();

            
        }

        public void upisiuBazu()
        {
            List<Stavka> prog = new List<Stavka>();
            List<Stavka> izm = new List<Stavka>();
<<<<<<< HEAD
            ListTrojke upis = new ListTrojke();
=======
            List<Trojke> upis = new List<Trojke>();
>>>>>>> e4a235354e6b0b927783313a3a1d44cc13795741

            prog = vratiPrognozirano();
            izm = vratiIzmereno();

            int i = 0;
            foreach (Stavka x in izm)
            {
                upis.Add(new Trojke(x.SAT, prog[i].LOAD, x.LOAD));
                i++;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ListTrojke));
            using (TextWriter textWriter = new StreamWriter("bazaPodataka.xml"))
            {
                serializer.Serialize(textWriter, upis);
            }
        }
        public List<Stavka> vratiPrognozirano(/*string zem, int od, int to*/)
        {
            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();
            //List<Stavka> trojke = new List<Stavka>();
            //Trojke trojke;
            //50$
            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader("prog_2018_05_07.xml"))
            {
                object obj = deserializer.Deserialize(reader);
                listStavki = (ListStavki)obj;
                Console.WriteLine();
            }


            for (int i = 0; i < listStavki.Stavke.Count; i++)
            {
                //if (listStavki.Stavke[i].SAT >= od && listStavki.Stavke[i].SAT <= to)
                //{
                    ret.Add((Stavka)listStavki.Stavke[i]);
                //}
            }
            //foreach(Stavka x in listStavki)
            //{
            //    if(x.OBLAST.Equals(zem))
            //    {
            //        if(x.SAT >= od && x.SAT <= to)
            //        {
            //            ret.Add(x);
            //        }
            //    }

            //}

            return ret;
        }

        public List<Stavka> vratiIzmereno(/*string zem, int od, int to*/)
        {

            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();
            //List<Stavka> trojke = new List<Stavka>();
            //Trojke trojke;
            //50$
            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader("ostv_2018_05_07.xml"))
            {
                object obj = deserializer.Deserialize(reader);
                listStavki = (ListStavki)obj;
                Console.WriteLine();
            }


            for (int i = 0; i < listStavki.Stavke.Count; i++)
            {
                //if (listStavki.Stavke[i].SAT >= od && listStavki.Stavke[i].SAT <= to)
                //{
                    ret.Add((Stavka)listStavki.Stavke[i]);
                //}
            }

            //List<Stavka> ret = new List<Stavka>();
            //List<Stavka> trojke = new List<Stavka>();

            //XmlSerializer deserializer = new XmlSerializer(typeof(List<Stavka>));
            //using (TextReader reader = new StreamReader("ostv_2018_05_07.xml"))
            //{
            //    object obj = deserializer.Deserialize(reader);
            //    trojke = (List<Stavka>)obj;

            //}

            //foreach (Stavka x in trojke)
            //{
            //    if (x.OBLAST.Equals(zem))
            //    {
            //        if (x.SAT >= od && x.SAT <= to)
            //        {
            //            ret.Add(x);
            //        }
            //    }

            //}

            return ret;
        }

    }
}
