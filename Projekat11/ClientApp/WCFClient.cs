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

    }
}
