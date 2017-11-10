using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client;
using CarmackFX.Client.Message;

namespace CarmackFX.ChatRoom
{
	[Service(ServiceType.Server)]
    [Message(MessageType.Internal)]
	public interface RoomService
	{
		ServiceTask Join();
		ServiceTask Chat(string msg);
	}
}
