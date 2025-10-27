using Microsoft.EntityFrameworkCore;
using backend.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.Data
{
    public class BlogProjectDbContext : IdentityDbContext
    {
        public BlogProjectDbContext(DbContextOptions options) : base(options) { }
        public DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;

    }
}
