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
    public class BooksController : ControllerBase
    {
        private readonly IMongoRepository<Book> _mongoRepository;

        public BooksController(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAuthors()
        {
            return await _mongoRepository.GetAll();
        }

        [HttpGet("{Id}")]
        public async Task<Book> GetAuthor(string Id)
        {
            return await _mongoRepository.GetById(Id);
        }

        [HttpPost]
        public async Task InsertAuthor(Book entity)
        {
            await _mongoRepository.Insert(entity);
        }

        [HttpPut("{Id}")]
        public async Task UpdateAuthor(string Id, Book entity)
        {
            entity.Id = Id;
            await _mongoRepository.Update(entity);
        }

        [HttpDelete("{Id}")]
        public async Task DeteleAuthor(string Id)
        {
            await _mongoRepository.Delete(Id);
        }

        [HttpPost("pagination")]
        public async Task<PaginationEntity<Book>> FilterAndPagination(PaginationEntity<Book> pagination)
        {
            return await _mongoRepository.PaginationByFilter(pagination);
        }
    }
}