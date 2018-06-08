using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BazaPodataka;
using Common;

namespace TestBazaPodat
{
    [TestFixture]
    public class TestBaza
    {

        [Test]
        [TestCase("SRB", 1, 3500, 3500)]
        public void proveraRacunaDevijacijeOk(string s, int i, int j, int k)
        {
            DataStatistic data = new DataStatistic(s, i, j, k);
            Assert.AreEqual((0), data.dev);
        }

        [Test]
        [TestCase("SRB", 1, 3500, 854)]
        public void proveraRacunaDevijacijeFail(string s, int i, int j, int k)
        {
            DataStatistic data = new DataStatistic(s, i, j, k);
            Assert.AreNotEqual(1, data.dev);
        }


        [Test]
        [TestCase(null, null)]
        [TestCase(null, "prog_2018_05_07.xml")]
        [ExpectedException(typeof(TypeInitializationException))]

        public void TestUpisiPrognoziranoNull(List<Stavka> asd, string s)
        {
            BazaPodataka.Baza b = Baza.Instance;

            b.UpisiPrognozirano(asd, s);
            

        }

        

        [Test]
        [ExpectedException(typeof(TypeInitializationException))]
        public void TestUpisiPrognoziranoOK()
        {
            BazaPodataka.Baza b = Baza.Instance;
            List<Stavka> stavke = null;
            b.UpisiPrognozirano(stavke, "prog_2018_05_07.xml");

        }
    }
}
