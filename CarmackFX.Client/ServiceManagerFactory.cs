using CarmackFX.Client.Callback;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Message;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Security;

namespace CarmackFX.Client
{
	public static class ServiceManagerFactory
	{
		public static IServiceManager CreateInstance()
		{
			var sm = new ServiceManager();

			sm.Register<IConnectionService>(new ConnectionService(sm));
			sm.Register<IProtocolService>(new ProtocolService(sm));
			sm.Register<ISecurityService>(new SecurityService(sm));
			sm.Register<IMessageService>(new MessageService(sm));
			sm.Register<ICallbackService>(new CallbackService(sm));

			return sm;
		}
	}
}
