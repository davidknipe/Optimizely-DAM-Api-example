using Newtonsoft.Json;
using System.Configuration;

namespace Foundation.Features.OptimizelyDAM.REST.Authorization
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest()
        {
        }
        
        public AuthorizationRequest(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = "client_credentials";

        [JsonProperty("scope")]
        public string Scope { get; set; } = "scope";

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}