using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using PixelDesk.Domain.Abstractions.Services;
using PixelDesk.Domain.Models;

namespace PixelDesk.Domain.Services
{
    public class IntercomService : IIntercomService
    {
        private readonly MQTTConfig mqttConfig;
        private readonly MqttClientOptions mqttOptions;
        private readonly IManagedMqttClient managedMqttClientSubscriber;
        private Action<DeviceData> receiveMessageHandler;

        public IntercomService(
            MQTTConfig mqttConfig)
        {
            this.mqttConfig = mqttConfig;
            mqttOptions = ConfigureMQTT(mqttConfig);
            managedMqttClientSubscriber = new MqttFactory()
                .CreateManagedMqttClient();
        }

        public async Task Subscribe(Action<DeviceData> receiveMessage)
        {
            receiveMessageHandler = receiveMessage;
            managedMqttClientSubscriber.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnSubscriberConnected);
            managedMqttClientSubscriber.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnSubscriberDisconnected);
            managedMqttClientSubscriber.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnSubscriberMessageReceived);
            if (!managedMqttClientSubscriber.IsStarted)
            {
                await managedMqttClientSubscriber.StartAsync(
                    new ManagedMqttClientOptions
                    {
                        ClientOptions = mqttOptions
                    });
            }
            var topicFilter = new MqttTopicFilter { Topic = mqttConfig.IntercomTopic };
            await managedMqttClientSubscriber.SubscribeAsync(topicFilter);
        }

        public async Task Unsubscribe()
        {
            receiveMessageHandler = null;
            await managedMqttClientSubscriber.UnsubscribeAsync(mqttConfig.IntercomTopic);
            await managedMqttClientSubscriber.StopAsync();
        }

        private static void OnSubscriberConnected(MqttClientConnectedEventArgs args)
        {
            Console.WriteLine("Intercom subscriber connected");
        }

        private static void OnSubscriberDisconnected(MqttClientDisconnectedEventArgs args)
        {
            Console.WriteLine("Intercom subscriber disconnected");
        }

        private void OnSubscriberMessageReceived(MqttApplicationMessageReceivedEventArgs args)
        {
            Console.WriteLine($"Intercom message received {DateTime.Now}");
            if (args.ApplicationMessage != null && args.ApplicationMessage.Payload != null)
            {
                var message = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
                var result = JsonSerializer.Deserialize<DeviceData>(message);
                receiveMessageHandler.Invoke(result);
            }
        }

        private MqttClientOptions ConfigureMQTT(MQTTConfig mqttConfig)
        {
            var options = new MqttClientOptions
            {
                ClientId = $"{mqttConfig.DeviceId}-{Helpers.SessionIdHelper.CreateSessionId()}",
                ProtocolVersion = MqttProtocolVersion.V500,
                ChannelOptions = new MqttClientTcpOptions
                {
                    Server = mqttConfig.Server,
                    Port = mqttConfig.Port,
                    TlsOptions = new MqttClientTlsOptions
                    {
                        UseTls = false,
                        IgnoreCertificateChainErrors = true,
                        IgnoreCertificateRevocationErrors = true,
                        AllowUntrustedCertificates = true
                    }
                }
            };
            options.Credentials = new MqttClientCredentials
            {
                Username = mqttConfig.User,
                Password = Encoding.UTF8.GetBytes(mqttConfig.Password)
            };
            options.CleanSession = false;
            return options;
        }
    }
}