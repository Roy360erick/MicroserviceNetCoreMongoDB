using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.api.Libreria.Core.Entities;
using Microservice.api.Libreria.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMongoRepository<Author> _authorRepository;

        public AuthorsController(IMongoRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _authorRepository.GetAll();
        }

        [HttpGet("{Id}")]
        public async Task<Author> GetAuthor(string Id)
        {
            return await _authorRepository.GetById(Id);
        }

        [HttpPost]
        public async Task InsertAuthor(Author entity)
        {
           await _authorRepository.Insert(entity);
        }

        [HttpPut("{Id}")]
        public async Task UpdateAuthor(string Id, Author entity)
        {
            entity.Id = Id;
            await _authorRepository.Update(entity);
        }

        [HttpDelete("{Id}")]
        public async Task DeteleAuthor(string Id)
        {
            await _authorRepository.Delete(Id);
        }

        [HttpPost("pagination")]
        public async Task<PaginationEntity<Author>> FilterAndPagination(PaginationEntity<Author> pagination)
        {
            return await _authorRepository.PaginationByFilter(pagination);
        }
    }
}