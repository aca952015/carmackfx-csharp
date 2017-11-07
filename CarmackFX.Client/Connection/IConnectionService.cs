using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Message;

namespace CarmackFX.Client.Connection
{
    [Service(ServiceType.Connection)]
    public interface IConnectionService
    {
        ConnectionConfig Config { get; }
        void Connect();
        void Disconnect();
	    void Send(MessageIn msgIn);
    }
}
