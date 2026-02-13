using Planner.Core.Entities;

namespace Planner.Core.Interfaces;

public interface IPlannerTaskRepository
{
    Task<PlannerTask?> GetByIdAsync(Guid id, string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetAllAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetPendingAsync(string userId, CancellationToken cancellationToken = default);
    Task<PlannerTask> AddAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default);
    Task<PlannerTask> UpdateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, string userId, CancellationToken cancellationToken = default);
}