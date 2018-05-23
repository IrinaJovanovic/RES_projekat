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
        //konekcija sa bazom
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Vuk\\Documents\\Res_Projekat.mdf;Integrated Security=True;Connect Timeout=30");

        public byte[] vratiTrojku(string zem, int od, int to)
        {
            List<Trojke> ret = new List<Trojke>();
             //ucitavamo u bazu. mislim da i sa ove dve funkcije nesto ne valja posto su tabele prazne
            vratiPrognozirano();
            vratiIzmereno();
             //citamo iz baze, ono sto je u zagradi to je selekcija. mozda je drugacija sintaksa za ovaj sql u odnosu na oracleov, to bi trebalo proveriti
            SqlDataAdapter sda = new SqlDataAdapter("select p.Sat, p.Prognozirano, i.izm from Table p cross join Izmereno i where  p.Sat >=" + od.ToString() + "and p.Sat <=" + to.ToString() + "order by sat; ", con);

            DataTable dt = new DataTable();
            sda.Fill(dt);  //iscitano iz tabele ubacujemo u datatable

            //iz datatable upisujemo u listu koju vracamo
            foreach (DataRow dr in dt.Rows)
            {
                ret.Add(new Trojke(Int32.Parse(dr[0].ToString()), Int32.Parse(dr[1].ToString()), Int32.Parse(dr[2].ToString())));
            }
             //pretvaramo u niz bajtova i saljemo klijentu
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, ret);

            return mStream.ToArray();
        }
        public void vratiPrognozirano()
        {

            ListStavki listStavki = new ListStavki();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader("prog_2018_05_07.xml"))
            {
                object obj = deserializer.Deserialize(reader);            //citamo iz xml
                listStavki = (ListStavki)obj;
                Console.WriteLine();
            }

            if (listStavki.Stavke.Count > 25 || listStavki.Stavke.Count < 23)
            {
                return;

            }

            con.Open();
            string insert;    

            for (int i = 0; i < listStavki.Stavke.Count; i++)           //teoretski ubacujemo u tabele. i tu treba proveriti sintaksu za ubacivanje u tabele
            {
                //tu treba provera da li postoji
                insert = "insert into Table values('" + listStavki.Stavke[i].OBLAST + "','" + listStavki.Stavke[i].SAT + "','" + listStavki.Stavke[i].LOAD + "')";
                SqlCommand sc = new SqlCommand(insert,con);

            }

            con.Close();

        }  
        
        public void vratiIzmereno()
        {               //sve isto kao u prethodnoj funkciji samo drugi xml

            ListStavki listStavki = new ListStavki();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListStavki));
            using (TextReader reader = new StreamReader("ostv_2018_05_07.xml"))
            {
                object obj = deserializer.Deserialize(reader);
                listStavki = (ListStavki)obj;
                Console.WriteLine();
            }

            if (listStavki.Stavke.Count > 25 || listStavki.Stavke.Count < 23)
            {
                return;

            }

            con.Open();

            string insert;

            for (int i = 0; i < listStavki.Stavke.Count; i++)
            {
                //tu treba provera
                insert = "insert into Izmereno values('" + listStavki.Stavke[i].OBLAST + "','" + listStavki.Stavke[i].SAT + "','" + listStavki.Stavke[i].LOAD + "')";
                SqlCommand sc = new SqlCommand(insert, con);
            }

            con.Close();
        }
    }
}
