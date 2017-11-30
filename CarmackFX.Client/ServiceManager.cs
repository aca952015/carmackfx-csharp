using System;
using System.Collections.Generic;
using CarmackFX.Client.Proxy;
using CarmackFX.Client.Error;
using CarmackFX.Client.Debug;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;

namespace CarmackFX.Client
{
	class ServiceManager : IServiceManager
	{
		private Dictionary<string, ServiceInstance> instances;

		public ServiceManager()
		{
			instances = new Dictionary<string, ServiceInstance>();
		}

		public T Register<T>(T instance)
			where T : class
		{
			var type = typeof(T);

			var si = new ServiceInstance
			{
				ServiceType = GetServiceType(type),
				Instance = instance
			};

			instances.Add(type.FullName, si);

			return instance;
		}

		public T Register<T>()
			where T : class
		{
			Type type = typeof(T);
			var si = new ServiceInstance
			{
				ServiceType = GetServiceType(type)
			};

			if (si.ServiceType == ServiceType.Server)
			{
				Object instance = new ServerProxy(type, this).GetTransparentProxy();

				si.Instance = instance;
			}

			instances.Add(type.FullName, si);

			return (T)si.Instance;
		}

		public object Register(String name, object instance)
		{
			var si = new ServiceInstance
			{
				ServiceType = GetServiceType(instance.GetType()),
				Instance = instance,
			};

			instances.Add(name, si);

			return instance;
		}

		public T Resolve<T>()
		{
			string name = typeof(T).FullName;

			return (T)Resolve(name);
		}

		public object Resolve(string name)
		{
			if (instances.ContainsKey(name))
			{
				return instances[name].Instance;
			}

			return null;
		}

		private ServiceType GetServiceType(Type type)
		{
			var atts = type.GetCustomAttributes(typeof(ServiceAttribute), true);
			if (atts.Length != 0)
			{
				return (atts[0] as ServiceAttribute).ServiceType;
			}

			return ServiceType.Server;
		}

		public long GetToken()
		{
			return Resolve<IProtocolService>().Config.Token;
		}

		public void Error(Exception ex)
		{
			try
			{
				var service = this.Resolve<IErrorService>();
				if (service != null)
				{
					service.OnError(ex);
				}
				else
				{
					Console.WriteLine(ex.ToString());
				}
			}
			catch (Exception nex)
			{
				if (ex != null)
				{
					Console.WriteLine(ex.ToString());
				}

				Console.WriteLine(nex.ToString());
			}
		}

		public void Log(string message)
		{
			var service = this.Resolve<IDebugService>();
			if (service != null)
			{
				service.WriteLog(string.Format("[{0:yyyy-MM-dd HH:mm:ss}]{1}", DateTime.Now, message));
			}
		}

		public void Release()
		{
			var service = this.Resolve<IConnectionService>();
			if(service != null)
			{
				service.Disconnect();
			}
		}
	}
}
