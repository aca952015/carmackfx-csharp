using System.Threading.Tasks;

namespace CarmackFX.Client.Message
{
	[Service(ServiceType.Internal)]
	public interface IMessageService
	{
		Task<ServiceResponse> Push(MessageType messageType, object messageData);
		void Completed(MessageOut msgOut);
		void Clear();
	}
}