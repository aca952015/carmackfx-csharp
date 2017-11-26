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
using CarmackFX.Client.Error;

namespace CarmackFX.ChatRoom
{
	[Service(ServiceType.Client)]
	public partial class MainForm : Form, IErrorService
	{
		private static string username = "游客" + new Random().Next(100);
		private IServiceManager serviceManager;
		private RoomService roomService;

		public MainForm()
		{
			InitializeComponent();
		}

		public void OnError(Exception ex)
		{

		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			serviceManager = ServiceManagerFactory.CreateInstance();

			serviceManager.Register<IErrorService>(this);
			serviceManager.Register("ClientCallback", this);
			roomService = serviceManager.Register<RoomService>();

			var connection = serviceManager.Resolve<IConnectionService>();
			connection.Config.Host = "app.crossgay.club";
			connection.Config.Port = 18000;
			connection.Connect();

			var authTask = serviceManager.Resolve<ISecurityService>()
				.Auth(new AuthIn() { UserName = username, Password = "123456" });

			authTask.Start();
			authTask.ContinueWith(JoinRoom);

		}

		private void JoinRoom(Task<AuthResult> task)
		{
			roomService.Join().ContinueWith((joinTask) => 
			{
				roomService.Chat("大家好");
			});
		}

		public void UserJoin(String username)
		{
			userList.Items.Add(username);

			msgBox.AppendText(username + "进入聊天室" + "\r\n");
		}

		public void UserLeave(String username)
		{
			userList.Items.Remove(username);
		}

		public void Broadcast(string from, string content)
		{
			msgBox.AppendText(from + "说：" + content + "\r\n");
		}

		private void send_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(message.Text))
			{
				return;
			}

			roomService.Chat(message.Text);
			message.Text = string.Empty;
		}
	}
}
