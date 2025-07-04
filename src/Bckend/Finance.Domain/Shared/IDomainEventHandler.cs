
namespace Finance.Domain.Shared;
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent 
{
    Task Handle(TEvent domainEvent);
}
