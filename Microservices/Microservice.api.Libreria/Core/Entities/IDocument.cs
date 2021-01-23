using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.api.Libreria.Core.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        DateTime CreateAt { get; }
    }
}
