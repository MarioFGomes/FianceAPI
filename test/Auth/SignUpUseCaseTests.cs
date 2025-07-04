using System;
using System.Threading.Tasks;
using Finance.Application.Service.Cryptography;
using Finance.Application.UseCase.Auth.SingUp;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.Security.Token;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.Auth;

public class SignUpUseCaseTests
{
    private readonly Mock<PasswordEncryptor> _mockPasswordEncryptor;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IAccessTokenGenerator> _mockTokenGenerator;
    private readonly SignUpUseCase _useCase;

    public SignUpUseCaseTests()
    {
        _mockPasswordEncryptor = new Mock<PasswordEncryptor>("testKey");
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenGenerator = new Mock<IAccessTokenGenerator>();
        _useCase = new SignUpUseCase(_mockPasswordEncryptor.Object, _mockUserRepository.Object, _mockTokenGenerator.Object);
    }

    [Fact]
    public async Task Execute_WithValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new SignUpRequest { Name = "Test User", Email = "test@example.com", Password = "123456" };
        var token = "jwt_token";

        _mockUserRepository.Setup(x => x.AnyAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(false);
        _mockPasswordEncryptor.Setup(x => x.Encrypt(request.Password)).Returns("hashedPassword");
        _mockUserRepository.Setup(x => x.AddOneAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _mockTokenGenerator.Setup(x => x.Generate(It.IsAny<Guid>())).Returns(token);

        // Act
        var result = await _useCase.Execute(request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Test User", result.Data.Nome);
        Assert.Equal(token, result.Data.Token);
    }

    [Fact]
    public async Task Execute_WithExistingEmail_ThrowsResourceAlreadyExistsException()
    {
        // Arrange
        var request = new SignUpRequest { Name = "Test User", Email = "test@example.com", Password = "123456" };

        _mockUserRepository.Setup(x => x.AnyAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _useCase.Execute(request));
    }
} 