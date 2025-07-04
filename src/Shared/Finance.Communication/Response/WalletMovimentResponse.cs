
using Finance.Domain.Enum;
using System.Diagnostics.Metrics;

namespace Finance.Communication.Response; 
public class WalletMovimentResponse {
    public decimal BalanceAfterMovement { get; set; }
    public string? Description { get; set; }
    public string? currency { get; set; }
    public Guid WalletId { get; set; }
    public MovimentType MovimentType { get; set; }
    public decimal Amount { get; set; }
    public Guid? TransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
}
