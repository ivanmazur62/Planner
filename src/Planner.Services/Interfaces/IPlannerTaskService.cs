using Planner.Core.Entities;

namespace Planner.Services.Interfaces;

public interface IPlannerTaskService
{
    Task<PlannerTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlannerTask>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<PlannerTask> CreateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default);
    Task<PlannerTask> UpdateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}