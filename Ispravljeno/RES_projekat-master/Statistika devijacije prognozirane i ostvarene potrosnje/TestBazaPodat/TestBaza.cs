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
