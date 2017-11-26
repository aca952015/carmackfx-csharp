using CarmackFX.Client.Message;

namespace CarmackFX.Client.Callback
{
	[Service(ServiceType.Internal)]
	interface ICallbackService
	{
		void Callback(MessageOut msgOut);
	}
}