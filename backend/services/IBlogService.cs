using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface IBlogService
    {
        IEnumerable<BlogPost> GetAllPosts();
        BlogPost? GetBlogPost(int id);
        Task<bool> CreatePost(BlogPost blogPost);
        Task<bool> UpdatePost(BlogPost blogPost);
        Task<bool> DeletePost(int id);
    }
}