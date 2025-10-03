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

        public async Task<ServiceResponse<IEnumerable<Author>>> GetAllAuthors()
        {
            return ServiceResponse<IEnumerable<Author>>.Success(await _context.Authors.ToListAsync());
        }

        public async Task<ServiceResponse<Author>> GetAuthor(int id)
        {
            Author? foundAuthor = await _context.Authors.FindAsync(id);
            if (foundAuthor == null)
                return ServiceResponse<Author>.NotFound();
            else
                return ServiceResponse<Author>.Success(foundAuthor);
        }
        public async Task<ServiceResponse<Author>> CreateAuthor(AuthorCreateDto author)
        {
            Author newAuthor = new Author();
            newAuthor.Patch(author);
            await _context.Authors.AddAsync(newAuthor);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return  bSuccess ? ServiceResponse<Author>.Success(newAuthor) : ServiceResponse<Author>.Failed();
        }

        public async Task<ServiceResponse<Author>> UpdateAuthor(int id, AuthorUpdateDto author)
        {
            Author? existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null) return ServiceResponse<Author>.NotFound();
            existingAuthor.Patch(author);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<Author>.Success(existingAuthor) : ServiceResponse<Author>.Failed();
        }

        public async Task<ServiceResponse<Author>> DeleteAuthor(int id)
        {
            Author? authorToDelete = await _context.Authors.FindAsync(id);
            if (authorToDelete == null)
            {
                return ServiceResponse<Author>.NotFound();
            }
            _context.Authors.Remove(authorToDelete);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<Author>.Success() : ServiceResponse<Author>.Failed();

        }

    }
}