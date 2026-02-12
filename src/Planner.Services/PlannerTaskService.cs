using Planner.Core.Entities;
using Planner.Core.Interfaces;
using Planner.Services.Interfaces;

namespace Planner.Services;

public sealed class PlannerTaskService(IPlannerTaskRepository taskRepository) : IPlannerTaskService
{
    public async Task<PlannerTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await taskRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await taskRepository.GetAllAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(CancellationToken cancellationToken = default)
    {
        return await taskRepository.GetCompletedAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        return await taskRepository.GetPendingAsync(cancellationToken);
    }

    public async Task<PlannerTask> CreateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default)
    {
        plannerTask.Id = Guid.NewGuid();
        plannerTask.CreatedAt = DateTime.UtcNow;
        return await taskRepository.AddAsync(plannerTask, cancellationToken);
    }

    public async Task<PlannerTask> UpdateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default)
    {
        plannerTask.UpdatedAt = DateTime.UtcNow;
        return await taskRepository.UpdateAsync(plannerTask, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await taskRepository.DeleteAsync(id, cancellationToken);
    }
}