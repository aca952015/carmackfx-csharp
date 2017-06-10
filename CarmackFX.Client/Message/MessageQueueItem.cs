using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Message
{
	class MessageQueueItem
	{
		public long Id { get; set; }
		public MessageOut Result { get; set; }
	}
}
