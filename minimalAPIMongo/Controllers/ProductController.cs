using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection.
        /// </summary>
        private readonly IMongoCollection<Product> _product;

        /// <summary>
        /// Construtor que recebe como dependencia o objeto da classe  MongoDbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public ProductController(MongoDbService mongoDbService)
        {
            //Obtem a collection "product"
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]
        // Task para atividade assincronas, ActionResult para retornar um status code,
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
                if (products == null)
                {
                    NotFound($"Not any product found.");
                }
                return Ok(products);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewItem(Product newProduct)
        {
            try
            {
                await _product.InsertOneAsync(newProduct);
                return StatusCode(201,newProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;  
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            try
            {
                var product = await _product.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (product is null)
                {
                    return NotFound($"Not found product with id : {id}");
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product productAlterate)
        {
            try
            {
                var product = await _product.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (product is null)
                {
                    return NotFound($"Not found product with id : {id}");
                }

                productAlterate.Id = product.Id;

                await _product.ReplaceOneAsync(x => x.Id == id, productAlterate);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var book = await _product.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (book is null)
                {
                    return NotFound();
                }

                await _product.DeleteOneAsync(x => x.Id == id);

                return NoContent();
            }
            catch (Exception e )
            {
                return BadRequest(e.Message);
                throw;
            }
        }
    }
}
