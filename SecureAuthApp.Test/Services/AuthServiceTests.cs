using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using SecureAuthApp.Infrastracture.Data;
using SecureAuthApp.Infrastructure.Services;
using Xunit;

namespace SecureAuthApp.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task RegisterAsync_ValidUser_ReturnsUserWithHashedPassword()
        {
            // Arrange: Set up the fake RAM database and mock configuration
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAuthDb")
                .Options;
            var dbContext = new AppDbContext(options);

            // We use Moq to create a fake configuration file
            var mockConfig = new Mock<IConfiguration>();
            var authService = new AuthService(dbContext, mockConfig.Object);

            // Act: Run the method just like a user clicking "Register"
            var result = await authService.RegisterAsync("TestRobot", "RobotPassword123");

            // Assert: Verify the security rules automatically
            Assert.NotNull(result);
            Assert.Equal("TestRobot", result.Username);

            // This mathematically proves the password was scrambled before saving!
            Assert.NotEqual("RobotPassword123", result.PasswordHash);
        }
    }
}