using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class WCFClient: ChannelFactory<IWCFService>, IWCFService, IDisposable
    {
        IWCFService factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public bool CreateFile(string path)
        {
            bool allowed = false;
            factory.SendPerms(Program.permissions);

            try
            {
                allowed = factory.CreateFile(path);
                
                if (allowed == false)
                {
                    Console.WriteLine("Fajl sa tim nazivom vec postoji.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to CreateFile(). {0}", e.Message);
            }

            return allowed;
        }

        public bool DeleteFile(string path)
        {
            bool allowed = false;
            factory.SendPerms(Program.permissions);
            try
            {
                allowed = factory.DeleteFile(path);
                Console.WriteLine("DeleteFile() >> {0}", allowed);
                if (allowed == false)
                {
                    Console.WriteLine("Fajl sa tim nazivom ne postoji.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to DeleteFile(). {0}", e.Message);
            }

            return allowed;
        }

        public string ReadFromFile(string path)
        {
            string allowed = String.Empty;
            factory.SendPerms(Program.permissions);
            try
            {
                allowed = factory.ReadFromFile(path);
                Console.WriteLine("ReadFromFile() >> \n {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to ReadFromFile(). {0}", e.Message);
            }

            return allowed;
        }

        public string WriteInFile(string path, string content)
        {
            string allowed = String.Empty;
            factory.SendPerms(Program.permissions);
            try
            {
                allowed = factory.WriteInFile(path, content);
                Console.WriteLine("WriteInFile() >> {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to WriteInFile(). {0}", e.Message);
            }

            return allowed;
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public void SendPerms(List<string[]> lista)
        {
            
        }
    }
}
