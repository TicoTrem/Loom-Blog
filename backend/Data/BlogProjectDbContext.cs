using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.models;

namespace backend.Data
{
    public class BlogProjectDbContext : DbContext
    {
        public BlogProjectDbContext(DbContextOptions options) : base(options) { }
        public DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        
    }
}