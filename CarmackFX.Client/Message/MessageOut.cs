using System;

namespace CarmackFX.Client.Message
{
	public class MessageOut
	{
		public int Size { get; set; }
		public long Id { get; set; }
		public MessageSuccess Success { get; set; }
		public String Data { get; set; }

		public static MessageOut Parse(byte[] buff)
		{
			try
			{
				MessageOut msgOut = new MessageOut();
				ByteBuf bb = new ByteBuf(buff);

				msgOut.Size = bb.ReadInt();
				msgOut.Id = bb.ReadLong();
				msgOut.Success = (MessageSuccess)bb.ReadByte();
				if (msgOut.Success == MessageSuccess.SUCCESS)
				{
					msgOut.Data = bb.ReadString();
				}

				return msgOut;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
