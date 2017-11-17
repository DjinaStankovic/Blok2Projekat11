using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:27000/WCFService";

            ServiceHost host = new ServiceHost(typeof(WCFService));
            host.AddServiceEndpoint(typeof(IWCFService), binding, address);

            host.Open();
            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

            host.Close();
        }
    }
}
