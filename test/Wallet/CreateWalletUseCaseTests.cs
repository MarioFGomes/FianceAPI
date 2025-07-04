using System;
using System.Threading.Tasks;
using Finance.Application.UseCase.Wallet.Create;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.Wallet;

public class CreateWalletUseCaseTests
{
    private readonly Mock<IWalletRepository> _mockWalletRepository;
    private readonly CreateWalletUseCase _useCase;

    public CreateWalletUseCaseTests()
    {
        _mockWalletRepository = new Mock<IWalletRepository>();
        _useCase = new CreateWalletUseCase(_mockWalletRepository.Object);
    }

    [Fact]
    public async Task Execute_WithValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new WalletRequest { Balance = 100, currency = "BRL" };
        var userId = Guid.NewGuid();

        _mockWalletRepository.Setup(x => x.AnyAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync(false);
        _mockWalletRepository.Setup(x => x.AddOneAsync(It.IsAny<Wallet>())).Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Execute(request, userId);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(100, result.Data.Balance);
        Assert.Equal("BRL", result.Data.currency);
        Assert.Equal(userId, result.Data.UserId);
    }

    [Fact]
    public async Task Execute_WithExistingWalletForUser_ThrowsResourceAlreadyExistsException()
    {
        // Arrange
        var request = new WalletRequest { Balance = 100, currency = "BRL" };
        var userId = Guid.NewGuid();

        _mockWalletRepository.Setup(x => x.AnyAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _useCase.Execute(request, userId));
    }
} 