using Microsoft.EntityFrameworkCore;
using Planner.Core.Entities;
using Planner.Core.Interfaces;
using Planner.Infrastructure.Data;

namespace Planner.Infrastructure.Repositories;

public sealed class PlannerTaskRepository(ApplicationDbContext context) : IPlannerTaskRepository
{
    public async Task<PlannerTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks.Where(x => x.IsCompleted).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        return await context.PlannerTasks.Where(x => !x.IsCompleted).ToListAsync(cancellationToken);
    }

    public async Task<PlannerTask> AddAsync(PlannerTask task, CancellationToken cancellationToken = default)
    {
        task.CreatedAt = DateTime.UtcNow;
        context.PlannerTasks.Add(task);
        await context.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task<PlannerTask> UpdateAsync(PlannerTask task, CancellationToken cancellationToken = default)
    {
        task.UpdatedAt = DateTime.UtcNow;
        context.PlannerTasks.Update(task);
        await context.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await GetByIdAsync(id, cancellationToken);
        if (task != null)
        {
            context.PlannerTasks.Remove(task);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}