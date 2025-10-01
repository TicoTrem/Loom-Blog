using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author?> GetAuthor(int id);
        Task<bool?> CreateAuthor(AuthorCreateDto author);
        Task<bool?> DeleteAuthor(int id);
    }
}