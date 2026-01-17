using OrderService.Application.Events;
using OrderService.Application.Ports;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderHandler
    {

        private readonly IOrderRepository _repository;
        private readonly IEventBus _eventBus;

        public CreateOrderHandler(IOrderRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(CreateOrderCommand command)
        {
            var order = new Order(command.CustomerName, command.TotalAmount);

            await _repository.SaveAsync(order);

            var evt = new OrderCreatedEvent
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt
            };

            await _eventBus.PublishAsync(evt);

            return order.Id;
        }
    }
}
