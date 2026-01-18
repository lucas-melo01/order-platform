using MongoDB.Driver;
using OrderReadService.Application.Ports;
using OrderReadService.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderReadService.Infrastructure.Persistence.Mongo
{
    public class MongoOrderReadRepository : IOrderReadRepository
    {

        private readonly IMongoCollection<OrderReadModel> _collection;

        public MongoOrderReadRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<OrderReadModel>("orders-read");
        }

        public async Task UpsertAsync(OrderReadModel order)
        {
            var filter = Builders<OrderReadModel>.Filter.Eq(x => x.Id, order.Id);
            await _collection.ReplaceOneAsync(filter, order, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<List<OrderReadModel>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
