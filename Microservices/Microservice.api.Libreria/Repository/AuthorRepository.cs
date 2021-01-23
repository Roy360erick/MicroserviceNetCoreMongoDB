using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.api.Libreria.Core.ContextMongoDB;
using Microservice.api.Libreria.Core.Entities;
using MongoDB.Driver;

namespace Microservice.api.Libreria.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IAuthorContext _context;

        public AuthorRepository(IAuthorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
           return await _context.Authors.Find(x => true).ToListAsync();
        }
    }
}
