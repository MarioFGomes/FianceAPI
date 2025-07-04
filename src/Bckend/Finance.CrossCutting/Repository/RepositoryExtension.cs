using Finance.Domain.Repositories;
using Finance.Infrastructure.DataAcess;
using Finance.Infrastructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.CrossCutting.Repository; 
public static class RepositoryExtension {
    public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager) {
        AddRepositories(services);
        AddContexto(services, configurationManager);
    }
    public static void AddRepositories(IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IWalletRepository, WalletRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<IWalletMovementRepository, WalletMovementRepository>();


    }

    public static void AddContexto(IServiceCollection services, IConfiguration configurationManager) {
        
        var conectionString = configurationManager.GetSection("ConnectionStrings:PostgreSQL").Value;

        services.AddDbContext<FinanceContext>(dbContextOptions => {
            dbContextOptions.UseNpgsql(conectionString, o => o.UseNodaTime());
            dbContextOptions.UseLazyLoadingProxies();

        });

    }
}
