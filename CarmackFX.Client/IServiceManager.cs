using System;

namespace CarmackFX.Client
{
	public interface IServiceManager
	{
		object Register(string name, object instance);
		T Register<T>() where T : class;
		T Register<T>(T instance) where T : class;
		object Resolve(string name);
		T Resolve<T>();
		void Error(Exception ex);
		void Log(string message);
		void Release();
	}
}