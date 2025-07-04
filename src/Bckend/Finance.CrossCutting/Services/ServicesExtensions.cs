using Finance.Application.Interfaces;
using Finance.Application.Service.Cryptography;
using Finance.Domain.Security.Token;
using Finance.Domain.Shared;
using Finance.Infrastructure.Events;
using Finance.Infrastructure.Security.Token.Access;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.CrossCutting.Services;
public static class ServicesExtensions {

    public static void AddAplicationService(this IServiceCollection services, IConfiguration configurationManager) {
        AddAdditionalKeyPassword(services, configurationManager);
        AdicionarTokenJwt(services, configurationManager);
        AddSubDomainEvents(services);
    }


    private static void AddAdditionalKeyPassword(this IServiceCollection services, IConfiguration configurationManager) {
        
        var setion = configurationManager.GetSection("Settings:password:Encryptionkey");

        services.AddScoped(options => new PasswordEncryptor(setion.Value));

 
    }

    private static void AdicionarTokenJwt(this IServiceCollection services, IConfiguration configurationManager) {
        services.AddScoped<IAccessTokenGenerator>(provider => {

            var setionTime = configurationManager.GetSection("Settings:Jwt:expirationTimeMinute").Value;
            var setionKey = configurationManager.GetSection("Settings:Jwt:siginKey").Value;

            return new JwtTokenGenerator(Convert.ToUInt16(setionTime), setionKey);
        });
    }

    private static void AddSubDomainEvents(this IServiceCollection services) {
        
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.Scan(scan => scan
                .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
    }
}
