using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Net;

namespace ESourcing.Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IDistributedCache _redisCache;


        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger, IDistributedCache redisCache)
        {
            _productRepository = productRepository;
            _logger = logger;
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            await _productRepository.Post(product);
            var res = await _productRepository.GetById(product.Id);

            await _redisCache.SetStringAsync(product.Id, JsonConvert.SerializeObject(product));
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Put([FromBody] Product product)
        {
            var res = await _productRepository.Put(product);
            await _redisCache.SetStringAsync(product.Id, JsonConvert.SerializeObject(product));
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            _redisCache.Remove(id);
            return Ok(await _productRepository.Delete(id));
        }


        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _redisCache.GetStringAsync("GetAllProducts");

            if (products is not null)
            {
                return Ok(JsonConvert.DeserializeObject<IEnumerable<Product>>(products));
            }

            var res = await _productRepository.GetProducts();

            await _redisCache.SetStringAsync("GetAllProducts", JsonConvert.SerializeObject(res));
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Get(string id)
        {

            var product = await _redisCache.GetStringAsync(id);

            if (product is not null)
            {
                return Ok(JsonConvert.DeserializeObject<Product>(product));
            }

            var res = await _productRepository.GetById(id);
            if (res is not null)
            {
                await _redisCache.SetStringAsync(id, JsonConvert.SerializeObject(res));
                return Ok(res);
            }
            else
            {
                _logger.LogError($"Product not found in database.Id : {id}");
                return NotFound();
            }
        }
    }
}
