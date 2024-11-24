using System.Text.Json.Serialization;

namespace vaccine_chain_bk.DTO.Request
{
    public class ResponseWrapper
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }

        [JsonPropertyName("Data")]
        public string Data { get; set; }
    }


}
