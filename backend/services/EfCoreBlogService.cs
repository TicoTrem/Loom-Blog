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

        public async Task<ServiceResponse<IEnumerable<BlogPost>>> GetAllPosts()
        {
            IEnumerable<BlogPost> blogPosts = await _context.BlogPosts.Include(bp => bp.Author).ToListAsync();
            return ServiceResponse<IEnumerable<BlogPost>>.Success(blogPosts);
        }
        public async Task<ServiceResponse<BlogPost>> GetBlogPost(int id)
        {
            BlogPost? bp = await _context.BlogPosts.Include(bp => bp.Author).FirstOrDefaultAsync(bp => bp.Id == id);
            return bp == null ? ServiceResponse<BlogPost>.NotFound() : ServiceResponse<BlogPost>.Success(bp);
        }
        public async Task<ServiceResponse<BlogPost>> CreatePost(BlogPostCreateDto postDto)
        {
            Console.WriteLine("Incoming content!");
            Console.WriteLine($"Title: {postDto.Title}\nContent: {postDto.Content}\nAuthorID: {postDto.AuthorId}");
            DateTime utcNow = DateTime.UtcNow;
            BlogPost newPost = new BlogPost { CreatedDateUtc = utcNow, LastUpdatedDateUtc = utcNow };
            newPost.Patch(postDto); 
            await _context.BlogPosts.AddAsync(newPost);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<BlogPost>.Success(newPost) : ServiceResponse<BlogPost>.Failed();
        }
        public async Task<ServiceResponse<BlogPost>> UpdatePost(int id, BlogPostUpdateDto post)
        {
            BlogPost? existingPost = await _context.BlogPosts.FindAsync(id);
            if (existingPost == null) return ServiceResponse<BlogPost>.NotFound();
            existingPost.Patch(post);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<BlogPost>.Success(existingPost) : ServiceResponse<BlogPost>.Failed();
        }
        public async Task<ServiceResponse<BlogPost>> DeletePost(int id)
        {
            BlogPost? bp = _context.BlogPosts.Find(id);
            if (bp == null) return ServiceResponse<BlogPost>.NotFound();
            _context.BlogPosts.Remove(bp);
            bool bSuccess = await _context.SaveChangesAsync() > 0;
            return bSuccess ? ServiceResponse<BlogPost>.Success() : ServiceResponse<BlogPost>.Failed();
        }


    }
}