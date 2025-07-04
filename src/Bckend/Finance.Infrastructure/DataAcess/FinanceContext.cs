using Finance.Application.Interfaces;
using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Domain.Shared;
using Finance.Infrastructure.DataAcess.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.DataAcess; 
public class FinanceContext: DbContext {
   
    private readonly IDomainEventDispatcher _dispatcher;

    public FinanceContext(DbContextOptions<FinanceContext> options, IDomainEventDispatcher dispatcher) : base(options) {
        Database.EnsureCreated();
        _dispatcher = dispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceContext).Assembly);

        modelBuilder.Entity<User>().HasQueryFilter(u => u.Status == BaseStatus.created && u.UserStatus== UserStatus.Enabled);
        modelBuilder.Entity<Wallet>().HasQueryFilter(w => w.Status == BaseStatus.created && w.WalletStatus== WalletStatus.Enabled);
        modelBuilder.Entity<Transaction>().HasQueryFilter(u => u.Status == BaseStatus.created);
        modelBuilder.Entity<WalletMovement>().HasQueryFilter(w => w.Status == BaseStatus.created);

        SeedData.Seed(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
    {
        
        var entitiesWithEvents = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var entity in entitiesWithEvents) {
            var events = entity.DomainEvents.ToList();
            entity.ClearEvents();

            foreach (var @event in events)
                await _dispatcher.Dispatch(@event);
        }

        return result;
    }
}
