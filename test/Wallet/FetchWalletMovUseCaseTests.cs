using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Application.UseCase.Wallet.Fetch;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;
using Moq;
using Xunit;

namespace Finance.UseCases.Test.Wallet;

public class FetchWalletMovUseCaseTests
{
    private readonly Mock<IWalletMovementRepository> _mockWalletMovementRepository;
    private readonly Mock<IWalletRepository> _mockWalletRepository;
    private readonly FetchWalletMov _useCase;

    public FetchWalletMovUseCaseTests()
    {
        _mockWalletMovementRepository = new Mock<IWalletMovementRepository>();
        _mockWalletRepository = new Mock<IWalletRepository>();
        _useCase = new FetchWalletMov(_mockWalletMovementRepository.Object, _mockWalletRepository.Object);
    }

    [Fact]
    public async Task Execute_WithValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new WalletMovQueryRequest { StartDate = DateTime.Now.AddDays(-7), EndDate = DateTime.Now, Page = 1, PageSize = 10, currency = "BRL" };
        var userId = Guid.NewGuid();
        var wallet = new Wallet { Id = Guid.NewGuid(), UserId = userId, currency = "BRL" };
        var movements = new List<WalletMovement>
        {
            new WalletMovement { Id = Guid.NewGuid(), WalletId = wallet.Id, Amount = 100, currency = "BRL", Description = "Depósito", CreatedAt = DateTime.UtcNow }
        };

        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync(wallet);
        _mockWalletMovementRepository.Setup(x => x.GetQueryable()).Returns(movements.AsQueryable());

        // Act
        var result = await _useCase.Execute(request, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Fact]
    public async Task Execute_WithNonExistentWallet_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new WalletMovQueryRequest { StartDate = DateTime.Now.AddDays(-7), EndDate = DateTime.Now, Page = 1, PageSize = 10, currency = "BRL" };
        var userId = Guid.NewGuid();

        _mockWalletRepository.Setup(x => x.GetOneAsync(It.IsAny<Func<Wallet, bool>>())).ReturnsAsync((Wallet)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.Execute(request, userId));
    }
} 