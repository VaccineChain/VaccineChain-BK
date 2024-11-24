using System.Text.Json.Serialization;

namespace vaccine_chain_bk.DTO.Sensor
{
    public class DataResonse
    {
        [JsonPropertyName("vaccine_id")]
        public string VaccineId { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("value")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Value { get; set; }
    }

}
