using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarmackFX.Client.Connection.Kcp
{
    abstract class Output
    {
       abstract public void output(ByteBuf msg, Kcp kcp, Object user);
    }
}
