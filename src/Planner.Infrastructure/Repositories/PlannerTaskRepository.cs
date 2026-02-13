using Microsoft.EntityFrameworkCore;
using Planner.Core.Entities;
using Planner.Core.Interfaces;
using Planner.Infrastructure.Data;

namespace Planner.Infrastructure.Repositories;

public sealed class PlannerTaskRepository(ApplicationDbContext context) : IPlannerTaskRepository
{
    public async Task<PlannerTask?> GetByIdAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks
            .Where(t => t.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks
            .Where(t => t.UserId == userId && t.IsCompleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetPendingAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks
            .Where(t => t.UserId == userId && !t.IsCompleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<PlannerTask> AddAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default)
    {
        context.PlannerTasks.Add(plannerTask);
        await context.SaveChangesAsync(cancellationToken);
        return plannerTask;
    }

    public async Task<PlannerTask> UpdateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default)
    {
        context.PlannerTasks.Update(plannerTask);
        await context.SaveChangesAsync(cancellationToken);
        return plannerTask;
    }

    public async Task DeleteAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        var task = await GetByIdAsync(id, userId, cancellationToken);
        if (task != null)
        {
            context.PlannerTasks.Remove(task);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}