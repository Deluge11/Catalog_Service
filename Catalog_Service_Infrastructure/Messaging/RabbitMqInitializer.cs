using Microsoft.Extensions.Hosting;

namespace Catalog_Service_Infrastructure.Messaging
{
    public class RabbitMqInitializer : IHostedService
    {
        private readonly RabbitMqConnection _connection;

        public RabbitMqInitializer(RabbitMqConnection connection)
        {
            _connection = connection;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _connection.InitializeInfrastructure();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
