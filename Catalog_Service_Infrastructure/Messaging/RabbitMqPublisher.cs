using ConstantsLib.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Catalog_Service_Infrastructure.Messaging
{
    public class RabbitMqPublisher : IEventBus
    {
        public RabbitMqPublisher(RabbitMqConnection rabbitMqConnection)
        {
            RabbitMqConnection = rabbitMqConnection;
        }

        public RabbitMqConnection RabbitMqConnection { get; }

        public async Task Publish<T>(T message) where T : IBaseEvent
        {
            if (!RabbitMqConnection.IsConnected)
                await RabbitMqConnection.TryConnect();

            using var channel = await RabbitMqConnection.CreateChannel();

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { Persistent = true };

            await channel.BasicPublishAsync(
               message.Exchange.Name,
               message.RoutingKey,
               mandatory: true,
               properties,
               body
               );
        }
    }
}
