using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderReadService.Application.EventHandlers;
using OrderReadService.Application.Events;

namespace OrderReadService.Infrastructure.Messaging.Kafka
{
    public class KafkaOrderCreatedConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public KafkaOrderCreatedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "order-read-service",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe("OrderCreatedEvent");

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var evt = JsonSerializer.Deserialize<OrderCreatedEvent>(result.Message.Value);
                if (evt is null) continue;

                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<OrderCreatedEventHandler>();

                await handler.Handle(evt);
            }
        }
    }
}
