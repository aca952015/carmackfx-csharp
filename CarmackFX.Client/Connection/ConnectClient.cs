using CarmackFX.Client.Connection.Kcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Connection
{
	class ConnectClient : KcpClient
	{
		private ConnectionConfig config;

		public ConnectClient(ConnectionConfig config)
		{
			this.config = config;
		}

		public event EventHandler<byte[]> Received;

		protected override void HandleReceive(ByteBuf bb)
		{
			Received?.Invoke(this, bb.GetRaw());
		}

		public void Send(Byte[] buff)
		{
			this.Send(new ByteBuf(buff));
		}

		public void Connect()
		{
			string host = this.config.Host;
			IPHostEntry info = Dns.GetHostEntry(host);
			if(info.AddressList.Length > 0)
			{
				host = info.AddressList[0].ToString();
			}

			this.Connect(host, config.Port);
		}
	}
}
