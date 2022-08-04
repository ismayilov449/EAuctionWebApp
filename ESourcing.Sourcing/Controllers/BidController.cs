using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entitites;
using ESourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _repository;

        public BidController(IBidRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("SendBid")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendBid([FromBody] Bid bid)
        {
            await _repository.SendBid(bid);
            return Ok();
        }

        [HttpGet("GetBidWinner")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBidWinner(string id)
        {
            var res = await _repository.GetBidWinner(id);
            return Ok(res);
        }

        [HttpGet("GetBidByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBidByAuctionId(string id)
        {
            var res = await _repository.GetBidsByAuctionId(id);
            return Ok(res);
        }


    }
}
