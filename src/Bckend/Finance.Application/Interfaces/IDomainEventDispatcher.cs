using Finance.Domain.Shared;

namespace Finance.Application.Interfaces; 
public interface IDomainEventDispatcher {
    Task Dispatch(IDomainEvent domainEvent);
}
