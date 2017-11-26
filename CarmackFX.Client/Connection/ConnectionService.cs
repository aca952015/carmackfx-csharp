using System;
using System.Threading.Tasks;
using CarmackFX.Client.Message;
using CarmackFX.Client.Callback;

namespace CarmackFX.Client.Connection
{
	class ConnectionService : ServiceBase, IConnectionService
	{
		private ConnectClient client = null;

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
				client = new ConnectClient(this.Config);
				client.Received += (sender, ByteBuf) =>
				{
					this.ServiceManager.Log("recevie data from server: " + ByteBuf.Length);

					try
					{
						MessageOut msgOut = MessageOut.Parse(ByteBuf);
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
						ServiceManager.Error(ex);
					}
				};

				lastHeartbeat = DateTime.Now;

				while (true)
				{
					if (client != null)
					{
						if(!client.IsRunning())
						{
							client.Connect();
							client.Start();
						}

						SendHeartbeat();
					}
					else
					{
						break;
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
				client.Stop();
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
					if(!client.IsRunning())
					{
						client.Connect();
						client.Start();
					}

					client.Send(msgIn.Build());
				}
			}
			catch (Exception ex)
			{
				ServiceManager.Error(ex);
			}
		}
	}
}
