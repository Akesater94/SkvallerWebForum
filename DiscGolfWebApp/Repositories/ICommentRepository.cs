using Skvaller.Models;

namespace Skvaller.Repositories;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task<Comment?> GetByIdAsync(int id);
    Task<List<Comment>> GetByPostAsync(int postId);
}