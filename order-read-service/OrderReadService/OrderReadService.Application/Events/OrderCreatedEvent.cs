using System;
using System.Collections.Generic;
using System.Text;

namespace OrderReadService.Application.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; init; }
        public string CustomerName { get; init; } = default!;
        public decimal TotalAmount { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
