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
            string input;
            string fileName;
            string content;
            using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
            {
                //ponuditi izbor klijentu
                while (true)
                {
                    Console.WriteLine("\n-------OPCIJE-------");
                    Console.WriteLine("1.Kreiranje fajla.");
                    Console.WriteLine("2.Brisanje fajla.");
                    Console.WriteLine("3.Citanje iz fajla.");
                    Console.WriteLine("4.Upis u fajl.");
                    Console.WriteLine("Vas izbor je: ");
                    input = Console.ReadLine();
                    Console.WriteLine("--------------------");

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
                            proxy.WriteInFile(fileName,content);
                            break;
                        default:
                            break;

                    }

                }

            }

           // Console.ReadLine();
        }
    }
}
