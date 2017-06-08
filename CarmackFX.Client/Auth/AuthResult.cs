using Newtonsoft.Json;

namespace CarmackFX.Client.Services
{
    public class AuthResult
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}