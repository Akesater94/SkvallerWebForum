using Skvaller.Data;
using Skvaller.Models;
using Skvaller.Repositories;
namespace Skvaller.Services;

public class MessageService(IMessageRepository messageRepository, IUserRepository userRepository) : IMessageService
{
    public async Task<List<ApplicationUser>> GetPossibleRecipientsAsync(string userId)
    {
        return await userRepository.GetAllExceptAsync(userId);
    }

    public async Task<bool> SendMessageAsync(string senderId, string receiverId, string content)
    {
        if (string.IsNullOrWhiteSpace(content) || string.IsNullOrEmpty(receiverId))
        {
            return false;
        }

        if (senderId == receiverId)
        {
            return false;
        }

        var receiver = await userRepository.GetByIdAsync(receiverId);
        if (receiver == null)
        {
            return false;
        }

        var newMessage = new PrivateMessage()
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content,
            SentAt = DateTime.Now,
            IsRead = false
        };

        await messageRepository.AddAsync(newMessage);
        return true;
    }

    public async Task<List<PrivateMessage>> GetInboxAsync(string userId)
    {
        return await messageRepository.GetInboxAsync(userId);
    }

    public async Task<List<PrivateMessage>> GetSentAsync(string userId)
    {
        return await messageRepository.GetSentAsync(userId);
    }

    public async Task<PrivateMessage?> GetMessageAsync(int messageId, string userId)
    {
        var message = await messageRepository.GetByIdAsync(messageId);
        if (message == null)
        {
            return null;
        }

        if (message.SenderId != userId && message.ReceiverId != userId)
        {
            return null;
        }

        if (message.ReceiverId == userId && !message.IsRead)
        {
            message.IsRead = true;
            await messageRepository.UpdateAsync(message);
        }

        return message;
    }
}
