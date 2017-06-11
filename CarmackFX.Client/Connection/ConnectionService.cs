using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Message;
using CarmackFX.Client.Protocol;

namespace CarmackFX.Client.Connection
{
    class ConnectionService : IConnectionService
    {
	    private UdpSocket client = null;

        public ConnectionConfig Config { get; private set; }


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
					MessageOut msgOut = MessageOut.Parse(buf);
					if (msgOut != null)
					{
						if (msgOut.Mode == MessageMode.Result)
						{
							MessageManager.Completed(msgOut);
						}
						else if (msgOut.Mode == MessageMode.Callback)
						{
							
						}
					}
				});

				// 绑定端口
				client.Connect(this.Config.Host, this.Config.Port);

		        while (true)
		        {
			        if (client != null && client.IsOpen())
			        {
				        client.Update();
			        }

			        System.Threading.Thread.Sleep(10);
		        }
			}));
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
			catch (Exception e)
		    {
		    }
	    }
	}
}
