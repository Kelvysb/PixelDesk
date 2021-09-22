using System;
using System.Text.Json.Serialization;

namespace PixelDesk.Domain.Models
{
    public class D0
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D1
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D2
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D3
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D4
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D5
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D6
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D7
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class D8
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class A0
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Sensors
    {
        [JsonPropertyName("d0")]
        public D0 D0 { get; set; }

        [JsonPropertyName("d1")]
        public D1 D1 { get; set; }

        [JsonPropertyName("d2")]
        public D2 D2 { get; set; }

        [JsonPropertyName("d3")]
        public D3 D3 { get; set; }

        [JsonPropertyName("d4")]
        public D4 D4 { get; set; }

        [JsonPropertyName("d5")]
        public D5 D5 { get; set; }

        [JsonPropertyName("d6")]
        public D6 D6 { get; set; }

        [JsonPropertyName("d7")]
        public D7 D7 { get; set; }

        [JsonPropertyName("d8")]
        public D8 D8 { get; set; }

        [JsonPropertyName("a0")]
        public A0 A0 { get; set; }
    }

    public class DeviceData
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("sensors")]
        public Sensors Sensors { get; set; }

        [JsonPropertyName("datetime")]
        public DateTime Datetime { get; set; }
    }
}