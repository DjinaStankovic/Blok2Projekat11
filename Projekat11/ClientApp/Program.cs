using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:27000/WCFService";

            using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
            {
                //proxy.CreateFile("aaaa");

            }

            Console.ReadLine();
        }
    }
}
