using System;
using System.Threading.Tasks;
using Finance.Application.UseCase.User.FetchUser;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.User;

public class FetchUserUseCaseTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly FecthUserUseCase _useCase;

    public FetchUserUseCaseTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _useCase = new FecthUserUseCase(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Execute_WithValidSearch_ReturnsSuccessResponse()
    {
        // Arrange
        var search = "test@example.com";
        var user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "test@example.com" };

        _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(user);

        // Act
        var result = await _useCase.Execute(search);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Test User", result.Data.Name);
        Assert.Equal("test@example.com", result.Data.Email);
    }

    [Fact]
    public async Task Execute_WithEmptySearch_ThrowsInvalidOperationException()
    {
        // Arrange
        var search = "";

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.Execute(search));
    }

    [Fact]
    public async Task Execute_WithNonExistentUser_ThrowsResourceNotFoundException()
    {
        // Arrange
        var search = "nonexistent@example.com";

        _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _useCase.Execute(search));
    }
} 