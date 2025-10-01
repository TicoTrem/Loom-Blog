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

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthor(int id)
        {
            return await _context.Authors.FindAsync(id);
        }
        public async Task<bool?> CreateAuthor(AuthorCreateDto author)
        {
            Author newAuthor = new Author();
            newAuthor.Patch(author);
            await _context.Authors.AddAsync(newAuthor);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool?> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

    }
}