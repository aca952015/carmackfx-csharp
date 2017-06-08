using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Protocol
{
    class ProtocolService : IProtocolService
    {
        public ProtocolConfig Config { get; private set; }

        public ProtocolService()
        {
            this.Config = new ProtocolConfig();
        }
    }
}
