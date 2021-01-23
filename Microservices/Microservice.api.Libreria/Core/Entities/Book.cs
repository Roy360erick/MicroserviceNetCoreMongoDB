using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.api.Libreria.Core.Entities
{
    [BsonCollectionAtribute("Book")]
    public class Book : Document
    {
        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Price")]
        public int Price { get; set; }

        [BsonElement("PublicationDate")]
        public DateTime? PublicationDate { get; set; }

        [BsonElement("Author")]
        public Author Author { get; set; }

    }
}
