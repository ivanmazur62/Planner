using Planner.API.Models.DTOs;
using Planner.API.Models.Mappings;
using Planner.API.Models.Requests;
using Planner.Core.Entities;

namespace Planner.API.Tests.Models;

public class PlannerTaskMappingsTests
{
    [Fact]
    public void ToDto_MapsAllFieldsCorrectly()
    {
        // Arrange
        var task = new PlannerTask
        {
            Id = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Test Description",
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = true,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow,
            UserId = "user-1"
        };

        // Act
        var dto = task.ToDto();

        // Assert
        Assert.Equal(task.Id, dto.Id);
        Assert.Equal(task.Title, dto.Title);
        Assert.Equal(task.Description, dto.Description);
        Assert.Equal(task.DueDate, dto.DueDate);
        Assert.Equal(task.IsCompleted, dto.IsCompleted);
        Assert.Equal(task.CreatedAt, dto.CreatedAt);
        Assert.Equal(task.UpdatedAt, dto.UpdatedAt);
    }

    [Fact]
    public void ToDto_WithNullOptionalFields_MapsCorrectly()
    {
        // Arrange
        var task = new PlannerTask
        {
            Id = Guid.NewGuid(),
            Title = "Minimal Task",
            Description = null,
            DueDate = null,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            UserId = "user-1"
        };

        // Act
        var dto = task.ToDto();

        // Assert
        Assert.Equal(task.Id, dto.Id);
        Assert.Equal(task.Title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.DueDate);
        Assert.False(dto.IsCompleted);
        Assert.Null(dto.UpdatedAt);
    }

    [Fact]
    public void ToEntity_FromCreatePlannerTaskRequest_MapsCorrectly()
    {
        // Arrange
        var request = new CreatePlannerTaskRequest(
            Title: "New Task",
            Description: "New Description",
            DueDate: DateTime.UtcNow.AddDays(2));

        // Act
        var entity = request.ToEntity();

        // Assert
        Assert.Equal("New Task", entity.Title);
        Assert.Equal("New Description", entity.Description);
        Assert.Equal(request.DueDate, entity.DueDate);
        Assert.False(entity.IsCompleted);
        Assert.Equal("", entity.UserId);
    }

    [Fact]
    public void ToEntity_WithNullOptionalFields_MapsCorrectly()
    {
        // Arrange
        var request = new CreatePlannerTaskRequest(
            Title: "Minimal",
            Description: null,
            DueDate: null);

        // Act
        var entity = request.ToEntity();

        // Assert
        Assert.Equal("Minimal", entity.Title);
        Assert.Null(entity.Description);
        Assert.Null(entity.DueDate);
        Assert.False(entity.IsCompleted);
    }
}
