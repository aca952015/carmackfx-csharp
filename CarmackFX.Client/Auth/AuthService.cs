using CarmackFX.Client.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Message;
using Newtonsoft.Json;

namespace CarmackFX.Client.Auth
{
	class AuthService : IAuthService
	{
		public Task<AuthResult> Verify<T>(T authIn)
		{
			return MessageManager.Push<T, AuthResult>(MessageType.AUTH, authIn);
		}
	}
}
