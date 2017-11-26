namespace CarmackFX.Client.Protocol
{
	[Service(ServiceType.Internal)]
	public interface IProtocolService
	{
		ProtocolConfig Config { get; }
	}
}
