using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Ports
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
    }
}
