using Newtonsoft.Json;

namespace CarmackFX.Client.Message
{
	class MessageError
	{
		[JsonProperty("errorCode")]
		public string ErrorCode { get; set; }

		[JsonProperty("errorMessage")]
		public string ErrorMessage { get; set; }
	}
}
