using System;
using System.Threading.Tasks;
using Finance.Application.Service.Cryptography;
using Finance.Application.UseCase.Auth.SingIn;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.Security.Token;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.Auth;

public class SignInUseCaseTests
{
    private readonly Mock<PasswordEncryptor> _mockPasswordEncryptor;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IAccessTokenGenerator> _mockTokenGenerator;
    private readonly SignInUseCase _useCase;

    public SignInUseCaseTests()
    {
        _mockPasswordEncryptor = new Mock<PasswordEncryptor>("testKey");
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenGenerator = new Mock<IAccessTokenGenerator>();
        _useCase = new SignInUseCase(_mockPasswordEncryptor.Object, _mockUserRepository.Object, _mockTokenGenerator.Object);
    }

    [Fact]
    public async Task Execute_WithValidCredentials_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new SignInRequest { Email = "test@example.com", Password = "123456" };
        var user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "test@example.com", Password = "hashedPassword" };
        var token = "jwt_token";

        _mockPasswordEncryptor.Setup(x => x.Encrypt(request.Password)).Returns("hashedPassword");
        _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(user);
        _mockTokenGenerator.Setup(x => x.Generate(user.Id)).Returns(token);

        // Act
        var result = await _useCase.Execute(request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Test User", result.Data.Name);
        Assert.Equal(token, result.Data.Token);
    }

    [Fact]
    public async Task Execute_WithInvalidCredentials_ThrowsResourceNotFoundException()
    {
        // Arrange
        var request = new SignInRequest { Email = "test@example.com", Password = "123456" };

        _mockPasswordEncryptor.Setup(x => x.Encrypt(request.Password)).Returns("hashedPassword");
        _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _useCase.Execute(request));
    }
} 