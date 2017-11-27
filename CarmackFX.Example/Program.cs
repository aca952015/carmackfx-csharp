using CarmackFX.Client;
using CarmackFX.Client.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Security;

namespace CarmackFX.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			IServiceManager sm = ServiceManagerFactory.CreateInstance();

			var connection = sm.Resolve<IConnectionService>();
			connection.Config.Host = "app.crossgay.club";
			connection.Config.Port = 18000;
			connection.Connect();

			var authService = sm.Resolve<ISecurityService>();
			var authIn = new AuthIn() { UserName = "ACA", Password = "123456" };
			var authTask = authService.Auth(authIn);
			var authResult = authTask.ConfigureAwait(true).GetAwaiter().GetResult();

			Console.WriteLine(authResult.User.Username);
		}
	}
}