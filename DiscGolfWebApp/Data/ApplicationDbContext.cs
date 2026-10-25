using Skvaller.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PrivateMessage> PrivateMessages { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>().HasOne(p => p.Author).WithMany()
            .HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>().HasOne(c => c.Author).WithMany()
            .HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PrivateMessage>().HasOne(m => m.Sender).WithMany()
            .HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PrivateMessage>().HasOne(m => m.Receiver).WithMany()
            .HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Report>().HasOne(r => r.Reporter).WithMany()
            .HasForeignKey(r => r.ReporterId).OnDelete(DeleteBehavior.Restrict);
    }
}
