using System.Text.Json.Serialization;

namespace vaccine_chain_bk.DTO.Response
{
    public class UserRegistrationResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
