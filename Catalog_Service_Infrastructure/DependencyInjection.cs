using Catalog_Service_Core.Interfaces;
using Catalog_Service_Infrastructure.Messaging;
using Catalog_Service_Infrastructure.Messaging.Consumers;
using Catalog_Service_Infrastructure.Options;
using ConstantsLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;


namespace Catalog_Service_Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitmqOptions = configuration.GetSection("RabbitMQ").Get<RabbitmqOptions>();

            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                connectionString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddHostedService<RabbitMqInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHostedService<UserCreatedEventConsumer>();
            services.AddScoped<IEventBus, RabbitMqPublisher>();
            services.AddSingleton<RabbitMqConnection>();
            services.AddSingleton<IConnectionFactory>(sp =>
            {
                return new ConnectionFactory()
                {
                    Uri = new Uri(rabbitmqOptions.Uri)
                };
            });
        }
    }
}
