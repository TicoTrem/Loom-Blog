using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetAllPosts();
        Task<BlogPost?> GetBlogPost(int id);
        Task<BlogPost?> CreatePost(BlogPostCreateDto blogPost);
        Task<BlogPost?> UpdatePost(int id, BlogPostUpdateDto blogPost);
        Task<bool> DeletePost(int id);
    }
}