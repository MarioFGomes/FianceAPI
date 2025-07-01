

using Finance.Domain.Enum;

namespace Finance.Domain.Entities; 
public class WalletMovement:BaseEntity {
    public Guid WalletId { get; set; }
    public MovimentType MovimentType { get; set; }
    public decimal Amount { get; set; }
    public Guid TransactionId { get; set; }
    public string? Description { get; set; }
    public virtual Wallet Wallet { get; set; }
    public virtual Transaction Transaction { get; set; }
}
