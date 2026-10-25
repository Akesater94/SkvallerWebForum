using Skvaller.Models;

namespace Skvaller.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task AddTopicAsync(Topic topic);
    Task DeleteAsync(Category category);
    Task DeleteTopicAsync(Topic topic);
    Task<List<Category>> GetAllWithTopicsAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Topic?> GetTopicByIdAsync(int id);
}