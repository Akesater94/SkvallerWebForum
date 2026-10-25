using Microsoft.EntityFrameworkCore;
using SkvallerAPI.Models;

namespace SkvallerAPI.Data;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("AspNetUsers");
        builder.Entity<Topic>().ToTable("Topics");
        builder.Entity<Post>().ToTable("Posts");
    }
}
