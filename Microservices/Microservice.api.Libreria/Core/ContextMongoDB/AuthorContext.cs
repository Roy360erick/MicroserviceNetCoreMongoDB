using System;
using Microservice.api.Libreria.Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.api.Libreria.Core.ContextMongoDB
{
    public class AuthorContext : IAuthorContext
    {
        private readonly IMongoDatabase _db;

        public AuthorContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);

        }

        public IMongoCollection<Author> Authors => _db.GetCollection<Author>("Author");
    }
}
