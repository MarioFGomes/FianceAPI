
using Finance.Application.Interfaces;
using Finance.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Infrastructure.Events;
public class DomainEventDispatcher : IDomainEventDispatcher {
    
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(IDomainEvent domainEvent) {
        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers) {
            var method = handlerType.GetMethod("Handle");

            if (method != null) {
                var task = (Task)method.Invoke(handler, new object[] { domainEvent });
                await task;
            }
        }
    }
}
