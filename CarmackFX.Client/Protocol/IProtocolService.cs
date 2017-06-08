using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Protocol
{
    public interface IProtocolService
    {
        ProtocolConfig Config { get; }
    }
}
