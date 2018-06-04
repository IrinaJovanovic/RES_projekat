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
    public interface IService
    {
        [OperationContract]
        byte[] returnStatistic(string ime, string zem, int od, int to);  

        [OperationContract]
        bool upisuBazu(string xml);

      
    }
}
