using Finance.Domain.Shared;

namespace Finance.Domain.Events;
public record WalletDebitedEvent(Guid WalletId, decimal Amount, string Description,string? currency,Guid? TransactionId = null) : IDomainEvent;
