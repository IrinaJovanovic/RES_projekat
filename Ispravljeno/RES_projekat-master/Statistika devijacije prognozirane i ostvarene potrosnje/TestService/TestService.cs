using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Server;
using Common;

namespace TestService
{
    [TestFixture]
    public class TestService
    {
        [Test]
        [TestCase("prog_2018_05_07.xml", "SRB", 1, 5)]
        [TestCase("prog_2017_03_08.xml", "BIH", 8, 23)]
        [TestCase("prog_2017_02_14.xml", "SRB", 10, 15)]   
        [TestCase("prog_2017_05_07.xml", "SRB", 1, 5)]
        [TestCase("prog_2017_05_07.xml", "SRB", 5, 7)]

        public void returnStaticOk(string s, string s1, int i, int j)
        {
            Service returnStatic = new Service();
            returnStatic.returnStatistic(s, s1, i, j);
        }

        [Test]
        [TestCase("prog_2017_05_07.xml", "SRB", 7, 62)]
        [TestCase("prog_2017_03_08.xml", "BIH", 2, 2333)]
        [TestCase("prog_2017_32_14.xml", "SRB", 10, 35)]
        [TestCase("prog_2017_05_07.xml", "MKN", 1, 32)]
        [TestCase("prog_2017_05_07.xml", "SRB", 0, 7)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void returnStaticFail(string s, string s1, int i, int j)
        {
            Service returnStatic = new Service();
            returnStatic.returnStatistic(s, s1, i, j);
        }

        [Test]
        [TestCase(null, "SRB", 3, 6)]
        [TestCase("prog_2017_05_07.xml", null, 2, 6)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void returnStaticNull(string s, string s1, int i, int j)
        {
            Service proba = new Service();
            proba.returnStatistic(s, s1, i, j);
        }


        [Test]

        [TestCase("prog_2017_03_01.xml")]   //ok
        [TestCase("ostv_2016_12_11.xml")]
        [TestCase("prog_2018_24_07.xml")]
        [TestCase("prog_2016_05_38.xml")]
        [TestCase("ostv_2018_87_07.xml")]
        [TestCase("ostv_2005_11_12.xml")]
       //[ExpectedException(typeof(TypeInitializationException))]
        public void upisuBazuOk(string xml)
        {
            Service upisu = new Service();
            upisu.upisuBazu(xml);

        }

        [Test]
        [TestCase("prog_2018_05_07.xml")]      //losi
        [TestCase("prog_2018_24_07.xml")]
        [TestCase("prog_2016_05_38.xml")]
        [TestCase("ostv_2018_87_07.xml")]
        [TestCase("izme_2014_05_01.xml")]
        public void upisuBazuFail(string xml)
        {
            Service upisu = new Service();
            upisu.upisuBazu(xml);

        }

        [Test]
        [TestCase("prog_2018_01_06.xml")]  //ok
        [TestCase("prog_2018_05_13.xml")]
        [TestCase("prog_2018_12_11.xml")]
        public void prongoziranaTrue(string s)       //VIDI KAKO 
        {
            Service prog = new Service();
            prog.prongozirana(s);

        }

        [Test]
        [TestCase("_prog_01)01.xml")]
        [TestCase("meda_2018_01_07.xml")]
        [TestCase("ostv_2014_01_01.xml")]
        [TestCase("voodo_01_2011.xml")]

        public void prongoziranaFalse(string s)       //VIDI KAKO 
        {
            Service prog = new Service();
            prog.prongozirana(s);

        }


        [Test]
        [TestCase(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void prognoziranaNull(string s)
        {
            Service prog = new Service();
            prog.prongozirana(s);
        }

        [Test]
        [TestCase("ostv_2018_05_07.xml")]

        public void proveraTrue(string s)
        {
            Service service = new Service();
            var upit = service.provera(s);
            Assert.IsFalse(upit);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ucitajXMLnull()
        {
            Service service = new Service();
            service.ucitajXML(null);
        }

        [Test]

        public void stavkeNisuKorektneFalse()
        {
            Service service = new Service();
            ListStavki list = new ListStavki();
            for (int i = 0; i < 26; i++)
            {
                list.Stavke.Add(new Stavka());
            }

            service.stavkeNisuKorektne(list);
        }

        [Test]
        [TestCase("prog_2018_05_07.xml")]
        public void ucitajXMLOK( string s)
        {
            Service ser = new Service();
            ser.ucitajXML(s);
        }

        [Test]
        public void stavkeNisuKorektneTrue()
        {
            Service service = new Service();
            ListStavki list = new ListStavki();
            for (int i = 0; i < 24; i++)
            {
                list.Stavke.Add(new Stavka("SRB", i, i+100));
            }

            service.stavkeNisuKorektne(list);
        }
    }
}
