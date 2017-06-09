using CarmackFX.Client.Auth;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Services;
using NMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client
{
    public static class Resolver
    {
        private static readonly Dictionary<Type, ServiceInstance> instances = new Dictionary<Type, ServiceInstance>();
        private static readonly MockFactory factory = new MockFactory();

        static Resolver()
        {
            Register(typeof(IConnectionService), new ConnectionService());
            Register(typeof(IProtocolService), new ProtocolService());
        }

        private static void Register(Type type, Object instance)
        {
            var si = new ServiceInstance();
            si.ServiceType = GetServiceType(type);
            si.Instance = instance;

            instances.Add(type, si);
        }

        public static void Register<T>()
            where T : class
        {
            Type type = typeof(T);
            var si = new ServiceInstance();
            si.ServiceType = GetServiceType(type);

            if(si.ServiceType == ServiceType.Auth)
            {
                var mock = factory.CreateMock<T>();
                T instance = mock.MockObject;
                Object proxy = new AuthProxy(instance).GetTransparentProxy();

                si.Instance = proxy;
            }
            else if(si.ServiceType == ServiceType.Game)
            {

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
            var atts = type.GetCustomAttributes(typeof(ServiceTypeAttribute), true);
            if(atts.Length != 0)
            {
                return (atts[0] as ServiceTypeAttribute).Type;
            }

            return ServiceType.Game;
        }
    }
}
