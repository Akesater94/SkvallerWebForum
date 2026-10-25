using Skvaller.Data;
using Skvaller.Models;
using Microsoft.EntityFrameworkCore;

namespace Skvaller.Repositories;

public class ReportRepository(ApplicationDbContext context) : IReportRepository
{
    public async Task<Report?> GetByIdAsync(int id)
    {
        return await context.Reports.FindAsync(id);
    }

    public async Task<List<Report>> GetUnhandledAsync()
    {
        return await context.Reports
            .Where(r => r.IsHandled == false)
            .Include(r => r.Reporter)
            .Include(r => r.Post)
                .ThenInclude(p => p!.Author)
            .OrderBy(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> CheckIfExistsAsync(int postId, string reporterId)
    {
        return await context.Reports
            .AnyAsync(r => r.PostId == postId && r.ReporterId == reporterId && r.IsHandled == false);
    }

    public async Task AddAsync(Report report)
    {
        context.Reports.Add(report);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Report report)
    {
        context.Reports.Update(report);
        await context.SaveChangesAsync();
    }

}
