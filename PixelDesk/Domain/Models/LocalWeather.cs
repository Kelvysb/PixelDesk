using System.Text.Json.Serialization;

namespace PixelDesk.Domain.Models
{
    public class LocalWeather
    {
        [JsonPropertyName("temperature")]
        public int Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("light")]
        public int Light { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
    }
}