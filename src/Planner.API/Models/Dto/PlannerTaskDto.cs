namespace Planner.API.Models.DTOs;

public record PlannerTaskDto(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);