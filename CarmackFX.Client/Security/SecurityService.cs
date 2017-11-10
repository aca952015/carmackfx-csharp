using CarmackFX.Client.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Message;
using Newtonsoft.Json;
using CarmackFX.Client.Protocol;

namespace CarmackFX.Client.Security
{
	class SecurityService : ISecurityService
	{
		public Task<AuthResult> Auth<T>(T authIn)
		{
            return new Task<AuthResult>(() =>
            {
                ServiceResponse response = MessageManager.Push(MessageType.Security, authIn).ConfigureAwait(true).GetAwaiter().GetResult();
                if(response != null && response.IsSuccess)
                {
					AuthResult result = response.Get<AuthResult>();
					if (result != null && result.Token > 0)
					{
						IProtocolService protocol = ServiceManager.Resolve<IProtocolService>();
						protocol.Config.Token = result.Token;
					}
                }

                return null;
            });
		}
	}
}
