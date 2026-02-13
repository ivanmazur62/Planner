namespace Planner.API.Models.Requests;

public record CreatePlannerTaskRequest(
    string Title, 
    string? Description, 
    DateTime? DueDate
);