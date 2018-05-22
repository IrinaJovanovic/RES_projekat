using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(Service));
            svc.AddServiceEndpoint(typeof(IServer),
            new NetTcpBinding(),
            new Uri("net.tcp://localhost:81/IServer"));

            svc.Open();
            Console.ReadLine();
            svc.Close();
        }
    }
}
