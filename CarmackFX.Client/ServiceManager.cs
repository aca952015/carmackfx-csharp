using CarmackFX.Client.Security;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Proxy;
using CarmackFX.Client.Error;

namespace CarmackFX.Client
{
    public static class ServiceManager
    {
        private static readonly Dictionary<string, ServiceInstance> instances = new Dictionary<string, ServiceInstance>();

		static ServiceManager()
        {
            Register<IConnectionService>(new ConnectionService());
            Register<IProtocolService>(new ProtocolService());
			Register<ISecurityService>(new SecurityService());
        }

        public static void Register<T>(T instance)
			where T : class
        {
	        var type = typeof(T);

	        var si = new ServiceInstance
	        {
		        ServiceType = GetServiceType(type),
				Instance = instance
	        };

	        instances.Add(type.FullName, si);
        }

        public static void Register<T>()
            where T : class
        {
            Type type = typeof(T);
	        var si = new ServiceInstance
	        {
		        ServiceType = GetServiceType(type)
	        };

	        if(si.ServiceType == ServiceType.Server)
            {
                Object instance = new ServerProxy(type).GetTransparentProxy();

                si.Instance = instance;
            }

            instances.Add(type.FullName, si);
        }

		public static void Register(String name, object instance)
		{
			var si = new ServiceInstance
			{
				ServiceType = GetServiceType(instance.GetType()),
				Instance = instance,
			};

			instances.Add(name, si);
		}

        public static T Resolve<T>()
        {
			string name = typeof(T).FullName;

			return (T)Resolve(name);
        }

		public static object Resolve(string name)
		{
			if (instances.ContainsKey(name))
			{
				return instances[name].Instance;
			}

			return null;
		}

		private static ServiceType GetServiceType(Type type)
        {
            var atts = type.GetCustomAttributes(typeof(ServiceAttribute), true);
            if(atts.Length != 0)
            {
                return (atts[0] as ServiceAttribute).ServiceType;
            }

            return ServiceType.Server;
        }

		internal static void OnError(Exception ex)
		{
			try
			{
				var errorService = ServiceManager.Resolve<IErrorService>();
				if (errorService != null)
				{
					errorService.OnError(ex);
				}
				else
				{
					Console.WriteLine(ex.ToString());
				}
			}
			catch (Exception nex)
			{
				Console.WriteLine(ex.ToString());
				Console.WriteLine(nex.ToString());
			}
		}
	}
}
