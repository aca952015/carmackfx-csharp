using CarmackFX.Client.Message;

namespace CarmackFX.Client.Connection
{
	[Service(ServiceType.Internal)]
	public interface IConnectionService
	{
		ConnectionConfig Config { get; }
		void Connect();
		void Disconnect();
		void Send(MessageIn msgIn);
	}
}
