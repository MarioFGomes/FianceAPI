using Finance.Application.UseCase.Auth.SingIn;
using Finance.Application.UseCase.Auth.SingUp;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.CrossCutting.UseCase; 
public static class UseCaseExtension {
    public static void AddAplicationUseCase(this IServiceCollection services) {
        AddUseCases(services);
    }

    public static void AddUseCases(this IServiceCollection services) {
        services.AddScoped<ISingUpUseCase, SingUpUseCase>()
                .AddScoped<ISingInUseCase, SingInUseCase>();


    }
}
