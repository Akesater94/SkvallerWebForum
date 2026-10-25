using Skvaller.Data;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<ApplicationUser?> GetByIdAsync(string userId)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<List<ApplicationUser>> GetAllExceptAsync(string userId)
    {
        return await context.Users.Where(u => u.Id != userId).OrderBy(u => u.Name).ToListAsync();
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }



}
