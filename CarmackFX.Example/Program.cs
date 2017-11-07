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
            var connection = ServiceManager.Resolve<IConnectionService>();
            connection.Config.Host = "app.crossgay.club";
            connection.Config.Port = 18000;
            connection.Connect();

            var authService = ServiceManager.Resolve<ISecurityService>();
            var authIn = new AuthIn() { UserName = "ACA", Password = "123456" };
            var authTask = authService.Auth(authIn);
	        var authResult = authTask.ConfigureAwait(true).GetAwaiter().GetResult();

            var protocolService = ServiceManager.Resolve<IProtocolService>();
            protocolService.Config.Token = authResult.Token;
        }
    }
}