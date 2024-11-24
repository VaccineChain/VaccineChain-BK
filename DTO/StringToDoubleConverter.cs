using System.Text.Json;
using System.Text.Json.Serialization;

namespace vaccine_chain_bk.DTO
{
    public class StringToDoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Xử lý khi value là chuỗi
            if (reader.TokenType == JsonTokenType.String && double.TryParse(reader.GetString(), out var result))
            {
                return result;
            }

            // Xử lý khi value là số
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble();
            }

            throw new JsonException("Invalid value type for double.");
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
