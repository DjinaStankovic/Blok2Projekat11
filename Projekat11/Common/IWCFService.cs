using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        bool CreateFile(string fileName,string user);
        [OperationContract]
        bool DeleteFile(string fileName,string user);

        [OperationContract]
        bool WriteInFile(string fileName, string content,string user);

        [OperationContract]
        string ReadFromFile(string fileName,string user);
        [OperationContract]
        void SendPerms(string user);

    }
}
