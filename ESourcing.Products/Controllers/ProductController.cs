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

        [HttpDelete]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            return Ok(await _productRepository.Delete(id));
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var res = await _productRepository.GetProducts();
            return Ok(res);
        }

        //[HttpGet("{id:length(24)}", Name = "GetById")]
        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetById([FromQuery] string id)
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

        [HttpGet("GetByCategoryName")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryName([FromQuery] string categoryName)
        {
            var res = await _productRepository.GetProductByCategory(categoryName);
            return Ok(res);
        }


    }
}
