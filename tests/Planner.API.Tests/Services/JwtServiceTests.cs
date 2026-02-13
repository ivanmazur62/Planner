using Microsoft.Extensions.Configuration;
using Planner.API.Services;

namespace Planner.API.Tests.Services;

public class JwtServiceTests
{
    [Fact]
    public void GenerateToken_WithValidConfig_ReturnsNonEmptyToken()
    {
        // Arrange
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "12345678901234567890123456789012",
                ["Jwt:Issuer"] = "TestIssuer",
                ["Jwt:Audience"] = "TestAudience"
            })
            .Build();
        var service = new JwtService(config);

        // Act
        var token = service.GenerateToken("user-123", "user@example.com");

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void GenerateToken_WithDifferentUsers_ReturnsDifferentTokens()
    {
        // Arrange
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "12345678901234567890123456789012",
                ["Jwt:Issuer"] = "TestIssuer",
                ["Jwt:Audience"] = "TestAudience"
            })
            .Build();
        var service = new JwtService(config);

        // Act
        var token1 = service.GenerateToken("user-1", "user1@example.com");
        var token2 = service.GenerateToken("user-2", "user2@example.com");

        // Assert
        Assert.NotEqual(token1, token2);
    }

    [Fact]
    public void GenerateToken_WhenKeyNotConfigured_ThrowsInvalidOperationException()
    {
        // Arrange
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Issuer"] = "TestIssuer",
                ["Jwt:Audience"] = "TestAudience"
            })
            .Build();
        var service = new JwtService(config);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            service.GenerateToken("user-123", "user@example.com"));
        Assert.Contains("Jwt:Key", ex.Message);
    }
}
