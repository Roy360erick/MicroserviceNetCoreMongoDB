using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microservice.api.Libreria.Core.Entities;

namespace Microservice.api.Libreria.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();
        Task<TDocument> GetById(string Id);
        Task Insert(TDocument document);
        Task Update(TDocument document);
        Task Delete(string Id);
        Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument,bool>> filterExpression,PaginationEntity<TDocument> pagination);
        Task<PaginationEntity<TDocument>> PaginationByFilter( PaginationEntity<TDocument> pagination);
    }
}
