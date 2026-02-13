using Planner.API.Models.DTOs;
using Planner.API.Models.Requests;
using Planner.Core.Entities;

namespace Planner.API.Models.Mappings;

public static class PlannerTaskMappings
{
    public static PlannerTaskDto ToDto(this PlannerTask plannerTask) =>
        new(
            plannerTask.Id,
            plannerTask.Title,
            plannerTask.Description,
            plannerTask.DueDate,
            plannerTask.IsCompleted,
            plannerTask.CreatedAt,
            plannerTask.UpdatedAt);

    public static PlannerTask ToEntity(this CreatePlannerTaskRequest request) =>
        new()
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            IsCompleted = false,
            UserId = ""
        };
}