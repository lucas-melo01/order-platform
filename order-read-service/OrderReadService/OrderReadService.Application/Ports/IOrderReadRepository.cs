using OrderReadService.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderReadService.Application.Ports
{
    public interface IOrderReadRepository
    {
        Task UpsertAsync(OrderReadModel order);
        Task<List<OrderReadModel>> GetAllAsync();

    }
}
