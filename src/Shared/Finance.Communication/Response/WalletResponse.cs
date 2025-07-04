using Finance.Domain.Enum;

namespace Finance.Communication.Response; 
public class WalletResponse {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public string currency { get; set; }
    public WalletStatus WalletStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public BaseStatus Status { get; set; }
}
