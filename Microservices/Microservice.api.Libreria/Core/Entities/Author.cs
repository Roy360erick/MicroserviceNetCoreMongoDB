using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.api.Libreria.Core.Entities
{
    [BsonCollectionAtribute("Author")]
    public class Author : Document
    {

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("DegreeAcademy")]
        public string DegreeAcademy { get; set; }

    }
}
