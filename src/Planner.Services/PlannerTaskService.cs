using Microsoft.Extensions.Logging;
using Planner.Core.Entities;
using Planner.Core.Interfaces;
using Planner.Services.Interfaces;

namespace Planner.Services;

public sealed class PlannerTaskService(
    IPlannerTaskRepository taskRepository, 
    ICurrentUserService currentUser,
    ILogger<PlannerTaskService> logger) : IPlannerTaskService
{
    private string GetUserId()
    {
        var userId = currentUser.UserId;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User is not authenticated");
        return userId;
    }

    public async Task<PlannerTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        return await taskRepository.GetByIdAsync(id, userId, cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        return await taskRepository.GetAllAsync(userId, cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetCompletedAsync(CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        return await taskRepository.GetCompletedAsync(userId, cancellationToken);
    }

    public async Task<IReadOnlyList<PlannerTask>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        return await taskRepository.GetPendingAsync(userId, cancellationToken);
    }

    public async Task<PlannerTask> CreateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        plannerTask.UserId = userId;
        plannerTask.Id = Guid.NewGuid();
        plannerTask.CreatedAt = DateTime.UtcNow;
        var created = await taskRepository.AddAsync(plannerTask, cancellationToken);
        logger.LogInformation("Task created. TaskId={TaskId}, UserId={UserId}, Title={Title}",
            created.Id, userId, created.Title);
        return created;
    }

    public async Task<PlannerTask> UpdateAsync(PlannerTask plannerTask, CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        if (plannerTask.UserId != userId)
            throw new UnauthorizedAccessException("You can only update your own tasks");

        plannerTask.UpdatedAt = DateTime.UtcNow;
        var updated = await taskRepository.UpdateAsync(plannerTask, cancellationToken);
        logger.LogInformation("Task updated. TaskId={TaskId}, UserId={UserId}", updated.Id, userId);
        return updated;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        await taskRepository.DeleteAsync(id, userId, cancellationToken);
        logger.LogInformation("Task deleted. TaskId={TaskId}, UserId={UserId}", id, userId);
    }
}