using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Product
    {
        [BsonId] // Esta notation define que a propriedade abaixo é o Id do objeto
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] // Esta opçao define o nome do campo no mongoDB como "_id" e o tipo como "ObjectId"
        public string? Id {  get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }

        // Asiciona um dicionario para atributos adicionais.
        public Dictionary<string, string> AdditionalAtributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um obj da classe Product, o atributo aDdtionalAttrobutes já vira um novo dicionario e portanto habilitado para adicionar + atributos
        /// </summary>
        public Product() 
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }

    }
}
