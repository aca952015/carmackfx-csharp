using System;

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
