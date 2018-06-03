using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IServer
    {
        [OperationContract]
        byte[] vratiTrojku(string ime, string zem, int od, int to); //vraca vreme izmereno,prog,devijacija 

        [OperationContract]
        bool upisuBazu(string xml);

        //[OperationContract]
        //List<Trojka> vratiIzmereno(string zem, int od, int to);

    }
}
