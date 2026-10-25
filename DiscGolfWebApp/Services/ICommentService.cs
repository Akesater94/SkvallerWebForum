using Skvaller.Models;

namespace Skvaller.Services;

public interface ICommentService
{
    Task AddCommentAsync(int postId, string authorId, string content);
    Task<List<Comment>> GetCommentsByPostAsync(int postId);
    Task<bool> DeleteCommentAsync(int commentId, string userId, bool isAdmin);
}