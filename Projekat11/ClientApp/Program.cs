using SecurityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        public static List<string[]> permissions;
        
        static void Main(string[] args)
        {
          
            
            string input;
            string fileName;
            string content;
            permissions = new List<string[]>();
            Console.WriteLine("---Choose account---");
            Console.WriteLine("1. Client1");
            Console.WriteLine("2. Client2");
            Console.WriteLine("3. Client3");
            Console.WriteLine("-Choose:");
            int izbor = Convert.ToInt32(Console.ReadLine());
            string user = LoggedUser(izbor);
            Console.WriteLine("--------------------");

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:27000/WCFService";
            string[] names = null;
            string[] groups = null;

            List<X509Certificate2> certCollection = CertificationManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine);
            foreach (X509Certificate2 cert in certCollection)
            {
                names = cert.Subject.Split('_');
                if (names[0] == user)
                {
                    int size = names.Count() - 2;
                    groups = new string[size];
                    for (int i = 1; i < names.Count() - 1; i++)
                    {
                        groups[i - 1] = names[i];

                    }
                }

            }
            
            foreach(string gr in groups)
            {
                permissions.Add(RolesConfiguration.RolesConfig.GetPermissions(gr)); 
            }


            using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
            {
                
                while (true)
                {
                    Console.WriteLine("\n-------OPCIJE-------");
                    Console.WriteLine("1.Kreiranje fajla.");
                    Console.WriteLine("2.Brisanje fajla.");
                    Console.WriteLine("3.Upis u fajl.");
                    Console.WriteLine("4.Citanje iz fajla.");      
                    Console.WriteLine("-Vas izbor je: ");
                    input = Console.ReadLine();
                    Console.WriteLine("--------------------");

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("-Unesite naziv fajla koji zelite da kreirate: ");
                            fileName = Console.ReadLine();
                            proxy.CreateFile(fileName);
                            break;
                        case "2":
                            Console.WriteLine("-Unesite naziv fajla za brisanje: ");
                            fileName = Console.ReadLine();
                            proxy.DeleteFile(fileName);
                            break;
                        case "3":
                            bool file = false;
                            do
                            {
                                Console.WriteLine("-Unesite naziv fajla koji zelite da modifikujete: ");
                                fileName = Console.ReadLine();
                                file = FileExists(fileName);
                            } while (file == false);

                            if (file == true)
                            {
                                Console.WriteLine("-Unesite sadrzaj koji zelite da upisete u fajl: ");
                                content = Console.ReadLine();
                                proxy.WriteInFile(fileName, content);
                            }

                            break;
                        case "4":
                            Console.WriteLine("-Unesite naziv fajla koji zelite da procitate: ");
                            fileName = Console.ReadLine();
                            proxy.ReadFromFile(fileName);
                            break;
                        
                        default:
                            break;

                    }

                }

            }

           // Console.ReadLine();
        }
        public static string LoggedUser(int a)
        {
            string ret = String.Empty;
            switch (a)
            {
                case 1:
                    ret = "CN=wcfclient1";
                    break;
                case 2:
                    ret = "CN=wcfclient2";
                    break;
                case 3:
                    ret = "CN=wcfclient3";
                    break;
            }
            return ret;
        }

        public static bool FileExists(string fileName)
        {
            string path = @"C:\Files\" + fileName + ".txt";
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                Console.WriteLine(">> Fajl ne postoji!");
                return false;
            }
        }

    }
}
