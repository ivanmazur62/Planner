using Microsoft.Extensions.Logging;
using Planner.Core.Entities;
using Planner.Core.Interfaces;
using Planner.Services;
using Moq;

namespace Planner.API.Tests.Services;

public class PlannerTaskServiceTests
{
    private const string TestUserId = "test-user-id";

    private static Mock<ILogger<PlannerTaskService>> CreateLoggerMock()
    {
        return new Mock<ILogger<PlannerTaskService>>();
    }
    
    private static Mock<ICurrentUserService> CreateCurrentUserMock()
    {
        var mock = new Mock<ICurrentUserService>();
        mock.Setup(c => c.UserId).Returns(TestUserId);
        mock.Setup(c => c.IsAuthenticated).Returns(true);
        return mock;
    }

    [Fact]
    public async Task GetAllAsync_WhenRepositoryEmpty_ReturnEmptyList()
    {
        //Arrange
        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.GetAllAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<PlannerTask>());
        
        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        //Act
        var result = await service.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTaskExists_ReturnsTask()
    {
        //Arrange
        var taskId = Guid.NewGuid();
        var expectedTask = new PlannerTask
        {
            Id = taskId,
            Title = "Test Task",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UserId = TestUserId,
        };

        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.GetByIdAsync(taskId, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTask);

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        //Act
        var result = await service.GetByIdAsync(taskId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);
        Assert.Equal(expectedTask.Title, result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTaskNotFound_ReturnsNull()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.GetByIdAsync(taskId, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PlannerTask?)null);

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act
        var result = await service.GetByIdAsync(taskId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_SetsIdCreatedAtAndUserId()
    {
        // Arrange
        var taskToCreate = new PlannerTask
        {
            Id = Guid.Empty,
            Title = "New Task",
            CreatedAt = default,
        };

        PlannerTask? capturedTask = null;
        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.AddAsync(It.IsAny<PlannerTask>(), It.IsAny<CancellationToken>()))
            .Callback<PlannerTask, CancellationToken>((task, _) => capturedTask = task)
            .ReturnsAsync((PlannerTask t, CancellationToken _) => t);

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act
        await service.CreateAsync(taskToCreate);

        // Assert
        Assert.NotNull(capturedTask);
        Assert.NotEqual(Guid.Empty, capturedTask.Id);
        Assert.NotEqual(default, capturedTask.CreatedAt);
        Assert.Equal(TestUserId, capturedTask.UserId);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var repository = new Mock<IPlannerTaskRepository>();

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act
        await service.DeleteAsync(taskId);

        // Assert
        repository.Verify(
            r => r.DeleteAsync(taskId, TestUserId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetCompletedAsync_ReturnsCompletedTasks()
    {
        // Arrange
        var completedTasks = new List<PlannerTask>
        {
            new() { Id = Guid.NewGuid(), Title = "Done", IsCompleted = true, UserId = TestUserId, CreatedAt = DateTime.UtcNow }
        };
        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.GetCompletedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(completedTasks);

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act
        var result = await service.GetCompletedAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.True(result[0].IsCompleted);
    }

    [Fact]
    public async Task GetPendingAsync_ReturnsPendingTasks()
    {
        // Arrange
        var pendingTasks = new List<PlannerTask>
        {
            new() { Id = Guid.NewGuid(), Title = "Todo", IsCompleted = false, UserId = TestUserId, CreatedAt = DateTime.UtcNow }
        };
        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.GetPendingAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pendingTasks);

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act
        var result = await service.GetPendingAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.False(result[0].IsCompleted);
    }

    [Fact]
    public async Task UpdateAsync_WhenTaskBelongsToUser_UpdatesSuccessfully()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new PlannerTask
        {
            Id = taskId,
            Title = "Updated Title",
            Description = "Updated Desc",
            UserId = TestUserId,
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };
        var repository = new Mock<IPlannerTaskRepository>();
        repository
            .Setup(r => r.UpdateAsync(It.IsAny<PlannerTask>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PlannerTask t, CancellationToken _) => t);

        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act
        var result = await service.UpdateAsync(task);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);
        Assert.NotEqual(default, result.UpdatedAt);
    }

    [Fact]
    public async Task UpdateAsync_WhenTaskBelongsToAnotherUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new PlannerTask
        {
            Id = taskId,
            Title = "Other User Task",
            UserId = "other-user-id",
            CreatedAt = DateTime.UtcNow
        };
        var repository = new Mock<IPlannerTaskRepository>();
        var service = new PlannerTaskService(repository.Object, CreateCurrentUserMock().Object, CreateLoggerMock().Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.UpdateAsync(task));
        Assert.Equal("You can only update your own tasks", ex.Message);
        repository.Verify(r => r.UpdateAsync(It.IsAny<PlannerTask>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetAllAsync_WhenUserNotAuthenticated_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var currentUserMock = new Mock<ICurrentUserService>();
        currentUserMock.Setup(c => c.UserId).Returns((string?)null);
        var repository = new Mock<IPlannerTaskRepository>();
        var service = new PlannerTaskService(repository.Object, currentUserMock.Object, CreateLoggerMock().Object);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.GetAllAsync());
    }

    [Fact]
    public async Task CreateAsync_WhenUserNotAuthenticated_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var currentUserMock = new Mock<ICurrentUserService>();
        currentUserMock.Setup(c => c.UserId).Returns((string?)null);
        var repository = new Mock<IPlannerTaskRepository>();
        var task = new PlannerTask { Title = "Test", UserId = "" };
        var service = new PlannerTaskService(repository.Object, currentUserMock.Object, CreateLoggerMock().Object);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.CreateAsync(task));
    }
}