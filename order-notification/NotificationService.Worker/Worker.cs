using NotificationService.Infrastructure.Kafka;

namespace NotificationService.Worker;

public class Worker : BackgroundService
{
    private readonly OrderCreatedConsumer _consumer;

    public Worker(OrderCreatedConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _consumer.StartAsync(stoppingToken);
    }
}
