using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Client
    {
        [BsonId] // O BsonId atributo especifica um campo que deve ser sempre único.
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] // O BsonElementatributo é mapeado para o nome do campo BSON. O BsonRepresentationatributo mapeia um tipo C# para um tipo BSON específico.
        public string? Id { get; set; }    
        [BsonElement("cpf")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string Cpf { get; set;}
        [BsonElement("phone")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string Phone { get; set;}
        [BsonElement("adress")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string Adress { get; set; }

        [BsonElement("userId")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public ObjectId UserId { get; set; }

        // As instruções abaixo permiti adicionar mais propriedades ao Obj que não estão deifinas no escopo acima.
        public Dictionary<string, string> AdditionalAtributes { get; set; }

        public Client()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }

    }
}
