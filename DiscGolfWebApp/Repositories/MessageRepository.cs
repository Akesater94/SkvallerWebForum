using Skvaller.Data;
using Skvaller.Models;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Repositories;

public class MessageRepository(ApplicationDbContext context) : IMessageRepository
{
    public async Task<PrivateMessage?> GetByIdAsync(int id)
    {
        return await context.PrivateMessages.Include(m => m.Sender).Include(m => m.Receiver).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<PrivateMessage>> GetInboxAsync(string userId)
    {
        return await context.PrivateMessages.Where(m => m.ReceiverId == userId).Include(m => m.Sender).OrderByDescending(m => m.SentAt).ToListAsync();
    }

    public async Task<List<PrivateMessage>> GetSentAsync(string userId)
    {
        return await context.PrivateMessages.Where(m => m.SenderId == userId).Include(m => m.Receiver).OrderByDescending(m => m.SentAt).ToListAsync();
    }

    public async Task AddAsync(PrivateMessage message)
    {
        context.PrivateMessages.Add(message);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PrivateMessage message)
    {
        context.PrivateMessages.Update(message);
        await context.SaveChangesAsync();
    }
}
