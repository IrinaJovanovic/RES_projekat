using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Server
{
    public class Service  : IServer
    {

        public byte[] vratiTrojku(string zem, int od, int to)
        {
            List<Trojke> ret = new List<Trojke>();
            List<Stavka> prog = new List<Stavka>();
            List<Stavka> izm = new List<Stavka>();

            prog = vratiPrognozirano(zem, od, to);
            izm = vratiIzmereno(zem, od, to);

            int i = 0;

            foreach(Stavka x in izm)
            {
                ret.Add(new Trojke(x.SAT, prog[i].LOAD, x.LOAD));
                i++;
            }

            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, ret);

            //This gives you the byte array.
            

            return mStream.ToArray();
        }
        public List<Stavka> vratiPrognozirano(string zem, int od, int to)
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
                if (listStavki.Stavke[i].SAT >= od && listStavki.Stavke[i].SAT <= to)
                {
                    ret.Add((Stavka)listStavki.Stavke[i]);
                }
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
        
        public List<Stavka> vratiIzmereno(string zem, int od, int to)
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
                if (listStavki.Stavke[i].SAT >= od && listStavki.Stavke[i].SAT <= to)
                {
                    ret.Add((Stavka)listStavki.Stavke[i]);
                }
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
