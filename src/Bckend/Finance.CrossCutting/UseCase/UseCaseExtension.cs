using Finance.Application.UseCase.Auth.SingIn;
using Finance.Application.UseCase.Auth.SingUp;
using Finance.Application.UseCase.Transaction.Create;
using Finance.Application.UseCase.User.FetchUser;
using Finance.Application.UseCase.Wallet.Create;
using Finance.Application.UseCase.Wallet.Credite;
using Finance.Application.UseCase.Wallet.Debit;
using Finance.Application.UseCase.Wallet.Fetch;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.CrossCutting.UseCase; 
public static class UseCaseExtension {
    public static void AddAplicationUseCase(this IServiceCollection services) {
        AddUseCases(services);
    }

    public static void AddUseCases(this IServiceCollection services) {
        services.AddScoped<ISingUpUseCase, SingUpUseCase>()
                .AddScoped<ISingInUseCase, SingInUseCase>()
                .AddScoped<ICreateWalletUseCase, CreateWalletUseCase>()
                .AddScoped<ICrediteUseCase, CrediteUseCase>()
                .AddScoped<IDebitUseCase, DebitUseCase>()
                .AddScoped<ICreateTransitionUseCase, CreateTransitionUseCase>()
                .AddScoped<IFecthUserUseCase, FecthUserUseCase>()
                .AddScoped<IFetchWalletMov, FetchWalletMov>();


    }
}
