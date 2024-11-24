using System.Text.Json.Serialization;

namespace vaccine_chain_bk.DTO.User
{
    public class UserTokenRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("orgName")]
        public string OrgName { get; set; }
    }
}
