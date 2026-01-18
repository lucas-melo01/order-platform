using NotificationService.Application.EventHandlers;
using NotificationService.Infrastructure.Kafka;
using NotificationService.Worker;

if (AppDomain.CurrentDomain.GetData("HostBuilt") == null)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddSingleton<OrderCreatedEventHandler>();
    builder.Services.AddSingleton<OrderCreatedConsumer>();
    builder.Services.AddHostedService<Worker>();

    var host = builder.Build();
    AppDomain.CurrentDomain.SetData("HostBuilt", true);
    host.Run();
}