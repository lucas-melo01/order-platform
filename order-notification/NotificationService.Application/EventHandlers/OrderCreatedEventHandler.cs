using NotificationService.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Application.EventHandlers
{
    public class OrderCreatedEventHandler
    {

        public Task Handle(OrderCreatedEvent evt)
        {
            Console.WriteLine(
                $"📩 Notificação enviada para {evt.CustomerName} " +
                $"| Pedido: {evt.OrderId} " +
                $"| Total: {evt.TotalAmount:C}"
            );

            return Task.CompletedTask;
        }
    }
}
