using Planner.API.Models;
using Planner.API.Validators;

namespace Planner.API.Tests.Validators;

public class LoginRequestValidatorTests
{
    private readonly LoginRequestValidator _validator = new();

    [Fact]
    public void ValidRequest_ShouldNotHaveErrors()
    {
        var request = new LoginRequest("user@example.com", "password123");
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Email_WhenEmpty_ShouldHaveError()
    {
        var request = new LoginRequest("", "password123");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid@")]
    public void Email_WhenInvalidFormat_ShouldHaveError(string email)
    {
        var request = new LoginRequest(email, "password123");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void Password_WhenEmpty_ShouldHaveError()
    {
        var request = new LoginRequest("user@example.com", "");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }
}
