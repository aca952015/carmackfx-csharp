using System;
using System.Collections.Generic;

namespace CarmackFX.Client.Proxy
{
	class RpcMessageData
    {
        public String ServiceName { get; set; }
        public String MethodName { get; set; }
        public IDictionary<String, RpcMessageArgument> Arguments { get; set; }
    }
}
