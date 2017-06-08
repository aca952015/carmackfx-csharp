using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;
using CarmackFX.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmackFX.Client
{
    public static class Resolver
    {
        static Resolver()
        {
            Register(typeof(IConnectionService), new ConnectionService());
            Register(typeof(IProtocolService), new ProtocolService());
        }

        private static void Register(Type type, Object instance)
        {

        }

        public static void Register(Type type)
        {

        }

        public static T Resolve<T>()
        {

        }
    }
}
