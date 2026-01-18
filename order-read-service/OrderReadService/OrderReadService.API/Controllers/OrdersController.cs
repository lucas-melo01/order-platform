using Microsoft.AspNetCore.Mvc;
using OrderReadService.Application.Ports;
using OrderReadService.Infrastructure.Cache;

namespace OrderReadService.API.Controllers
{

    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private const string CacheKey = "orders:all";

        private readonly IOrderReadRepository _repository;
        private readonly RedisCacheService _cache;

        public OrdersController(IOrderReadRepository repository, RedisCacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cached = await _cache.GetAsync<object>(CacheKey);

            if (cached is not null)
            {
                return Ok(cached);
            }

            var orders = await _repository.GetAllAsync();

            await _cache.SetAsync(CacheKey, orders, TimeSpan.FromSeconds(30));

            return Ok(orders);
        }
    }
}
