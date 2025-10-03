using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using backend.Data;
using backend.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.services
{
    public class EfCoreBlogPostService : IBlogPostService
    {

        private readonly BlogProjectDbContext _context;

        public EfCoreBlogPostService(BlogProjectDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPosts()
        {
            return await _context.BlogPosts.Include(bp => bp.Author).ToListAsync();
        }
        public async Task<BlogPost?> GetBlogPost(int id)
        {
            return await _context.BlogPosts.Include(bp => bp.Author).FirstOrDefaultAsync(bp => bp.Id == id);
        }
        public async Task<BlogPost?> CreatePost(BlogPostCreateDto postDto)
        {
            DateTime utcNow = DateTime.UtcNow;
            BlogPost newPost = new BlogPost { CreatedDateUtc = utcNow, LastUpdatedDateUtc = utcNow };
            newPost.Patch(postDto);
            await _context.BlogPosts.AddAsync(newPost);
            return await _context.SaveChangesAsync() > 0 ? newPost : null;
        }
        public async Task<BlogPost?> UpdatePost(int id, BlogPostUpdateDto post)
        {
            BlogPost? existingPost = await _context.BlogPosts.FindAsync(id);
            if (existingPost == null) return null;
            existingPost.Patch(post);
            return await _context.SaveChangesAsync() > 0 ? existingPost : throw new InvalidOperationException("Update Failed");
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