using Skvaller.Models;

namespace Skvaller.Repositories;

public interface IReportRepository
{
    Task AddAsync(Report report);
    Task<bool> CheckIfExistsAsync(int postId, string reporterId);
    Task<Report?> GetByIdAsync(int id);
    Task<List<Report>> GetUnhandledAsync();
    Task UpdateAsync(Report report);
}