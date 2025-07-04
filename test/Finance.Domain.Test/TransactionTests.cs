using System;
using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Exception.Exceptions;
using Xunit;

namespace Finance.Domain.Test;

public class TransactionTests
{
    [Fact]
    public void TransferTo_Throws_When_Amount_Is_Negative_Or_Zero()
    {
        var sender = new Wallet { Balance = 100, Id = Guid.NewGuid() };
        var receiver = new Wallet { Balance = 50, Id = Guid.NewGuid() };
        var transaction = new Transaction();
        Assert.Throws<BusinessException>(() => transaction.TransferTo(sender, receiver, 0));
        Assert.Throws<BusinessException>(() => transaction.TransferTo(sender, receiver, -10));
    }

    [Fact]
    public void TransferTo_Throws_When_Insufficient_Balance()
    {
        var sender = new Wallet { Balance = 10, Id = Guid.NewGuid() };
        var receiver = new Wallet { Balance = 50, Id = Guid.NewGuid() };
        var transaction = new Transaction();
        Assert.Throws<BusinessException>(() => transaction.TransferTo(sender, receiver, 20));
    }

    [Fact]
    public void TransferTo_Throws_When_Same_Wallet()
    {
        var id = Guid.NewGuid();
        var sender = new Wallet { Balance = 100, Id = id };
        var receiver = new Wallet { Balance = 50, Id = id };
        var transaction = new Transaction();
        Assert.Throws<BusinessException>(() => transaction.TransferTo(sender, receiver, 10));
    }

    [Fact]
    public void TryChangeState_Changes_Status_Correctly()
    {
        var transaction = new Transaction { StatusTransaction = TransactionStatus.Pending };
        var result = transaction.TryChangeState(TransactionAction.Execute);
        Assert.True(result);
        Assert.Equal(TransactionStatus.Ongoing, transaction.StatusTransaction);
    }

    [Fact]
    public void TryChangeState_Throws_On_Invalid_Transition()
    {
        var transaction = new Transaction { StatusTransaction = TransactionStatus.succeeded };
        Assert.Throws<BusinessException>(() => transaction.TryChangeState(TransactionAction.Cancel));
    }
} 