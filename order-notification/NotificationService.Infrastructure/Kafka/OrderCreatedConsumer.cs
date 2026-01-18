using NotificationService.Application.EventHandlers;
using NotificationService.Application.Events;
using Confluent.Kafka;
using System.Text.Json;

namespace NotificationService.Infrastructure.Kafka
{
    public class OrderCreatedConsumer
    {
        private readonly OrderCreatedEventHandler _handler;

        public OrderCreatedConsumer(OrderCreatedEventHandler handler)
        {
            _handler = handler;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "notification-service",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("order-created");

            Console.WriteLine("📡 Notification Service escutando 'order-created'...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(stoppingToken);

                    var evt = JsonSerializer.Deserialize<OrderCreatedEvent>(cr.Message.Value);

                    if (evt != null)
                    {
                        await _handler.Handle(evt);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao processar evento: {ex.Message}");
                }
            }

            consumer.Close();
        }

    }
}
