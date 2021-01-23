
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.api.Libreria.Core.Entities;

namespace Microservice.api.Libreria.Repository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
    }
}
