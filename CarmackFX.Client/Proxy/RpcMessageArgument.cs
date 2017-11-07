using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Proxy
{
    class RpcMessageArgument
    {
        public string ArgumentName { get; set; }
        public string ArgumentValue { get; set; }
        public bool IsValueType { get; set; }
    }
}
