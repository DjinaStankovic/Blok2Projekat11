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
        public List<string[]> PermList = new List<string[]>();
        public string User = String.Empty;
        public readonly object LockObj = new object();

        public bool CreateFile(string fileName)
        {
            lock (LockObj)
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
                    Audit a = new Audit(Program.logName, Program.logSourceName);
                    a.CreateFailed(User);
                    return false;

                }
            }

        }
        

        public bool DeleteFile(string fileName)
        {
            lock (LockObj)
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
                    Audit a = new Audit(Program.logName, Program.logSourceName);
                    a.DeleteFailed(User);
                    return false;

                }
            }
        }

        public string ReadFromFile(string fileName)
        {
            lock (LockObj)
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
                    Audit a = new Audit(Program.logName, Program.logSourceName);
                    a.ReadFromFileFailed(User);
                    return "Neuspesno citanje";
                }
            }
        }
        
        public void SendUser(string user)
        {
            this.User = user;
            string[] names = null;
            string[] groups = null;
            string userCN = String.Format("CN={0}", user);
            List<X509Certificate2> certCollection = CertificationManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine);
            foreach (X509Certificate2 cert in certCollection)
            {
                names = cert.Subject.Split('_');
                if (names[0] == userCN)
                {
                    int size = names.Count() - 2;
                    groups = new string[size];
                    for (int i = 1; i < names.Count() - 1; i++)
                    {
                        groups[i - 1] = names[i];

                    }
                }

            }

            PermList.Clear();
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
            lock (LockObj)
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
                    Audit a = new Audit(Program.logName, Program.logSourceName);
                    a.WriteInFileFailed(User);
                    return false;
                }
            }
        }
       
        
    }
}
