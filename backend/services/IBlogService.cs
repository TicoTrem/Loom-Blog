using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface IBlogPostService
    {
        Task<ServiceResponse<IEnumerable<BlogPost>>> GetAllPosts();
        Task<ServiceResponse<BlogPost>> GetBlogPost(int id);
        Task<ServiceResponse<BlogPost>> CreatePost(BlogPostCreateDto blogPost);
        Task<ServiceResponse<BlogPost>> UpdatePost(int id, BlogPostUpdateDto blogPost);
        Task<ServiceResponse<BlogPost>> DeletePost(int id);
    }
}