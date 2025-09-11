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
        BlogPost GetBlogPost(int id);
        bool CreatePost(BlogPost blogPost);
        bool UpdatePost(BlogPost blogPost);
        bool DeletePost(int id);
    }
}