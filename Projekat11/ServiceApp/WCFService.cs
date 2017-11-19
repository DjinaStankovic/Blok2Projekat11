using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        public bool CreateFile(string fileName)
        {
            string path = @"C:\Files\"+fileName+".txt";
            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Fajl sa tim nazivom vec postoji.\n");
                    return false;
                }else
                {
                    using(FileStream fs = File.Create(path))
                    {
                        Console.WriteLine("Uspjesno ste kreirali fajl na lokaciji: " + path);
                        return true;
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
           
        }

        public bool DeleteFile(string fileName)
        {
            string path = @"C:\Files\" + fileName + ".txt";
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Uspjesno ste obrisali fajl na lokaciji "+path);
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

        public string ReadFromFile(string fileName)
        {
            string path = @"C:\Files\" + fileName + ".txt";
            try
            {
                if (File.Exists(path))
                {
                    var content = string.Empty;
                    Console.WriteLine("Uspjesno iscitavanje fajla.");
                   using(StreamReader reader=new StreamReader(path))
                    {
                        content = reader.ReadToEnd();
                        reader.Close();  
                    }
                    return content;
                }
                else
                {
                    Console.WriteLine("Fajl sa tim nazivom ne postoji.");
                    return "Fajl sa tim nazivom ne postoji.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }

        }

        public string WriteInFile(string fileName, string content)
        {
            string path = @"C:\Files\" + fileName + ".txt";
            try
            {
                if (File.Exists(path))
                {
                   
                    Console.WriteLine("Uspjesno pisanje u fajl.");
                    
                    File.AppendAllText(path," "+content);
                    return "Uspjesno pisanje u fajl.";
                }
                else
                {
                    Console.WriteLine("Fajl sa tim nazivom ne postoji.");
                    return "Fajl sa tim nazivom ne postoji.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }






        }
    }
}
