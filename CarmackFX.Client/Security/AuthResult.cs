﻿using Newtonsoft.Json;

namespace CarmackFX.Client.Security
{
    public class AuthResult
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("token")]
        public long Token { get; set; }
    }
}