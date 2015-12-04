using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MessageService
{
    [ServiceContract(Namespace = "MessageService")]
    public interface ITrafficMessage
    {
        [OperationContract]
        string GetServerName();

        [OperationContract]
        void SendMessage(string message, int adress);

        [OperationContract]
        string RetrieveMessage(int adress);
    }
}
