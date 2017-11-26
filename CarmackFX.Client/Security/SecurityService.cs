﻿using System.Threading.Tasks;
using CarmackFX.Client.Message;
using CarmackFX.Client.Protocol;

namespace CarmackFX.Client.Security
{
	class SecurityService : ServiceBase, ISecurityService
	{
		public SecurityService(ServiceManager serviceManager)
			: base(serviceManager)
		{

		}

		public Task<AuthResult> Auth<T>(T authIn)
		{
			return new Task<AuthResult>(() =>
			{
				ServiceResponse response = ServiceManager.Resolve<IMessageService>().Push(MessageType.Security, authIn).ConfigureAwait(true).GetAwaiter().GetResult();
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
