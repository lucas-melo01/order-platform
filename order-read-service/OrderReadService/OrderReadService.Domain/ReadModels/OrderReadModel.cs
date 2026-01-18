using System;
using System.Collections.Generic;
using System.Text;

namespace OrderReadService.Domain.ReadModels
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
