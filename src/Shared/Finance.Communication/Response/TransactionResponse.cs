
namespace Finance.Communication.Response; 
public class TransactionResponse {
    public Guid SenderWalletId { get; set; }
    public Guid ReceiverWalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Finance.Domain.Enum.TransactionStatus StatusTransaction { get; set; }
}
