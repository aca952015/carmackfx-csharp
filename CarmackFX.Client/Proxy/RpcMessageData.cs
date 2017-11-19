using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Proxy
{
    class RpcMessageData
    {
        public String ServiceName { get; set; }
        public String MethodName { get; set; }
        public IDictionary<String, RpcMessageArgument> Arguments { get; set; }
    }
}
