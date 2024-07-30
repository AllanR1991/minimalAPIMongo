using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId] //O BsonId atributo especifica um campo que deve ser sempre único.
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] // Esta opçao define o nome do campo no mongoDB como "_id" e o tipo como "ObjectId"
        public string? Id { get; set; }
        
        [BsonElement("date")]
        public DateTime? Date { get; set; }
        
        [BsonElement("status")]
        public string? Status { get; set; }


        // Referencia para que eu consiga cadastrar um pedido com os produtos.
        [JsonIgnore] // Usado para ele ser ignorado pelo Json para nao ter redundancia, com a lista abaixo.
        [BsonElement("produto_id")]
        public List<string>? ProductId { get; set; }

        // Referencia para quando listar os pedidos , venham os dados de cada produto.
        public List<Product>? Products { get; set; } // Criado uma prop do tipo lista devido a termos varios dados em um pedido.

        // Referencia para que seja possivel cadastrar um pedido com o cliente.
        [JsonIgnore] // Usado para ele ser ignorado pelo Json para nao ter redundancia, com a lista abaixo.
        [BsonElement("cliente_id")]
        public string? ClienteId { get; set; }


        // Referencia para quando listar os pedidos, apareça os dados do cliente.
        public Client? Client { get; set; } // Neste caso não foi criado uma lista pois temos apenas um unico cliente para pedido e nao uma lista de clientes para um unico pedido.


        // As instruções abaixo permiti adicionar mais propriedades ao Obj que não estão deifinas no escopo acima.
        public Dictionary<string, string> AdditionalAtributes { get; set; }
        public Order()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }


    }
}
