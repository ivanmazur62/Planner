using Planner.API.Models.Requests;
using Planner.API.Validators;

namespace Planner.API.Tests.Validators;

public class UpdatePlannerTaskRequestValidatorTests
{
    private readonly UpdatePlannerTaskRequestValidator _validator = new();

    [Fact]
    public void ValidRequest_ShouldNotHaveErrors()
    {
        var request = new UpdatePlannerTaskRequest("Valid Title", "Description", DateTime.UtcNow.AddDays(1), false);
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Title_WhenEmpty_ShouldHaveError()
    {
        var request = new UpdatePlannerTaskRequest("", "Desc", null, false);
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Title_WhenExceeds200Characters_ShouldHaveError()
    {
        var request = new UpdatePlannerTaskRequest(new string('a', 201), "Desc", null, false);
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Description_WhenExceeds2000Characters_ShouldHaveError()
    {
        var request = new UpdatePlannerTaskRequest("Title", new string('a', 2001), null, false);
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }
}
