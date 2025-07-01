using System.Transactions;

namespace Finance.Domain.Entities; 
public class Transaction:BaseEntity {
    public Guid SenderWalletId { get; set; }
    public Guid ReceiverWalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public TransactionStatus TransactionStatus { get; set; }

}
