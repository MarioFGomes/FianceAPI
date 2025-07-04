

namespace Finance.Domain.Shared; 
public interface IHasDomainEvents 
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearEvents();
}
