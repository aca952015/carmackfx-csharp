using CarmackFX.Client.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace CarmackFX.Client.Proxy
{
	class ServerProxy : RealProxy
	{
		private ServiceType serviceType = ServiceType.Server;
		private MessageType messageType = MessageType.Public;
		private IServiceManager serviceManager;

		public ServerProxy(Type proxy, IServiceManager serviceManager)
			: base(proxy)
		{
			ServiceAttribute sattr = proxy.GetCustomAttribute<ServiceAttribute>();
			if(sattr != null)
			{
				serviceType = sattr.ServiceType;
			}

			MessageAttribute mttr = proxy.GetCustomAttribute<MessageAttribute>();
			if (mttr != null)
			{
				messageType = mttr.MessageType;
			}

			this.serviceManager = serviceManager;
		}

		public override IMessage Invoke(IMessage msg)
		{
			var methodCall = (IMethodCallMessage)msg;
			var method = (MethodInfo)methodCall.MethodBase;

			try
			{
				RpcMessageData data = new RpcMessageData();
				data.ServiceName = method.ReflectedType.Name;
				data.MethodName = method.Name;

				if (methodCall.Args.Length > 0)
				{
					IDictionary<string, RpcMessageArgument> args = new Dictionary<string, RpcMessageArgument>();

					var prams = method.GetParameters();

					for (int pos = 0; pos < prams.Length; pos++)
					{
						RpcMessageArgument arg = new RpcMessageArgument();
						arg.ArgumentName = prams[pos].Name;
						arg.IsValueType = prams[pos].ParameterType.IsValueType;

						if (methodCall.Args[pos] != null)
						{
							if (arg.IsValueType)
							{
								arg.ArgumentValue = methodCall.Args[pos].ToString();
							}
							else
							{
								arg.ArgumentValue = JsonConvert.SerializeObject(methodCall.Args[pos]);
							}
						}

						args.Add(arg.ArgumentName, arg);
					}

					data.Arguments = args;
				}

				object result = this.serviceManager.Resolve<IMessageService>().Push(messageType, data);

				return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
			}
			catch (Exception e)
			{
				if (e is TargetInvocationException && e.InnerException != null)
				{
					return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
				}

				return new ReturnMessage(e, msg as IMethodCallMessage);
			}
		}
	}
}
