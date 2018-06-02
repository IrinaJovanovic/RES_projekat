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

            XmlSerializer deserializer = new XmlSerializer(typeof(ListTrojke));
            using (TextReader reader = new StreamReader("bazaPodataka.xml"))
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

        public void upisiuBazu()
        {
            List<Stavka> prog = new List<Stavka>();
            List<Stavka> izm = new List<Stavka>();
            ListTrojke upis = new ListTrojke();

            prog = vratiPrognozirano();
            izm = vratiIzmereno();

            int i = 0;
            foreach (Stavka x in izm)
            {
                upis.Add(new Trojke(x.OBLAST, x.SAT, prog[i].LOAD, x.LOAD));
                i++;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ListTrojke));
            using (TextWriter textWriter = new StreamWriter("bazaPodataka.xml"))
            {
                serializer.Serialize(textWriter, upis);
            }
        }
        public List<Stavka> vratiPrognozirano()
        {
            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();
 
            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader("prog_2018_05_07.xml"))
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

        public List<Stavka> vratiIzmereno()
        {

            List<Stavka> ret = new List<Stavka>();
            ListStavki listStavki = new ListStavki();
             
            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader("ostv_2018_05_07.xml"))
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
    }
}
