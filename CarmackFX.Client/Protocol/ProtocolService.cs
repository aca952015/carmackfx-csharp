namespace CarmackFX.Client.Protocol
{
	class ProtocolService : ServiceBase, IProtocolService
	{
		public ProtocolConfig Config { get; private set; }

		public ProtocolService(ServiceManager serviceManager)
			: base(serviceManager)
		{
			this.Config = new ProtocolConfig();
		}
	}
}
