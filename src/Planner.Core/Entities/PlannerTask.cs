namespace Planner.Core.Entities;

public sealed class PlannerTask
{
    public Guid      Id          { get; set; }
    public string    Title       { get; set; } =  string.Empty;
    public string?   Description { get; set; }
    public DateTime? DueDate     { get; set; }
    public bool      IsCompleted { get; set; }
    public DateTime  CreatedAt   { get; set; }
    public DateTime? UpdatedAt   { get; set; }
    public string    UserId      { get; set; }
}