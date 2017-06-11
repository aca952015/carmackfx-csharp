using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarmackFX.Client;
using CarmackFX.Client.Auth;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;

namespace CarmackFX.ChatRoom
{
	public partial class MainForm : Form
	{
		private static string username = "游客" + new Random().Next(100);

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			ServiceManager.Register<RoomService>();
			ServiceManager.Register<ClientService>(new ClientService());

			var connection = ServiceManager.Resolve<IConnectionService>();
			connection.Config.Host = "127.0.0.1";
			connection.Config.Port = 18000;
			connection.Connect();

			var authService = ServiceManager.Resolve<IAuthService>();
			var authIn = new AuthIn() { UserName = username, Password = "123456" };
			var authTask = authService.Verify(authIn);
			var authResult = authTask.ConfigureAwait(true).GetAwaiter().GetResult();

			var protocolService = ServiceManager.Resolve<IProtocolService>();
			protocolService.Config.Token = authResult.Token;

			var roomService = ServiceManager.Resolve<RoomService>();
			roomService.Join();
		}
	}
}
