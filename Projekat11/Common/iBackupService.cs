using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface iBackupService
    {
        [OperationContract]
        void LogChanged(string LogName, EventLogEntry entry);

    }
}
