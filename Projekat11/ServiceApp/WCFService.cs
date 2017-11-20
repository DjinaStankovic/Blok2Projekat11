using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        public static List<string[]> PermList = new List<string[]>();
        

        public bool CreateFile(string fileName)
        {

            
            if (CheckRole(RolesConfiguration.Permissions.CreateFile.ToString()))
            {
                string path = @"C:\Files\" + fileName + ".txt";
                try
                {
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Fajl sa tim nazivom vec postoji.\n");
                        return false;
                    }
                    else
                    {
                        using (FileStream fs = File.Create(path))
                        {
                            Console.WriteLine("Uspjesno ste kreirali fajl na lokaciji: " + path);
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            else
            {
                Audit.CreateFailed(Thread.CurrentPrincipal.Identity.Name);
                return false;
            }
           
        }

        public bool DeleteFile(string fileName)
        {
            if (CheckRole(RolesConfiguration.Permissions.DeleteFile.ToString()))
            {
                string path = @"C:\Files\" + fileName + ".txt";
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        Console.WriteLine("Uspjesno ste obrisali fajl na lokaciji " + path);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Fajl sa tim nazivom ne postoji.\n");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            else
            {
                Audit.DeleteFailed(Thread.CurrentPrincipal.Identity.Name);
                return false;

            }
        }

        public string ReadFromFile(string fileName)
        {
            if (CheckRole(RolesConfiguration.Permissions.ReadFile.ToString()))
            {
                string path = @"C:\Files\" + fileName + ".txt";
                try
                {
                    if (File.Exists(path))
                    {
                        var content = string.Empty;
                        Console.WriteLine("Uspjesno iscitavanje fajla.");
                        using (StreamReader reader = new StreamReader(path))
                        {
                            content = reader.ReadToEnd();
                            reader.Close();
                        }
                        return content;
                    }
                    else
                    {
                        Console.WriteLine("Fajl sa tim nazivom ne postoji.");
                        return "Neuspjesno citanje iz fajla!.";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return e.ToString();
                }
            }
            else
            {
                Audit.ReadFromFileFailed(Thread.CurrentPrincipal.Identity.Name);
                return "Neuspesno citanje";
            }

        }
        
        public void SendPerms(string user)
        {
            string[] names = null;
            string[] groups = null;
            // PermList = lista;
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

            foreach (string gr in groups)
            {
                PermList.Add(RolesConfiguration.RolesConfig.GetPermissions(gr));
            }
        }

        public bool CheckRole(string role)
        {
            foreach(string[] gr in PermList)
            {
                foreach(string a in gr)
                {
                    if(a==role)
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        public bool WriteInFile(string fileName, string content)
        {
            if (CheckRole(RolesConfiguration.Permissions.WriteInFile.ToString()))
            {

                string path = @"C:\Files\" + fileName + ".txt";
                try
                {
                    if (File.Exists(path))
                    {

                        Console.WriteLine("Uspjesno pisanje u fajl.");

                        File.AppendAllText(path, " " + content);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Fajl sa tim nazivom ne postoji.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }

            }
            else
            {
                Audit.WriteInFileFailed(Thread.CurrentPrincipal.Identity.Name);
                return false;
            }
        }
       
        
    }
}
