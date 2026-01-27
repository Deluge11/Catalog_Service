
using Catalog_Service_Application.Users.Commands.AddNewUser;
using ConstantsLib.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Catalog_Service_Infrastructure.Messaging.Consumers
{
    public class UserCreatedEventConsumer : BackgroundService
    {
        private RabbitMqConnection RabbitMqConnection { get; }
        private IServiceProvider ServiceProvider { get; }

        public UserCreatedEventConsumer(RabbitMqConnection rabbitMqConnection, IServiceProvider serviceProvider)
        {
            RabbitMqConnection = rabbitMqConnection;
            ServiceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = await RabbitMqConnection.CreateChannel();
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (obj, eventArgs) =>
            {
                await using var scope = ServiceProvider.CreateAsyncScope();

                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var userCreatedEvent = JsonSerializer.Deserialize<UserCreatedEvent>(message);

                    if (userCreatedEvent == null)
                    {
                        throw new ArgumentNullException();
                    }

                    if (await mediator.Send(new AddNewUserCommand(userCreatedEvent.UserId, userCreatedEvent.Name)))
                    {
                        Console.WriteLine($" [x] Message Ack Successfully: {message}");
                        await channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                    }
                    else
                    {
                        Console.WriteLine($"Error processing User Created Event");
                        await channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    await channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, false);
                }
            };

            await channel.BasicConsumeAsync("catalog.queue.user.created", false, consumer, stoppingToken);
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 10, global: false);
        }
    }
}
