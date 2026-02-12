using Planner.Core.Entities;

namespace Planner.Core.Interfaces;

public interface IPlannerTaskRepository
{
    Task<PlannerTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<PlannerTask> AddAsync(PlannerTask task, CancellationToken cancellationToken = default);
    Task<PlannerTask> UpdateAsync(PlannerTask task, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}