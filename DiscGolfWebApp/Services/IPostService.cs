using Skvaller.Models;

namespace Skvaller.Services;

public interface IPostService
{
    Task CreatePostAsync(int topicId, string authorId, string title, string content);
    Task<Post?> GetPostAsync(int postId);
    Task<List<Post>> GetPostsByTopicAsync(int topicId);
    Task<Topic?> GetTopicAsync(int id);
    Task<bool> DeletePostAsync(int postId, string userId, bool isAdmin);
}