using Skvaller.Data;

namespace Skvaller.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(string userId);
    Task UpdateAsync(ApplicationUser user);
    Task<List<ApplicationUser>> GetAllExceptAsync(string userId);
}