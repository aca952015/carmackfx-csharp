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
using CarmackFX.Client.Security;
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
			ServiceManager.Register(new ClientService());

			var connection = ServiceManager.Resolve<IConnectionService>();
			connection.Config.Host = "app.crossgay.club";
			connection.Config.Port = 18000;
			connection.Connect();

            var authTask = ServiceManager.Resolve<ISecurityService>()
                .Auth(new AuthIn() { UserName = username, Password = "123456" });

            authTask.Start();
            authTask.ContinueWith(JoinRoom);
		}

        private void JoinRoom(Task<AuthResult> task)
        {
            var roomService = ServiceManager.Resolve<RoomService>();
			roomService.Join().ContinueWith((joinTask) => 
			{
				
			});
        }
	}
}
