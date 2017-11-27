using Newtonsoft.Json;

namespace CarmackFX.Client.Domain
{
    public class AuthResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}