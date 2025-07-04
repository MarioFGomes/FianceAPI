using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Domain.Events;
using Finance.Domain.Repositories;
using Finance.Domain.Shared;

namespace Finance.Application.Handlers;
public class WalletCreditedEventHandler : IDomainEventHandler<WalletCreditedEvent> {
    private readonly IWalletMovementRepository _repository;

    public WalletCreditedEventHandler(IWalletMovementRepository repository) {
        _repository = repository;
    }

    public async Task Handle(WalletCreditedEvent @event) {
        var movement = new WalletMovement {
            Id = Guid.NewGuid(),
            WalletId = @event.WalletId,
            MovimentType = MovimentType.CREDIT,
            Amount = @event.Amount,
            currency=@event.currency,
            Description = @event.Description,
            TransactionId = @event.TransactionId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddOneAsync(movement);
    }
}
