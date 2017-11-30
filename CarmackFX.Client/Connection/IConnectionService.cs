using CarmackFX.Client.Message;

namespace CarmackFX.Client.Connection
{
	[Service(ServiceType.Internal)]
	public interface IConnectionService
	{
		ConnectionConfig Config { get; }
		bool Connect();
		void Disconnect();
		void Send(MessageIn msgIn);
	}
}
