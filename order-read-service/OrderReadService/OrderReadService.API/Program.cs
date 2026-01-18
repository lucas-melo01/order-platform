using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OrderReadService.Application.EventHandlers;
using OrderReadService.Application.Ports;
using OrderReadService.Infrastructure.Cache;
using OrderReadService.Infrastructure.Messaging.Kafka;
using OrderReadService.Infrastructure.Persistence.Mongo;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mongo (Read DB)
builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient("mongodb://localhost:27017"));

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("order-read-db");
});

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect("localhost:6379"));

// DI
builder.Services.AddScoped<IOrderReadRepository, MongoOrderReadRepository>();
builder.Services.AddScoped<OrderCreatedEventHandler>();
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<RedisCacheService>();

// Kafka consumer em background
builder.Services.AddHostedService<KafkaOrderCreatedConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
