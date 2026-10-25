using Skvaller.Data;
using Skvaller.Models;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Repositories;

public class CommentRepository(ApplicationDbContext context) : ICommentRepository
{
    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await context.Comments.FindAsync(id);
    }

    public async Task<List<Comment>> GetByPostAsync(int postId)
    {
        return await context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.Author)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }
}