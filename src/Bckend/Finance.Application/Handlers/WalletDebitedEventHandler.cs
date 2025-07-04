using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Domain.Events;
using Finance.Domain.Repositories;
using Finance.Domain.Shared;

namespace Finance.Application.Handlers;
public class WalletDebitedEventHandler : IDomainEventHandler<WalletDebitedEvent> {
    
    private readonly IWalletMovementRepository _repository;

    public WalletDebitedEventHandler(IWalletMovementRepository repository) {
        _repository = repository;
    }

    public async Task Handle(WalletDebitedEvent @event) {
       
        var movement = new WalletMovement {
            Id = Guid.NewGuid(),
            WalletId = @event.WalletId,
            MovimentType = MovimentType.DEBIT,
            Amount = @event.Amount,
            currency= @event.currency,
            Description = @event.Description,
            TransactionId = @event.TransactionId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddOneAsync(movement);
    }
}
