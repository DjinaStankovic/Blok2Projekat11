﻿using Common;
using ServiceApp;
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
            factory.SendPerms(Program.user);

            try
            {
                allowed = factory.CreateFile(path);
                Console.WriteLine("CreateFile() >> {0}", allowed);
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
            factory.SendPerms(Program.user);
            try
            {
                allowed = factory.DeleteFile(path);
                Console.WriteLine("DeleteFile() >> {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to DeleteFile(). {0}", e.Message);
            }

            return allowed;
        }

        public bool WriteInFile(string path, string content)
        {
            string allowed = String.Empty;
            //  factory.SendPerms(Program.permissions);
            factory.SendPerms(Program.user);
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

        public string ReadFromFile(string path)
        {
            bool allowed = false;
            factory.SendPerms(Program.user);
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

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public void SendPerms(string user)
        {

        }

    }
}
