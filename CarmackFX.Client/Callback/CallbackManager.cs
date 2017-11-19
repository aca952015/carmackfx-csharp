using CarmackFX.Client.Error;
using CarmackFX.Client.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client.Callback
{
	static class CallbackManager
	{
		internal static void Callback(MessageOut msgOut)
		{
			var data = JsonConvert.DeserializeObject<CallbackData>(msgOut.Data);
			var service = ServiceManager.Resolve(data.ServiceName);

			if(service != null)
			{
				Invoke(service, data.MethodName, data.Args);
			}
		}

		private static void Invoke(object service, string methodName, string[] args)
		{
			Type type = service.GetType();
			MethodInfo method = type.GetMethod(methodName);
			if(method == null)
			{
				throw new MethodNotFoundException(type.Name, methodName);
			}

			var prams = method.GetParameters();
			if(prams.Length != args.Length)
			{
				throw new MethodArgsNotMatchException(type.Name, methodName);
			}

			List<Object> invokeArgs = new List<object>();
			for(int pos = 0; pos < prams.Length; pos++)
			{
				var val = args[pos];
				if(String.IsNullOrEmpty(val))
				{
					invokeArgs.Add(null);
				}
				else
				{
					var pram = prams[pos];
					if (pram.ParameterType.IsValueType)
					{
						invokeArgs.Add(Convert.ChangeType(val, pram.ParameterType));
					}
					else
					{
						if (pram.ParameterType == typeof(string))
						{
							invokeArgs.Add(val);
						}
						else
						{
							invokeArgs.Add(JsonConvert.DeserializeObject(val, pram.ParameterType));
						}
					}
				}
			}

			method.Invoke(service, invokeArgs.ToArray());
		}
	}
}
