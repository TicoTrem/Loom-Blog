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
            post.CreatedDateUtc = DateTime.UtcNow;
            post.LastUpdatedDateUtc = post.CreatedDateUtc;
            await _context.BlogPosts.AddAsync(post);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdatePost(int id, BlogPost post)
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

        private void patchInChanges(BlogPost ogPost, BlogPost newPost)
        {
            foreach (var p in ogPost.GetType().GetProperties())
            {
                object? newValue = p.GetValue(newPost);
                if (newValue == null) continue;
                p.SetValue(ogPost, newValue);
            }
        }
    }
}