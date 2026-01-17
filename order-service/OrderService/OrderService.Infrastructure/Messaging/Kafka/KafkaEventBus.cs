using Confluent.Kafka;
using OrderService.Application.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.Messaging.Kafka
{
    public class KafkaEventBus : IEventBus
    {

        private readonly IProducer<string, string> _producer;

        public KafkaEventBus(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync<T>(T @event)
        {

            var topic = typeof(T).Name; 
            var payload = JsonSerializer.Serialize(@event);

            await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = payload
            });
        }
    }
}
