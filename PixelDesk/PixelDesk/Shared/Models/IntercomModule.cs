using System.Text.Json.Serialization;

namespace PixelDesk.Shared.Models
{
    public class DigitalIn
    {
        [JsonPropertyName("d2")]
        public string D2 { get; set; }

        [JsonPropertyName("d3")]
        public string D3 { get; set; }

        [JsonPropertyName("d4")]
        public string D4 { get; set; }

        [JsonPropertyName("d5")]
        public string D5 { get; set; }

        [JsonPropertyName("d6")]
        public string D6 { get; set; }

        [JsonPropertyName("d7")]
        public string D7 { get; set; }

        [JsonPropertyName("d8")]
        public string D8 { get; set; }
    }

    public class IntercomModule
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("external")]
        public string External { get; set; }

        [JsonPropertyName("sound")]
        public string Sound { get; set; }

        [JsonPropertyName("digital_in")]
        public DigitalIn DigitalIn { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("datetime")]
        public string Datetime { get; set; }
    }
}