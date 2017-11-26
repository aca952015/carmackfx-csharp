using System;
using System.Threading.Tasks;
using CarmackFX.Client.Message;
using CarmackFX.Client.Callback;

namespace CarmackFX.Client.Connection
{
	class ConnectionService : ServiceBase, IConnectionService
	{
		private UdpSocket client = null;

		public ConnectionConfig Config { get; private set; }

		private DateTime lastHeartbeat;

		public ConnectionService(ServiceManager serviceManager)
			: base(serviceManager)
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
								ServiceManager.Resolve<IMessageService>().Completed(msgOut);
							}
							else if (msgOut.Mode == MessageMode.Callback)
							{
								ServiceManager.Resolve<ICallbackService>().Callback(msgOut);
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
				if (client != null)
				{
					if(!client.IsOpen())
					{
						this.Connect();
					}

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
