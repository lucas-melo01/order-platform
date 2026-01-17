using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        public string CustomerName { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
