using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace ESourcing.Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            await _productRepository.Post(product);
            var res = await _productRepository.GetById(product.Id);
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Put([FromBody] Product product)
        {
            var res = await _productRepository.Put(product);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }


        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var res = await _productRepository.GetProducts();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var res = await _productRepository.GetById(id);
            if (res is not null)
            {
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
