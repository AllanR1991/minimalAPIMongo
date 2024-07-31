using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.Domains
{
    public class Client
    {
        [BsonId] // O BsonId atributo especifica um campo que deve ser sempre único.
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] // O BsonElementatributo é mapeado para o nome do campo BSON. O BsonRepresentationatributo mapeia um tipo C# para um tipo BSON específico.
        public string? Id { get; set; }    
        [BsonElement("cpf")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string? Cpf { get; set;}
        [BsonElement("phone")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string? Phone { get; set;}
        [BsonElement("adress")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string? Adress { get; set; }

        //[JsonIgnore] // Notation para ignorar o dado abaixo no Json (Caso queira precise cadastrar a propriedae abaixo, necessário retirar o mesmo).
        [BsonElement("userId")]// O BsonElementatributo é mapeado para o nome do campo BSON.
        public string? UserId { get; set; }

        // Referencia para que consiga exibir o Usuario quando ver Cliente.
        // public User? User { get; set; }

        // As instruções abaixo permiti adicionar mais propriedades ao Obj que não estão deifinas no escopo acima.
        public Dictionary<string, string> AdditionalAtributes { get; set; }

        public Client()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }

    }
}
