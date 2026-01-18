using OrderReadService.Application.Events;
using OrderReadService.Application.Ports;
using OrderReadService.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderReadService.Application.EventHandlers
{
    public class OrderCreatedEventHandler
    {
        private const string CacheKey = "orders:all";

        private readonly IOrderReadRepository _repository;
        private readonly ICacheService _cache;

        public OrderCreatedEventHandler(IOrderReadRepository repository, ICacheService cache) 
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task Handle(OrderCreatedEvent evt)
        {

            var readModel = new OrderReadModel
            {
                Id = evt.OrderId,
                CustomerName = evt.CustomerName,
                TotalAmount = evt.TotalAmount,
                CreatedAt = evt.CreatedAt,
            };

            await _repository.UpsertAsync(readModel);
            await _cache.RemoveAsync(CacheKey);
        }
    }
}
