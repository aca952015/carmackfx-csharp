using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Connection
{
    [ServiceType(Type = ServiceType.Connection)]
    public interface IConnectionService
    {
        ConnectionConfig Config { get; }
        void Connect();
        void Disconnect();
    }
}
