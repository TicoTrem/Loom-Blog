using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services
{
    public class EfCoreAuthorService : IAuthorService
    {

        private readonly BlogProjectDbContext _context;

        public EfCoreAuthorService(BlogProjectDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<ServiceResponse<Author>> GetAuthor(int id)
        {
            Author? foundAuthor = await _context.Authors.FindAsync(id);
            if (foundAuthor == null)
                return ServiceResponse<Author>.NotFound("The author with ID {id} could not be found");
            else
                return ServiceResponse<Author>.Success(foundAuthor);
        }
        public async Task<ServiceResponse<Author>> CreateAuthor(AuthorCreateDto author)
        {
            Author newAuthor = new Author();
            newAuthor.Patch(author);
            await _context.Authors.AddAsync(newAuthor);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return  bSuccess ? ServiceResponse<Author>.Success() : ServiceResponse<Author>.Failed("The Author was not able to be created for unknown reasons");
        }

        public async Task<ServiceResponse<Author>> UpdateAuthor(int id, AuthorUpdateDto author)
        {
            Author? existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null) return ServiceResponse<Author>.NotFound($"The Author with ID {id} could not be found and was not updated");
            existingAuthor.Patch(author);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<Author>.Success(existingAuthor) : ServiceResponse<Author>.Failed("The Author was found, but was not able to be updated");
        }

        public async Task<ServiceResponse<Author>> DeleteAuthor(int id)
        {
            Author? authorToDelete = await _context.Authors.FindAsync(id);
            if (authorToDelete == null)
            {
                return ServiceResponse<Author>.NotFound($"The Author with ID {id} could not be found and was not deleted.");
            }
            _context.Authors.Remove(authorToDelete);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<Author>.Success() : ServiceResponse<Author>.Failed("The Author was found, but was not able to be deleted.");

        }

    }
}