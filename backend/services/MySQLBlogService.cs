using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public class MySQLBlogService : IBlogService
    {
        public IEnumerable<BlogPost> GetAllPosts()
        {
            throw new NotImplementedException();
        }
        public BlogPost GetBlogPost(int id)
        {
            throw new NotImplementedException();
        }
        public bool CreatePost(BlogPost post)
        {
            throw new NotImplementedException();
        }
        public bool UpdatePost(BlogPost post)
        {
            throw new NotImplementedException();
        }
        public bool DeletePost(int id)
        {
            throw new NotImplementedException();
        }
    }
}