using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {

        private readonly CreateOrderHandler _handler;

        public OrdersController(CreateOrderHandler handler)
        {
            _handler = handler;   
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var orderId = await _handler.Handle(command);

            return Created($"/orders/{orderId}", new { Id = orderId });
        }
    }
}
