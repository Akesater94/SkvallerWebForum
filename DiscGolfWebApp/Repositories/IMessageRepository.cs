using Skvaller.Models;

namespace Skvaller.Repositories;

public interface IMessageRepository
{
    Task AddAsync(PrivateMessage message);
    Task<PrivateMessage?> GetByIdAsync(int id);
    Task<List<PrivateMessage>> GetInboxAsync(string userId);
    Task<List<PrivateMessage>> GetSentAsync(string userId);
    Task UpdateAsync(PrivateMessage message);
}