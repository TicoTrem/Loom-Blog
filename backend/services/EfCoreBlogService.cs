using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.services
{
    public class EfCoreBlogService : IBlogService
    {
        private readonly BlogProjectDbContext _context;

        public EfCoreBlogService(BlogProjectDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<BlogPost> GetAllPosts()
        {
            return _context.BlogPosts.ToList();
        }
        public BlogPost? GetBlogPost(int id)
        {
            return _context.BlogPosts.Find(id);
        }
        public async Task<bool> CreatePost(BlogPost post)
        {
            await _context.BlogPosts.AddAsync(post);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdatePost(BlogPost post)
        {
            _context.BlogPosts.Update(post);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeletePost(int id)
        {
            BlogPost? bp = _context.BlogPosts.Find(id);
            if (bp == null)
            {
                return false;
            }
            _context.BlogPosts.Remove(bp);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}