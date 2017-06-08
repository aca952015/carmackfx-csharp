using CarmackFX.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Connection
{
    class ConnectionService : IConnectionService
    {
        public ConnectionConfig Config { get; private set; }

        public ConnectionService()
        {
            this.Config = new ConnectionConfig();
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {
        }
    }
}
