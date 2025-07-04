using Finance.Domain.Enum;
using Finance.Exception.Exceptions;
using System.Transactions;

namespace Finance.Domain.Entities; 
public class Transaction:BaseEntity {
    public Guid SenderWalletId { get; set; }
    public Guid ReceiverWalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Enum.TransactionStatus StatusTransaction { get; set; } = Enum.TransactionStatus.Ongoing;

    public void TransferTo(Wallet sender, Wallet receiver, decimal amount) 
    {
        if(amount <= 0) throw new BusinessException("Valor inválido para transferência");
        if(sender.Balance<amount) throw new BusinessException("Saldo insuficiente");
        if(sender.Id==receiver.Id) throw new BusinessException("Não é possível tranferir para a propria conta");
    }

    public bool TryChangeState(TransactionAction action) {

        var newState = (StatusTransaction, action) switch {
            (Enum.TransactionStatus.Pending, TransactionAction.Cancel) => Enum.TransactionStatus.canceled,
            (Enum.TransactionStatus.Ongoing, TransactionAction.Cancel) => Enum.TransactionStatus.canceled,
            (Enum.TransactionStatus.Pending, TransactionAction.Execute) => Enum.TransactionStatus.Ongoing,
            (Enum.TransactionStatus.Ongoing, TransactionAction.Execute) => Enum.TransactionStatus.succeeded,
            _ => default
        };

     if (newState == default || newState == this.StatusTransaction) throw new BusinessException("Operação Invalida");

        this.StatusTransaction = newState;

        return true;
    }
}
