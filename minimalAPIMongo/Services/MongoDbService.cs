using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armaena a configuração da aplicação.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Armazena uma referencia ao mongoDB.
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a configuraçao da aplicação como parametro
        /// </summary>
        /// <param name="configuration">Objeto Configuration</param>
        public MongoDbService(IConfiguration configuration) 
        {
            // Atribui a configuraçao recebida em _configuration
            _configuration = configuration;

            // Obtem a string de conexão atraves do _configuration, DbConnection foi estabelecido no appsettings.json
            var connectionString = _configuration.GetConnectionString("DbConnection");

            // Cria um objeto MongoUrl que recebe como parametro a string de conexão;
            var mongoUrl = MongoUrl.Create(connectionString);

            // Cria um client MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);

            // Obtem a referencia ao Bando de dados com o nome especifico da string de conexão.
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// Propriedade para acessaro o banco de dados.
        /// </summary>
        public IMongoDatabase GetDatabase => _database;

    }
}
