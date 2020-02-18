using Newtonsoft.Json;

namespace TD2Sarget.DTO
{
    public class RefreshRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}