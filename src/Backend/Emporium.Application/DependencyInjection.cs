using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Emporium.Application;
public static class DependencyInjection
{
    // https://github.com/martinothamar/Mediator

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        _ = services.AddMediatR(configuration =>
        {
            _ = configuration.RegisterServicesFromAssemblies(assembly);
        });

        _ = services.AddValidatorsFromAssembly(assembly);

        services.AddMapster();

        return services;
    }
}
