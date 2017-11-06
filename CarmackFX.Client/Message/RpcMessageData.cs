using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Message
{
    class RpcMessageData
    {
        public String ServiceName { get; set; }
        public String MethodName { get; set; }
        public String[] Arguments { get; set; }
    }
}
