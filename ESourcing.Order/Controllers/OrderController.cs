using ESouring.Ordering.Application.Commands.OrderCreate;
using ESouring.Ordering.Application.Queries.OrderQueries;
using ESouring.Ordering.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESourcing.Orders.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("GetOrdersByUsername")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrdersByUsername([FromQuery] string username)
        {
            var res = await _mediator.Send(new GetOrdersBySellerUsernameQuery() { Username = username });

            if (res.Count() == decimal.Zero)
                return NotFound();

            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] OrderCreateCommand model)
        {
            var res = await _mediator.Send(model);
            return Ok(res);
        }
    }
}
