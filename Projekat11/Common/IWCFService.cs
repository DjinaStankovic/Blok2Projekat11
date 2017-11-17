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
        bool CreateFile(string path);
        [OperationContract]
        bool DeleteFile(string path);

        [OperationContract]
        bool WriteInFile(string path, string content);

        [OperationContract]
        bool ReadFromFile(string path);
    }
}
