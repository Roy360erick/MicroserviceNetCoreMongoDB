using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.api.Libreria.Core.Entities
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get ; set ; }

        public DateTime CreateAt => DateTime.Now;
    }
}
