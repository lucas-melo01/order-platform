using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Ports
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event);
    }
}
