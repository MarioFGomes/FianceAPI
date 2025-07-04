using System;
using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Domain.Events;
using Finance.Exception.Exceptions;
using Xunit;

namespace Finance.Domain.Test;

public class WalletTests
{
    [Fact]
    public void Credit_Increases_Balance_And_Adds_Event()
    {
        var wallet = new Wallet { Balance = 100, currency = "BRL", UserId = Guid.NewGuid() };
        wallet.Credit(50, "Depósito", "BRL");
        Assert.Equal(150, wallet.Balance);
        Assert.Contains(wallet.DomainEvents, e => e is WalletCreditedEvent);
    }

    [Fact]
    public void Debit_Decreases_Balance_And_Adds_Event()
    {
        var wallet = new Wallet { Balance = 100, currency = "BRL", UserId = Guid.NewGuid() };
        wallet.Debit(40, "Saque", "BRL");
        Assert.Equal(60, wallet.Balance);
        Assert.Contains(wallet.DomainEvents, e => e is WalletDebitedEvent);
    }

    [Fact]
    public void Debit_Throws_When_Insufficient_Balance()
    {
        var wallet = new Wallet { Balance = 30, currency = "BRL", UserId = Guid.NewGuid() };
        Assert.Throws<BusinessException>(() => wallet.Debit(50, "Saque", "BRL"));
    }
} 