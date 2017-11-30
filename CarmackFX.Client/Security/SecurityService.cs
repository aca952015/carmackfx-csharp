using CarmackFX.Client.Message;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Domain.Response;

namespace CarmackFX.Client.Security
{
	class SecurityService : ServiceBase, ISecurityService
	{
		public SecurityService(ServiceManager serviceManager)
			: base(serviceManager)
		{

		}

		public ServiceTask Auth<T>(T authIn)
		{
			var task = new ServiceTask(() =>
			{
				ServiceResponse response = ServiceManager.Resolve<IMessageService>().Push(MessageType.Security, authIn).ConfigureAwait(true).GetAwaiter().GetResult();
				if(response != null)
				{
					if (response.IsSuccess)
					{
						var authResponse = response.Get<AuthResponse>();
						if (authResponse != null && authResponse.Success)
						{
							IProtocolService protocol = ServiceManager.Resolve<IProtocolService>();
							protocol.Config.Token = response.Token;

							return response;
						}
					}

					return response;
				}

				return new ServiceResponse()
				{
					IsSuccess = false,
					Error = new System.Exception()
				};
			});

			task.Start();

			return task;
		}
	}
}
