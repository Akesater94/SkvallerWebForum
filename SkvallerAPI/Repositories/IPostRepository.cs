using SkvallerAPI.Models;

namespace SkvallerAPI.Repositories;

public interface IPostRepository
{
    Task<List<Post>> GetByTopicAsync(int topicId);
    Task<bool> TopicExistsAsync(int topicId);
}