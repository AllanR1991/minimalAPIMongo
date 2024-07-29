using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId] //O BsonId atributo especifica um campo que deve ser sempre único.
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] // Esta opçao define o nome do campo no mongoDB como "_id" e o tipo como "ObjectId"
        public string? Id { get; set; }
        
        [BsonElement("date")]
        public DateOnly Date { get; set; }
        
        [BsonElement("status")]
        public string Status { get; set; }


        [BsonElement("produto_id")]
        public ObjectId ProdutoId { get; set; }
        [BsonElement("cliente_id")]
        public ObjectId ClienteId { get; set; }


        // As instruções abaixo permiti adicionar mais propriedades ao Obj que não estão deifinas no escopo acima.
        public Dictionary<string, string> AdditionalAtributes { get; set; }
        public Order()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }


    }
}
