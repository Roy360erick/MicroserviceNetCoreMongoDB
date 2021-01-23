using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microservice.api.Libreria.Core;
using Microservice.api.Libreria.Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.api.Libreria.Repository
{
    public class MongoRepository<TDocument> :IMongoRepository<TDocument> where TDocument :IDocument
    {

        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var _db = client.GetDatabase(options.Value.Database);

            _collection = _db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);

            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task Insert(TDocument document)
        {
           await _collection.InsertOneAsync(document);
        }

        public async Task Update(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public async Task Delete(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument, bool>> filterExpression, PaginationEntity<TDocument> pagination)
        {
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);

            if(pagination.SortDirection == "desc")
            {
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);
            }

            if (string.IsNullOrEmpty(pagination.Filter))
            {
                pagination.Data = await _collection.Find(doc => true).Sort(sort).Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();
            }
            else
            {
                pagination.Data = await _collection.Find(filterExpression).Sort(sort).Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();
            }

            long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty);
            var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalDocuments / pagination.PageSize)));
            pagination.PageQuantity = totalPages;

            return pagination;
        }

        public async Task<PaginationEntity<TDocument>> PaginationByFilter(PaginationEntity<TDocument> pagination)
        {
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);

            if (pagination.SortDirection == "desc")
            {
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);
            }

            long totalDocuments = 0;

            if (pagination.FilterValue == null)
            {
                pagination.Data = await _collection.Find(doc => true).Sort(sort).Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();

                totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty);
            }
            else
            {
                var valueFilter = ".*" + pagination.FilterValue.Value + ".*";
                var filter = Builders<TDocument>.Filter.Regex(pagination.FilterValue.Label, new MongoDB.Bson.BsonRegularExpression(valueFilter,  "i"));

                pagination.Data = await _collection.Find(filter).Sort(sort).Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();

                totalDocuments = await _collection.CountDocumentsAsync(filter);
            }

            

            var rounded = Convert.ToInt32(Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.PageSize)));

            pagination.PageQuantity = rounded;
            pagination.TotalRows = Convert.ToInt32(totalDocuments);
            return pagination;
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAtribute)documentType.GetCustomAttributes(typeof(BsonCollectionAtribute), true).FirstOrDefault()).CollectionName;
        }
    }
}
