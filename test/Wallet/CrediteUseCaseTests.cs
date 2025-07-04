using System;
using System.Threading.Tasks;
using Finance.Application.UseCase.Wallet.Credite;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.Wallet;

public class CrediteUseCaseTests
{
    private readonly Mock<IWalletRepository> _mockWalletRepository;
    private readonly CrediteUseCase _useCase;

    public CrediteUseCaseTests()
    {
        _mockWalletRepository = new Mock<IWalletRepository>();
        _useCase = new CrediteUseCase(_mockWalletRepository.Object);
    }

    [Fact]
    public async Task Execute_WithValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new WalletMovimentRequest { Amount = 50, Description = "Depósito", currency = "BRL" };
        var userId = Guid.NewGuid();
        var wallet = new Wallet { Id = Guid.NewGuid(), Balance = 100, currency = "BRL", UserId = userId };

        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync(wallet);
        _mockWalletRepository.Setup(x => x.ReplaceOneAsync(It.IsAny<Func<Wallet, bool>>(), It.IsAny<Wallet>())).Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Execute(request, userId);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(50, result.Data.Amount);
        Assert.Equal(150, result.Data.BalanceAfterMovement);
        Assert.Equal("BRL", result.Data.currency);
    }

    [Fact]
    public async Task Execute_WithNonExistentWallet_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new WalletMovimentRequest { Amount = 50, Description = "Depósito", currency = "BRL" };
        var userId = Guid.NewGuid();

        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync((Wallet)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.Execute(request, userId));
    }
} 