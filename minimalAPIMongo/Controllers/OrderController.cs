using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using minimalAPIMongo.ViewModels;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<Product> _product;

        public OrderController(MongoDbService mongoDbService)
        {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]
        // Task para atividade assincronas, ActionResult para retornar um status code,
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var orders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> NewItem(OrderViewModel newOrder)
        {
            try
            {
                //  Criamos uma instancia de order onde armazenamos os dados.
                Order order = new Order();
                order.Id = newOrder.Id;
                order.Date = newOrder.Date;
                order.Status = newOrder.Status;
                order.ProductId = newOrder.ProductId; // Aguardando uma lista de produtos.
                order.ClienteId = newOrder.ClienteId;
                //  Fazemos uma busca pelo id do Cliente.
                var client = await _client.Find(client => client.Id == newOrder.Id).FirstOrDefaultAsync();
                //  Verificamos se cliente existe
                if(client == null)
                {
                    return NotFound();
                }
                //  Cadastramos 
                order.Client = client;

                await _order.InsertOneAsync(order);
                return StatusCode(201, order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            try
            {
                var order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (order is null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Order orderAlterate)
        {
            try
            {
                var order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (order is null)
                {
                    return NotFound();
                }

                orderAlterate.Id = order.Id;

                await _order.ReplaceOneAsync(x => x.Id == id, orderAlterate);

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
                var order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (order is null)
                {
                    return NotFound();
                }

                await _order.DeleteOneAsync(x => x.Id == id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
    }
}
