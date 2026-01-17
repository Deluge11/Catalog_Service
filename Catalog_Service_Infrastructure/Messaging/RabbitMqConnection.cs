

using ConstantsLib.Exchanges;
using ConstantsLib.Interfaces;
using RabbitMQ.Client;


namespace Catalog_Service_Infrastructure.Messaging
{
    public class RabbitMqConnection : IDisposable
    {
        public RabbitMqConnection(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        private IConnectionFactory ConnectionFactory { get; }
        private IConnection Connection { get; set; }
        private bool IsDisposed;
        private readonly SemaphoreSlim ConnectionLock = new SemaphoreSlim(1, 1);

        public bool IsConnected => Connection != null && Connection.IsOpen && !IsDisposed;


        public async Task<bool> TryConnect(CancellationToken cancellationToken = default)
        {
            if (IsConnected) return true;

            await ConnectionLock.WaitAsync(cancellationToken);

            try
            {
                if (IsConnected) return true;

                Connection = await ConnectionFactory.CreateConnectionAsync(cancellationToken: cancellationToken);

                return IsConnected;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ConnectionLock.Release();
            }
        }

        public async Task<IChannel> CreateChannel(CancellationToken cancellationToken = default)
        {
            if (!IsConnected && !await TryConnect(cancellationToken))
            {
                throw new InvalidOperationException("Cannot create RabbitMQ channel: connection failed.");
            }

            return await Connection!.CreateChannelAsync(null, cancellationToken);
        }

        public async Task InitializeInfrastructure()
        {
            using var channel = await CreateChannel();

            IExchange catalogEx = new CatalogExchange();
            IExchange authEx = new AuthExchange();


            string QueueName = "catalog.queue.user.created";

            var arguments = new Dictionary<string, object?>
            {
                { "x-queue-type", "quorum" }
            };


            await channel.ExchangeDeclareAsync(
                catalogEx.Name,
                catalogEx.Type,
                catalogEx.IsDurable
                );

            await channel.ExchangeDeclareAsync(
              authEx.Name,
              authEx.Type,
              authEx.IsDurable
              );

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: arguments
                );


            await channel.QueueBindAsync(
                queue: QueueName,
                exchange: authEx.Name,
                routingKey: "auth.user.created"
                );

            await channel.BasicQosAsync(0, 1, false);

        }


        public void Dispose()
        {
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        public async ValueTask DisposeAsync()
        {
            if (IsDisposed) return;

            IsDisposed = true;

            if (Connection != null)
            {
                try
                {
                    await Connection.DisposeAsync();
                }
                catch
                {
                }
            }

            Connection?.Dispose();
        }
    }
}
