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
        bool CreateFile(string fileName);
        [OperationContract]
        bool DeleteFile(string fileName);

        [OperationContract]
        string WriteInFile(string fileName, string content);

        [OperationContract]
        string ReadFromFile(string fileName);
        [OperationContract]
        void SendPerms(List<string[]> lista);
    }
}
