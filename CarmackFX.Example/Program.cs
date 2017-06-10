﻿using CarmackFX.Client;
using CarmackFX.Client.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Auth;

namespace CarmackFX.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = ServiceManager.Resolve<IConnectionService>();
            connection.Config.Host = "127.0.0.1";
            connection.Config.Port = 18000;
            connection.Connect();

            var authService = ServiceManager.Resolve<IAuthService>();
            var authIn = new AuthIn() { UserName = "ACA", Password = "123456" };
            var authTask = authService.Verify(authIn);
	        var authResult = authTask.ConfigureAwait(true).GetAwaiter().GetResult();

            var protocolService = ServiceManager.Resolve<IProtocolService>();
            protocolService.Config.Token = authResult.Token;
        }
    }
}