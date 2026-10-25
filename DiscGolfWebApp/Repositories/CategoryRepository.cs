using Skvaller.Data;
using Skvaller.Models;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllWithTopicsAsync()
    {
        return await context.Categories
            .Include(c => c.Topics)
                .ThenInclude(t => t.Posts)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task<Topic?> GetTopicByIdAsync(int id)
    {
        return await context.Topics.FindAsync(id);
    }

    public async Task AddTopicAsync(Topic topic)
    {
        context.Topics.Add(topic);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTopicAsync(Topic topic)
    {
        context.Topics.Remove(topic);
        await context.SaveChangesAsync();
    }
}