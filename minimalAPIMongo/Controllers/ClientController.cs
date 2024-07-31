using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;
using System;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection
        /// </summary>
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<User> _user;

        /// <summary>
        /// Construtor que recebe como dependencia o objeto da classe Mongo DbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public ClientController(MongoDbService mongoDbService)
        {
            // Obtem a collection "client"
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpGet]
        // Task para atividade assincronas, ActionResult para retornar um status code,
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {   
                var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewItem(Client newClient)
        {
            try
            {
                //  Realizando uma verificação para ver se existe o UserId passado por newClient na colection User.
                var user = await _user.Find(user => user.Id == newClient.UserId).FirstOrDefaultAsync();
                
                //  Caso o user não seja encontrado ele retorna notFound parando assim a inserção de dados.
                if (user == null)
                {
                    return NotFound("Usuário não encontrado ou não existe.");
                }

                await _client.InsertOneAsync(newClient);
                return StatusCode(201, newClient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            try
            {
                var cliente = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (cliente is null)
                {
                    return NotFound();
                }

                return Ok(cliente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Client clienteAlterate)
        {
            try
            {
                var cliente = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (cliente is null)
                {
                    return NotFound();
                }

                clienteAlterate.Id = cliente.Id;

                await _client.ReplaceOneAsync(x => x.Id == id, clienteAlterate);

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
                var cliente = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (cliente is null)
                {
                    return NotFound();
                }

                await _client.DeleteOneAsync(x => x.Id == id);

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
