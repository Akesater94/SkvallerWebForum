using Skvaller.Data;
using Skvaller.Models;

namespace Skvaller.Services;

public interface IMessageService
{
    Task<List<PrivateMessage>> GetInboxAsync(string userId);
    Task<PrivateMessage?> GetMessageAsync(int messageId, string userId);
    Task<List<ApplicationUser>> GetPossibleRecipientsAsync(string userId);
    Task<List<PrivateMessage>> GetSentAsync(string userId);
    Task<bool> SendMessageAsync(string senderId, string receiverId, string content);
}