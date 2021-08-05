namespace PixelDesk.Domain.Models
{
    public class MQTTConfig
    {
        public string Server { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string DeviceId { get; set; }

        public string IntercomTopic { get; set; }
    }
}