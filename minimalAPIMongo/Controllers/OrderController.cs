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
                
                List<Order> listOrders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();
                
                if (listOrders is null)
                {
                    return NotFound("Not Found List of orders.");
                }

                foreach (Order order in listOrders) 
                {
                    if (order.ProductId != null)
                    {
                        FilterDefinition<Product> filter = Builders<Product>.Filter.In(product => product.Id, order.ProductId); // Verifica se a o productId  esta em order.ProductId;

                        order.Products = await _product.Find(filter).ToListAsync();
                    }

                    if (order.ClienteId != null)
                    {
                        order.Client = await _client.Find(client => client.Id == order.ClienteId).FirstOrDefaultAsync();
                    }
                }

                

                

                return Ok(listOrders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPost]
        /*
            -   Task é uma tarefa que, quando concluída, retornará um valor do tipo T.
            -   ActionResult<T> é um tipo que representa o resultado de uma ação em um controlador ASP.NET Core. ActionResult pode representar diferentes tipos de resposta HTTP, como ViewResult, JsonResult, ou StatusCodeResult. Quando T é especificado, como ActionResult<Order>, ele significa que o resultado da ação pode ser um resultado de ação que também inclui um valor do tipo Order.
            -   Order é o tipo de dado que você espera que seja retornado. Em um contexto de API, isso pode ser um modelo de dados que você está manipulando ou retornando em resposta a uma solicitação.
        */
        public async Task<ActionResult<Order>> NewItem(OrderViewModel newOrder)
        {
            try
            {
                //  Criamos neste ponto um objeto, chamado order da classe Order através da instancia "new Order()" e instanciamos.                
                Order order = new Order
                {
                    //Products = new List<Product>()
                };

                order.Id = newOrder.Id;
                order.Date = newOrder.Date;
                order.Status = newOrder.Status;

                //foreach (var products in newOrder.ProductId)
                //{
                //    var findProduct = await _product.Find(product => product.Id == products).FirstOrDefaultAsync();

                //    if (findProduct == null)
                //    {
                //        return NotFound($"The product {products} is not found");
                //    }

                //    Console.WriteLine("Vasculhando os ids dos produtos.");
                //    Console.WriteLine(products);

                //    order.Products?.Add(findProduct);                  
                //}

                order.ProductId = newOrder.ProductId;



                order.ClienteId = newOrder.ClienteId;

                //  Fazemos uma busca pelo id do Cliente, para verificar se existe o id do cliente para poder adicionar os dados
                //var client = await _client.Find(client => client.Id == newOrder.ClienteId).FirstOrDefaultAsync();
                
                //  Verificamos se cliente existe
                //if(client == null)
                //{
                //    return NotFound($"The client {newOrder.ClienteId} not found.");
                //}

                //order.Client = client;

                //  Cadastramos                                
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
                Order order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (order is null)
                {
                    return NotFound();
                }

                if ( order.ProductId != null)
                {
                    var productList = Builders<Product>.Filter.In(product => product.Id, order.ProductId); 

                    order.Products = await _product.Find(productList).ToListAsync();
                }

                if ( order.ClienteId != null )
                {
                    order.Client = await _client.Find(client => client.Id == order.ClienteId).FirstOrDefaultAsync();
                }
                
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, OrderViewModel orderAlterate)
        {
            try
            {
                //  Variavel do tipo Order, responsavel por armazenar um objeto Order.
                Order orderSearch = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();
                
                if (orderSearch is null)
                {
                    return NotFound();
                }

                if(orderAlterate.Id != null) 
                {
                    orderSearch.Id= orderAlterate.Id;
                }
                if(orderAlterate.Status != null)
                {
                    orderSearch.Status = orderAlterate.Status;
                }
                if(orderAlterate.Date != null)
                {
                    orderSearch.Date = orderAlterate.Date;
                }
                if(orderAlterate.ProductId != null)
                {
                    orderSearch.ProductId = orderAlterate.ProductId;
                }
                if(orderAlterate.ClienteId != null)
                {
                    orderSearch.ClienteId = orderAlterate.ClienteId;
                }

                await _order.ReplaceOneAsync(orderSearch => orderSearch.Id == id, orderSearch);

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
                    return NotFound($"Not found a order with id {id}");
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
