using Newtonsoft.Json;

namespace CarmackFX.Client.Domain
{
	public class User
	{
		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("nickname")]
		public string Nickname { get; set; }
	}
}