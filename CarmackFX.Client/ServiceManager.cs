using CarmackFX.Client.Security;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarmackFX.Client.Proxy;

namespace CarmackFX.Client
{
    public static class ServiceManager
    {
        private static readonly Dictionary<Type, ServiceInstance> instances = new Dictionary<Type, ServiceInstance>();

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

	        instances.Add(type, si);
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

            instances.Add(type, si);
        }

        public static T Resolve<T>()
        {
            if(instances.ContainsKey(typeof(T)))
            {
                return (T)instances[typeof(T)].Instance;
            }

            return default(T);
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
    }
}
