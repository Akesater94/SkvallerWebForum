using Skvaller.Data;
using Skvaller.Models;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Repositories;

public class PostRepository(ApplicationDbContext context) : IPostRepository
{
    public async Task<Post?> GetByIdAsync(int id)
    {
        return await context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Post>> GetByTopicAsync(int topicId)
    {
        return await context.Posts
            .Where(p => p.TopicId == topicId)
            .Include(p => p.Author)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Post post)
    {
        context.Posts.Add(post);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }
}