using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CarmackFX.Client.Message
{
	public class MessageIn
	{
		private const int offset = 21;

		public int Size { get; set; }
		public long Id { get; set; }
		public MessageType Type { get; set; }
		public long Token { get; set; }
		public String Data { get; set; }

		public MessageIn()
		{
		}

		public byte[] Build()
		{
			byte[] content = Data == null ? null : Encoding.UTF8.GetBytes(this.Data);
			byte[] data = new byte[(content?.Length ?? 0) + offset];

			this.Size = data.Length;

			ByteBuf bb = new ByteBuf(data);
			bb.WriteInt(this.Size);
			bb.WriteLong(this.Id);
			bb.WriteByte((byte)this.Type);
			bb.WriteLong(this.Token);
			bb.WriteBytes(content);

			return data;
		}
	}
}