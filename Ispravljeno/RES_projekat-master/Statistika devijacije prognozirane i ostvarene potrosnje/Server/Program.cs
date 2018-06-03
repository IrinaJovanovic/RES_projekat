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
            //BazaPodataka.Baza b = new BazaPodataka.Baza();
            //b.load();

            ServiceHost svc = new ServiceHost(typeof(Service));
            svc.AddServiceEndpoint(typeof(IServer),
            new NetTcpBinding(),
            new Uri("net.tcp://localhost:81/IServer"));

            ServiceHost svc1 = new ServiceHost(typeof(Service));
            svc1.AddServiceEndpoint(typeof(IServer),
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
