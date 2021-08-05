using System;
using System.Text.Json.Serialization;

namespace PixelDesk.Domain.Models
{
    public class DigitalIn
    {
        [JsonPropertyName("d2")]
        public bool D2 { get; set; }

        [JsonPropertyName("d3")]
        public bool D3 { get; set; }

        [JsonPropertyName("d4")]
        public bool D4 { get; set; }

        [JsonPropertyName("d5")]
        public bool D5 { get; set; }

        [JsonPropertyName("d6")]
        public bool D6 { get; set; }

        [JsonPropertyName("d7")]
        public bool D7 { get; set; }

        [JsonPropertyName("d8")]
        public bool D8 { get; set; }
    }

    public class IntercomData
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("intercom")]
        public bool Intercom { get; set; }

        [JsonPropertyName("sound")]
        public bool Sound { get; set; }

        [JsonPropertyName("digital_in")]
        public DigitalIn DigitalIn { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("datetime")]
        public DateTime Datetime { get; set; }
    }
}