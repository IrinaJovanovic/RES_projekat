using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            

            ServiceHost svc = new ServiceHost(typeof(Service));
            svc.AddServiceEndpoint(typeof(IService),
            new NetTcpBinding(),
            new Uri("net.tcp://localhost:81/IServer"));

            ServiceHost svc1 = new ServiceHost(typeof(Service));
            svc1.AddServiceEndpoint(typeof(IService),
            new NetTcpBinding(),
            new Uri("net.tcp://localhost:1235/IServer"));

            svc.Open();
            svc1.Open();

            Console.ReadLine();


            svc.Close();
            svc1.Close();
            
        }
    }
}
