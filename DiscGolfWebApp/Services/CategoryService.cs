using Skvaller.Models;
using Skvaller.Data;
using Microsoft.EntityFrameworkCore;
using Skvaller.Repositories;
namespace Skvaller.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<Category>> GetAllWithTopicsAsync()
    {
        return await categoryRepository.GetAllWithTopicsAsync();
    }

    public async Task AddCategoryAsync(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var newCategory = new Category()
            {
                Name = name
            };

            await categoryRepository.AddAsync(newCategory);
        }

    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            await categoryRepository.DeleteAsync(category);
        }
    }

    public async Task AddTopicAsync(int categoryId, string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var newTopic = new Topic()
            {
                Name = name,
                CategoryId = categoryId
            };

            await categoryRepository.AddTopicAsync(newTopic);
        }

    }

    public async Task DeleteTopic(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            await categoryRepository.DeleteAsync(category);
        }
    }
}


