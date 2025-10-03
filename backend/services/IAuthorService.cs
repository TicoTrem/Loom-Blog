using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface IAuthorService
    {
        Task<ServiceResponse<Author>> GetAllAuthors();
        Task<ServiceResponse<Author>> GetAuthor(int id);
        Task<ServiceResponse<Author>> CreateAuthor(AuthorCreateDto author);
        Task<ServiceResponse<Author>> UpdateAuthor(int id, AuthorUpdateDto author);
        Task<ServiceResponse<Author>> DeleteAuthor(int id);
    }
}