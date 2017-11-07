using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Message
{
    public class MessageAttribute : Attribute
    {
        public MessageType MessageType { get; private set; }

        public MessageAttribute(MessageType type)
        {
            this.MessageType = type;
        }
    }
}
