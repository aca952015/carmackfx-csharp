using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Message;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Callback;
using CarmackFX.Client.Error;

namespace CarmackFX.Client.Connection
{
	class ConnectionService : IConnectionService
	{
		private UdpSocket client = null;

		public ConnectionConfig Config { get; private set; }

		private DateTime lastHeartbeat;


		public ConnectionService()
		{
			this.Config = new ConnectionConfig();
		}

		public void Connect()
		{
			Task.Run(new Action(() =>
			{
				// 创建一个实例
				client = new UdpSocket((byte[] buf) =>
				{
					try
					{
						MessageOut msgOut = MessageOut.Parse(buf);
						if (msgOut != null)
						{
							if (msgOut.Mode == MessageMode.Result)
							{
								MessageManager.Completed(msgOut);
							}
							else if (msgOut.Mode == MessageMode.Callback)
							{
								CallbackManager.Callback(msgOut);
							}
						}
					}
					catch (Exception ex)
					{
						ServiceManager.OnError(ex);
					}
				});

				// 绑定端口
				client.Connect(this.Config.Host, this.Config.Port);

				lastHeartbeat = DateTime.Now;

				while (true)
				{
					if (client != null && client.IsOpen())
					{
						client.Update();

						SendHeartbeat();
					}

					System.Threading.Thread.Sleep(10);
				}
			}));
		}

		private void SendHeartbeat()
		{
			// heartbeat
			if ((DateTime.Now - lastHeartbeat).TotalSeconds > 2)
			{
				MessageIn messageIn = new MessageIn();
				messageIn.Id = -1;
				messageIn.Type = MessageType.Heartbeat;

				Send(messageIn);

				lastHeartbeat = DateTime.Now;
			}
		}

		public void Disconnect()
		{
			try
			{
				client.Close();
			}
			catch (Exception e)
			{
			}

			client = null;
		}

		public void Send(MessageIn msgIn)
		{
			try
			{
				if (client != null && client.IsOpen())
				{
					client.Send(msgIn.Build());
				}
			}
			catch (Exception ex)
			{
				ServiceManager.OnError(ex);
			}
		}
	}
}
