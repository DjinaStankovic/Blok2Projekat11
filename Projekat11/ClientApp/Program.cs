using SecurityManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            string fileName;
            string content = null;
            int choice = 0;
            int input = 0;
            bool account = false;
            bool options = false;

            do
            {
                do
                {
                    Console.WriteLine("\n----Choose account-------");
                    Console.WriteLine("1. Client1(admins group)");
                    Console.WriteLine("2. Client2(writers group)");
                    Console.WriteLine("3. Client3(readers group)");
                    Console.WriteLine("-Your choice: ");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());

                        if (choice < 1 || choice > 3)
                            Console.WriteLine(">> Enter only the numbers: 1, 2 or 3 !");

                        user = LoggedUser(choice);
                        account = true;
                    }
                    catch
                    {
                        Console.WriteLine(">> Error! You need to enter the number!");
                        account = false;
                    }

                    Console.WriteLine("------------------------------------------");
                } while (choice < 1 || choice > 3);     
            } while (!account);    

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string machine = "localhost";
            X509Certificate2 srvCert = CertificationManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri(String.Format("net.tcp://{0}:202/WCFService", machine)),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {

                while (true)
                {
                    do
                    {
                        Console.WriteLine("\n----Options-------");
                        Console.WriteLine("1. Create file.");
                        Console.WriteLine("2. Write in file.");
                        Console.WriteLine("3. Read from file.");
                        Console.WriteLine("4. Delete file.");
                        Console.WriteLine("-Your choice: ");
                        try
                        {
                            input = Convert.ToInt32(Console.ReadLine());

                            if (input < 1 || input > 4)
                                Console.WriteLine(">> Enter only the numbers: 1, 2, 3 or 4 !");

                            Console.WriteLine("------------------------------------------------");

                            switch (input)
                            {
                                case 1:
                                    Console.WriteLine("-Enter the name of the file you want to create: ");
                                    fileName = Console.ReadLine();
                                    proxy.CreateFile(fileName);
                                    break;
                                case 2:
                                    bool fileExists = false;
                                    Console.WriteLine("-Enter the name of the file you want to modify: ");
                                    fileName = Console.ReadLine();
                                    fileExists = FileExists(fileName);
                                    if (fileExists)
                                    {
                                        Console.WriteLine("-Enter content: ");
                                        content = Console.ReadLine();
                                      
                                    }
                                    proxy.WriteInFile(fileName, content);
                                    break;
                                case 3:
                                    Console.WriteLine("-Enter the name of the file you want to read: ");
                                    fileName = Console.ReadLine();
                                    proxy.ReadFromFile(fileName);
                                    break;
                                case 4:
                                    Console.WriteLine("-Enter the name of the file you want to delete: ");
                                    fileName = Console.ReadLine();
                                    proxy.DeleteFile(fileName);
                                    break;
                                default:
                                    break;
                            }

                            options = true;
                        }
                        catch
                        {
                            Console.WriteLine(">> Error! You need to enter the number!");
                            options = false;
                        }

                        Console.WriteLine("------------------------------------------------");
                    } while (!options);
                    
                }
            }
       
        }

        public static string LoggedUser(int selectedUser)
        {
            string user = String.Empty;
            switch (selectedUser)
            {
                case 1:
                    user = "wcfclient1";
                    break;
                case 2:
                    user = "wcfclient2";
                    break;
                case 3:
                    user = "wcfclient3";
                    break;
            }
            return user;
        }

        public static bool FileExists(string fileName)
        {
            string path = @"C:\Files\" + fileName + ".txt";
            if (!File.Exists(path))
            {
                Console.WriteLine(">> File doesn't exist!");
                return false;
            } 
            else
            {
                return true;
            }
        }
        
    }
}
