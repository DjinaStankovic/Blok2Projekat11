using SecurityManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
       
        public static string user;
        static void Main(string[] args)
        {
           
            string srvCertCN = "wcfservice";
            string input;
            string fileName;
            string content;
            Console.WriteLine("Choose account:");
            Console.WriteLine("1. Client1");
            Console.WriteLine("2. Client2");
            Console.WriteLine("3. Client3");
            int izbor = Convert.ToInt32(Console.ReadLine());
            user = LoggedUser(izbor);

           

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string mach = "localhost";
            X509Certificate2 srvCert = CertificationManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri(String.Format("net.tcp://{0}:202/WCFService",mach)),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {

                while (true)
                {
                    Console.WriteLine("\n-------OPCIJE-------");
                    Console.WriteLine("1.Kreiranje fajla.");
                    Console.WriteLine("2.Brisanje fajla.");
                    Console.WriteLine("3.Citanje iz fajla.");
                    Console.WriteLine("4.Upis u fajl.");
                    Console.WriteLine("Vas izbor je: ");
                    input = Console.ReadLine();
                    Console.WriteLine("---------------------");
                    
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Unesite naziv fajla koji zelite da kreirate: ");
                            fileName = Console.ReadLine();
                            proxy.CreateFile(fileName);
                            break;
                        case "2":
                            Console.WriteLine("Unesite naziv fajla za brisanje: ");
                            fileName = Console.ReadLine();
                            proxy.DeleteFile(fileName);
                            break;
                        case "3":
                            Console.WriteLine("Unesite naziv fajla koji zelite da procitate: ");
                            fileName = Console.ReadLine();
                            proxy.ReadFromFile(fileName);
                            break;
                        case "4":
                            Console.WriteLine("Unesite naziv fajla koji zelite da modifikujete: ");
                            fileName = Console.ReadLine();
                            Console.WriteLine("Unesite sadrzaj koji zelite da upisete u fajl: ");
                            content = Console.ReadLine();
                            proxy.WriteInFile(fileName, content);
                            break;
                        default:
                            break;

                    }

                }

            }

           
        }
        public static string LoggedUser(int a)
        {
            string ret = String.Empty;
            switch (a)
            {
                case 1:
                    ret = "wcfclient1";
                    break;
                case 2:
                    ret = "wcfclient2";
                    break;
                case 3:
                    ret = "wcfclient3";
                    break;
            }
            return ret;
        }

        
    }
}
