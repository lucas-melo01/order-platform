using Confluent.Kafka;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Ports;
using OrderService.Infrastructure.Messaging.Kafka;
using OrderService.Infrastructure.Persistence.Mongo;

var builder = WebApplication.CreateBuilder(args);

// Registrar como RFC 4122 (Standard) para serialização de Guid
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient("mongodb://localhost:27017"));

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("order-write-db");
});

builder.Services.AddSingleton<IProducer<string, string>>(_ =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = "localhost:9092"
    };

    return new ProducerBuilder<string, string>(config).Build();
});

builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();
builder.Services.AddScoped<IEventBus, KafkaEventBus>();
builder.Services.AddScoped<CreateOrderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
