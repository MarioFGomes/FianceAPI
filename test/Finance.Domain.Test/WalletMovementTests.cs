using System;
using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Xunit;

namespace Finance.Domain.Test;

public class WalletMovementTests
{
    [Fact]
    public void WalletMovement_Can_Be_Created_And_Properties_Set()
    {
        var walletId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();
        var movement = new WalletMovement
        {
            WalletId = walletId,
            MovimentType = MovimentType.CREDIT,
            Amount = 100.5m,
            currency = "BRL",
            TransactionId = transactionId,
            Description = "Teste de crédito"
        };
        Assert.Equal(walletId, movement.WalletId);
        Assert.Equal(MovimentType.CREDIT, movement.MovimentType);
        Assert.Equal(100.5m, movement.Amount);
        Assert.Equal("BRL", movement.currency);
        Assert.Equal(transactionId, movement.TransactionId);
        Assert.Equal("Teste de crédito", movement.Description);
    }
} 