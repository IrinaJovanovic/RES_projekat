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
        [TestCase("prog_2017_05_07", "SRB", 1, 5)]
        [TestCase("prog_2017_03_08", "BIH", 8, 23)]
        [TestCase("prog_2017_02_14", "SRB", 10, 15)]   //losi
        [TestCase("prog_2017_05_07", "SRB", 1, 5)]
        [TestCase("prog_2017_05_07", "SRB", 5, 7)]


        public void returnStaticOk(string s, string s1, int i, int j)
        {
            Service returnStatic = new Service();
            returnStatic.returnStatistic(s, s1, i, j);
        }

        [Test]
        [TestCase("prog_2017_05_07", "SRB", 7, 5)]
        [TestCase("prog_2017_03_08", "BIH", 25, 2)]
        [TestCase("prog_2017_32_14", "SRB", 10, 35)]
        [TestCase("prog_2017_05_07", "MKN", 1, 32)]
        [TestCase("prog_2017_05_07", "SRB", 32, 7)]


        public void returnStaticFail(string s, string s1, int i, int j)
        {
            Service returnStatic = new Service();
            returnStatic.returnStatistic(s, s1, i, j);
        }

        [Test]

        [TestCase("prog_2017_03_01")]   //ok
        [TestCase("ostv_2016_12_11")]
        public void upisuBazuOk(string xml)
        {
            Service upisu = new Service();
            upisu.upisuBazu(xml);

        }

        [Test]
        [TestCase("prog_20_05_07")]      //losi
        [TestCase("prog_2018_24_07")]
        [TestCase("prog_2016_05_38")]
        [TestCase("ostv_2018_87_07")]
        [TestCase("izme_2014_05_01")]
        public void upisuBazuFail(string xml)
        {
            Service upisu = new Service();
            upisu.upisuBazu(xml);

        }

        [Test]
        [TestCase("prog_2018_01_06")]  //ok
        [TestCase("prog_2018_05_13")]
        [TestCase("prog_2018_12_11")]
        public void prongoziranaTrue(string s)       //VIDI KAKO 
        {
            Service prog = new Service();
            prog.prongozirana(s);

        }

        [Test]
        [TestCase("_prog_01)01")]
        [TestCase("meda_2018_01_07")]
        [TestCase("ostv_2014_01_01")]
        [TestCase("voodo_01_2011")]

        public void prongoziranaFalse(string s)       //VIDI KAKO 
        {
            Service prog = new Service();
            prog.prongozirana(s);

        }


        [Test]
        [TestCase(null)]

        public void prognoziranaNull(string s)
        {
            Service prog = new Service();
            prog.prongozirana(s);
        }

        [Test]
        [TestCase("")]

        public void proveraTrue(string s)
        {
            Service service = new Service();
            service.provera(s);
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
        public void stavkeNisuKorektneTrue()
        {
            Service service = new Service();
            ListStavki list = new ListStavki();
            for (int i = 0; i < 24; i++)
            {
                list.Stavke.Add(new Stavka());
            }

            service.stavkeNisuKorektne(list);
        }
    }
}
