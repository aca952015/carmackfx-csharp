using CarmackFX.Client;
using CarmackFX.Client.Connection;
using System;
using CarmackFX.Client.Security;
using CarmackFX.Client.Domain.Response;

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

			Console.WriteLine(authResult.Get<AuthResponse>().Success);
		}
	}
}