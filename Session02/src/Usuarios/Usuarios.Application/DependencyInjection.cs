using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR( c =>
        {
            c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<NombreUsuarioService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}