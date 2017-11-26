namespace CarmackFX.Client
{
	class ServiceBase
	{
		public IServiceManager ServiceManager { get; private set; }

		public ServiceBase(IServiceManager serviceManager)
		{
			this.ServiceManager = serviceManager;
		}
	}
}
