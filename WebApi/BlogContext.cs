using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}