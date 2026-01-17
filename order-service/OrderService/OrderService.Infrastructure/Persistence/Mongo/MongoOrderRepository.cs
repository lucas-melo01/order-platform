using MongoDB.Driver;
using OrderService.Application.Ports;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Infrastructure.Persistence.Mongo
{
    public class MongoOrderRepository: IOrderRepository
    {

        private readonly IMongoCollection<Order> _collection;

        public MongoOrderRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Order>("orders");
        }
        
        public async Task SaveAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }
    }
}
