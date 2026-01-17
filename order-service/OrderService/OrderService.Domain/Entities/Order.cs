using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entities
{
    public class Order
    {

        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Order() { }

        public Order(string customerName, decimal totalAmount)
        {

            if(string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("Customer name is required");
            }

            if(totalAmount <= 0)
            {
                throw new ArgumentException("Total amount must be greater than zero");
            }

            Id = Guid.NewGuid();
            CustomerName = customerName;
            TotalAmount = totalAmount;  
            CreatedAt = DateTime.UtcNow;
        }
    }
}
