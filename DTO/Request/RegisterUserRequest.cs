using System.Text.Json.Serialization;

namespace vaccine_chain_bk.DTO.Request
{
    public class RegisterUserRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("orgName")]
        public string OrgName { get; set; }
    }
}
