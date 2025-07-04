using System;
using System.Threading.Tasks;
using Finance.Application.UseCase.Transaction.Create;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.Transaction;

public class CreateTransactionUseCaseTests
{
    private readonly Mock<ITransactionRepository> _mockTransactionRepository;
    private readonly Mock<IWalletRepository> _mockWalletRepository;
    private readonly CreateTransitionUseCase _useCase;

    public CreateTransactionUseCaseTests()
    {
        _mockTransactionRepository = new Mock<ITransactionRepository>();
        _mockWalletRepository = new Mock<IWalletRepository>();
        _useCase = new CreateTransitionUseCase(_mockTransactionRepository.Object, _mockWalletRepository.Object);
    }

    [Fact]
    public async Task Execute_WithValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new TransactionRequest { ReceiverWalletId = Guid.NewGuid(), Amount = 50, Description = "Transferência", currency = "BRL" };
        var userId = Guid.NewGuid();
        var senderWallet = new Wallet { Id = Guid.NewGuid(), Balance = 100, currency = "BRL", UserId = userId };
        var receiverWallet = new Wallet { Id = Guid.NewGuid(), Balance = 50, currency = "BRL", UserId = request.ReceiverWalletId };

        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync(senderWallet);
        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync(receiverWallet);
        _mockTransactionRepository.Setup(x => x.AddOneAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Execute(request, userId);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(50, result.Data.Amount);
        Assert.Equal("Transferência", result.Data.Description);
    }

    [Fact]
    public async Task Execute_WithNonExistentWallets_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new TransactionRequest { ReceiverWalletId = Guid.NewGuid(), Amount = 50, Description = "Transferência", currency = "BRL" };
        var userId = Guid.NewGuid();

        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync((Wallet)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.Execute(request, userId));
    }
} 