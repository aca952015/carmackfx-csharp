using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client;
using CarmackFX.Client.Message;

namespace CarmackFX.ChatRoom
{
	[ServiceType(Type = ServiceType.Server)]
	public interface RoomService
	{
		Task<Boolean> Join();
	}
}
