using System;
using System.Linq;
using System.Text;

namespace CarmackFX.Client.Message
{
	class ByteBuf
	{
		private readonly byte[] buff;
		private int buffIndex;

		public ByteBuf(byte[] buff)
		{
			this.buff = buff;
			this.buffIndex = 0;
		}

		private byte[] GetData(int readLength)
		{
			byte[] data = new byte[readLength];

			for (int pos = 0; pos < readLength; pos++, buffIndex++)
			{
				data[pos] = this.buff[buffIndex];
			}

			return data;
		}

		private void SetData(byte[] data)
		{
			for (int pos = 0; pos < data.Length; pos++, buffIndex++)
			{
				this.buff[buffIndex] = data[pos];
			}
		}

		public long ReadShort()
		{
			byte[] data = GetData(2);
			return BitConverter.ToInt16(data.Reverse().ToArray(), 0);
		}

		public int ReadInt()
		{
			byte[] data = GetData(4);
			return BitConverter.ToInt32(data.Reverse().ToArray(), 0);
		}

		public long ReadLong()
		{
			byte[] data = GetData(8);
			return BitConverter.ToInt64(data.Reverse().ToArray(), 0);
		}

		public long ReadByte()
		{
			byte[] data = GetData(1);
			return data[0];
		}

		public string ReadString(int readLength)
		{
			byte[] data = GetData(readLength);
			return Encoding.UTF8.GetString(data);
		}

		public string ReadString()
		{
			byte[] data = GetData(this.buff.Length - buffIndex);
			return Encoding.UTF8.GetString(data);
		}

		public void WriteShort(short val)
		{
			SetData(BitConverter.GetBytes(val).Reverse().ToArray());
		}

		public void WriteInt(int val)
		{
			SetData(BitConverter.GetBytes(val).Reverse().ToArray());
		}

		public void WriteLong(long val)
		{
			SetData(BitConverter.GetBytes(val).Reverse().ToArray());
		}

		public void WriteByte(byte val)
		{
			SetData(new byte[] { val });
		}

		public void WriteString(String val, int fixedLength)
		{
			if (val == null)
			{
				val = string.Empty;
			}

			if (val.Length < fixedLength)
			{
				val += new string(' ', fixedLength - val.Length);
			}

			WriteString(val);
		}

		public void WriteString(string val)
		{
			SetData(Encoding.UTF8.GetBytes(val));
		}

		public void WriteBytes(byte[] content)
		{
			SetData(content);
		}
	}
}
