using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Infrastructure.DataAcess.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.DataAcess; 
public class FinanceContext: DbContext {

    public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceContext).Assembly);

        modelBuilder.Entity<User>().HasQueryFilter(u => u.Status == BaseStatus.created);
        modelBuilder.Entity<Wallet>().HasQueryFilter(w => w.Status == BaseStatus.created);
        modelBuilder.Entity<Transaction>().HasQueryFilter(u => u.Status == BaseStatus.created);
        modelBuilder.Entity<WalletMovement>().HasQueryFilter(w => w.Status == BaseStatus.created);

        SeedData.Seed(modelBuilder);
    }
}
