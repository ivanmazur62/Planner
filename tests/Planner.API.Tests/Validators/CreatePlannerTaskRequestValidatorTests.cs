using Planner.API.Models.Requests;
using Planner.API.Validators;

namespace Planner.API.Tests.Validators;

public class CreatePlannerTaskRequestValidatorTests
{
    private readonly CreatePlannerTaskRequestValidator _validator = new();

    [Fact]
    public void ValidRequest_ShouldNotHaveErrors()
    {
        var request = new CreatePlannerTaskRequest("Valid Title", "Description", DateTime.UtcNow.AddDays(1));
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Title_WhenEmpty_ShouldHaveError()
    {
        var request = new CreatePlannerTaskRequest("", "Desc", null);
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Title_WhenExceeds200Characters_ShouldHaveError()
    {
        var request = new CreatePlannerTaskRequest(new string('a', 201), "Desc", null);
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Description_WhenExceeds2000Characters_ShouldHaveError()
    {
        var request = new CreatePlannerTaskRequest("Title", new string('a', 2001), null);
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }

    [Fact]
    public void Description_WhenNull_ShouldNotHaveError()
    {
        var request = new CreatePlannerTaskRequest("Title", null, null);
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }
}
