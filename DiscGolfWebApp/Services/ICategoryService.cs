using Skvaller.Models;

namespace Skvaller.Services;

public interface ICategoryService
{
    Task AddCategoryAsync(string name);
    Task AddTopicAsync(int categoryId, string name);
    Task DeleteCategoryAsync(int id);
    Task DeleteTopic(int id);
    Task<List<Category>> GetAllWithTopicsAsync();
}