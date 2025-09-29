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
    public class EfCoreBlogService : IBlogService
    {
        private readonly BlogProjectDbContext _context;

        public EfCoreBlogService(BlogProjectDbContext dbContext)
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
            patchInChanges(newPost, postDto);
            await _context.BlogPosts.AddAsync(newPost);
            return await _context.SaveChangesAsync() > 0 ? newPost : null;
        }
        public async Task<bool> UpdatePost(int id, BlogPostUpdateDto post)
        {
            BlogPost? existingPost = await _context.BlogPosts.FindAsync(id);
            if (existingPost == null) return false;
            patchInChanges(existingPost, post);
            await _context.SaveChangesAsync();
            return true;
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

        private void patchInChanges(BlogPost ogPost, object newPost)
        {
            foreach (var p in newPost.GetType().GetProperties())
            {
                object? newValue = p.GetValue(newPost);
                PropertyInfo? bpProp = ogPost.GetType().GetProperty(p.Name);
                if (newValue == null || bpProp == null) continue;
                bpProp.SetValue(ogPost, newValue);
            }
        }
    }
}