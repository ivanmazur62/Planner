namespace Planner.API.Models.Requests;

public record UpdatePlannerTaskRequest(
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted
);