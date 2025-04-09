using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mapster;

namespace Emporium.Application;
public static class DependencyInjection
{
    // https://github.com/martinothamar/Mediator

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        _ = services.AddMediator();

        _ = services.AddValidatorsFromAssembly(assembly);

        _ = services.AddMapster();

        return services;
    }
}
