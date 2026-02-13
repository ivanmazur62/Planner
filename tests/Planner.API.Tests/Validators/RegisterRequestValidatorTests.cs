using Planner.API.Models;
using Planner.API.Validators;

namespace Planner.API.Tests.Validators;

public class RegisterRequestValidatorTests
{
    private readonly RegisterRequestValidator _validator = new();

    [Fact]
    public void ValidRequest_ShouldNotHaveErrors()
    {
        var request = new RegisterRequest("user@example.com", "password123", "username");
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Email_WhenEmpty_ShouldHaveError()
    {
        var request = new RegisterRequest("", "password123", "username");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid@")]
    [InlineData("@domain.com")]
    public void Email_WhenInvalidFormat_ShouldHaveError(string email)
    {
        var request = new RegisterRequest(email, "password123", "username");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void Password_WhenEmpty_ShouldHaveError()
    {
        var request = new RegisterRequest("user@example.com", "", "username");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void Password_WhenShorterThan6Characters_ShouldHaveError()
    {
        var request = new RegisterRequest("user@example.com", "12345", "username");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void UserName_WhenEmpty_ShouldHaveError()
    {
        var request = new RegisterRequest("user@example.com", "password123", "");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName");
    }

    [Fact]
    public void UserName_WhenShorterThan2Characters_ShouldHaveError()
    {
        var request = new RegisterRequest("user@example.com", "password123", "a");
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName");
    }

    [Fact]
    public void UserName_WhenExceeds50Characters_ShouldHaveError()
    {
        var request = new RegisterRequest("user@example.com", "password123", new string('a', 51));
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName");
    }
}
