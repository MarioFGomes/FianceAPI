using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Infrastructure.DataAcess.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Infrastructure.DataAcess; 
public class FinanceContext: DbContext {

    public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {

            var clrType = entityType.ClrType;

            if (typeof(BaseEntity).IsAssignableFrom(clrType)) {
                var parameter = Expression.Parameter(clrType, "e");
                var property = Expression.Property(parameter, typeof(BaseEntity).GetProperty(nameof(BaseEntity.Status))!);
                var activatedValue = Expression.Constant(BaseStatus.created);
                var equal = Expression.Equal(property, activatedValue);
                var lambda = Expression.Lambda(equal, parameter);

                modelBuilder.Entity(clrType).HasQueryFilter(lambda);
            }
        }

        SeedData.Seed(modelBuilder);
    }
}
