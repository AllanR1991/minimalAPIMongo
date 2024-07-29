using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class User
    {
        [BsonId] // O BsonId atributo especifica um campo que deve ser sempre único.
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] // Esta opçao define o nome do campo no mongoDB como "_id" e o tipo como "ObjectId"
        public string? Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("email")]
        public string? Email { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }

        // Adiciona um dicionario para atributos adicionais
        public Dictionary<string, string> AdditionalAtributes { get; set;  }

        /// <summary>
        /// Ao ser instanciado um objeto da classe Product, o atributo AdditionalAttribures ja vira um novo dicionario e portanto hablilitando para adicionar mais atributos ao Obj.
        /// </summary>
        public User()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }
    }
}
