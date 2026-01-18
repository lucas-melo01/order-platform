using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Application.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
