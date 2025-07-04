using Finance.Domain.Enum;
using Finance.Domain.Events;
using Finance.Domain.Shared;
using Finance.Exception.Exceptions;

namespace Finance.Domain.Entities; 
public class Wallet: BaseEntity, IHasDomainEvents {
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public decimal Balance {get;set; }
    public string currency { get; set; }
    public WalletStatus WalletStatus { get; set; } = WalletStatus.Enabled;
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public void Credit(decimal amount, string description,string currency,  Guid? transactionId = null) {
        Balance += amount;
        AddDomainEvent(new WalletCreditedEvent(this.Id, amount, description, currency, transactionId));
    }

    public void Debit(decimal amount, string description,string currency, Guid? transactionId = null) {
        
        if (Balance < amount)
            throw new BusinessException("Saldo insuficiente.");

        Balance -= amount;
        AddDomainEvent(new WalletDebitedEvent(this.Id, amount, description, currency, transactionId));
    }

    protected void AddDomainEvent(IDomainEvent @event)
       => _domainEvents.Add(@event);

    public void ClearEvents() => _domainEvents.Clear();
}
