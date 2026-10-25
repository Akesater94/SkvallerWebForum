using SkvallerAPI.Data;
using SkvallerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SkvallerAPI.Repositories;

public class PostRepository(ApiDbContext context) : IPostRepository
{
    public async Task<bool> TopicExistsAsync(int topicId)
    {
        return await context.Topics.AnyAsync(t => t.Id == topicId);
    }

    public async Task<List<Post>> GetByTopicAsync(int topicId)
    {
        return await context.Posts
            .Where(p => p.TopicId == topicId)
            .Include(p => p.Author)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();
    }
}