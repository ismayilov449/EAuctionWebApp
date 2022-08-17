using ESourcing.Orders.Extensions;
using ESourcing.Orders.Infrastructure.Model;
using ESouring.Ordering.Application.Commands.OrderCreate;
using ESouring.Ordering.Application.Queries.OrderQueries;
using ESouring.Ordering.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;

namespace ESourcing.Orders.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        private readonly IDistributedCache _redisCache;
        public OrderController(IMediator mediator, ILogger<OrderController> logger, IDistributedCache redisCache)
        {
            _mediator = mediator;
            _logger = logger;
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }


        [HttpGet("GetOrdersByUsername/{username}")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrdersByUsername(string username)
        {

            if (_redisCache is not null)
            {
                var basket = await _redisCache.GetStringAsync(username);

                if (basket is not null)
                {
                    return Ok(JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(basket));
                }
            }

            var res = await _mediator.Send(new GetOrdersBySellerUsernameQuery() { Username = username });

            if (res.Count() == decimal.Zero)
                return NotFound();


            await _redisCache.SetStringAsync(username, JsonConvert.SerializeObject(res));
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] OrderCreateCommand model)
        {
            if (_redisCache is not null)
            {
                await _redisCache.SetStringAsync(model.SellerUsername, JsonConvert.SerializeObject(model));
            }
            var res = await _mediator.Send(model);
            return Ok(res);
        }
    }
}
