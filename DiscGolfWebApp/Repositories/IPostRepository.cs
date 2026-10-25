using Skvaller.Models;

namespace Skvaller.Repositories;

public interface IPostRepository
{
    Task AddAsync(Post post);
    Task DeleteAsync(Post post);
    Task<Post?> GetByIdAsync(int id);
    Task<List<Post>> GetByTopicAsync(int topicId);
}