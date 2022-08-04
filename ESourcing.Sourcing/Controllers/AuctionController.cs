using AutoMapper;
using ESourcing.Products.Entities.Enums;
using ESourcing.Sourcing.Entitites;
using ESourcing.Sourcing.Repositories.Interfaces;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producers;
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
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<AuctionController> _logger;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _mQProducer;

        public AuctionController(IAuctionRepository auctionRepository, ILogger<AuctionController> logger, IBidRepository bidRepository, IMapper mapper, EventBusRabbitMQProducer mQProducer)
        {
            _auctionRepository = auctionRepository;
            _logger = logger;
            _bidRepository = bidRepository;
            _mapper = mapper;
            _mQProducer = mQProducer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var res = await _auctionRepository.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
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

        [HttpPost("CompleteAuction")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CompleteAuction(string auctionId)
        {
            var auction = await _auctionRepository.GetById(auctionId);
            if (auction is null)
            {
                return NotFound();
            }

            if (auction.Status != Status.Active)
            {
                _logger.LogError("Auction can not be completed");
                return BadRequest();
            }

            var bid = await _bidRepository.GetBidWinner(auctionId);
            if (bid is null)
            {
                return NotFound();
            }

            OrderCreateEvent eventMessage = _mapper.Map<OrderCreateEvent>(bid);
            eventMessage.Quantity = auction.Quantity;

            auction.Status = Status.Closed;

            bool updateResponse = await _auctionRepository.Put(auction);
            if (!updateResponse)
            {
                _logger.LogError("Auction can not updated!");
                return BadRequest();
            }

            try
            {
                _mQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);

            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR Publising integration event : {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted();
        }


        [HttpPost("TestEvent")]
        public ActionResult<OrderCreateEvent> TestEvent()
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.AuctionId = "test 1";
            eventMessage.ProductId = "test 1";
            eventMessage.Price = 10;
            eventMessage.Quantity = 10;
            eventMessage.SellerUserName = "test@gmail.com";


            try
            {
                _mQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);

            }
            catch (Exception)
            {
                _logger.LogError("ERROR Publising integration event : {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted(eventMessage);
        }

    }
}
