using ESourcing.Sourcing.Entitites;
using ESourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IAuctionRepository auctionRepository, ILogger<AuctionController> logger)
        {
            _auctionRepository = auctionRepository;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var res = await _auctionRepository.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            var res = await _auctionRepository.GetById(id);
            if (res is null)
            {
                _logger.LogError($"Auction not found.Id : {id}");
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("GetByName")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var res = await _auctionRepository.GetByName(name);
            if (res is null)
            {
                _logger.LogError($"Auction not found.Name : {name}");
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] Auction auction)
        {
            await _auctionRepository.Post(auction);
            var res = await _auctionRepository.GetById(auction.Id);
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] Auction auction)
        {
            var res = await _auctionRepository.Put(auction);
            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var res = await _auctionRepository.Delete(id);
            return Ok();
        }


    }
}
