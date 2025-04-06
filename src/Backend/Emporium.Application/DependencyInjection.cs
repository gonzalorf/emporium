using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Emporium.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        _ = services.AddMediatR(configuration =>
        {
            _ = configuration.RegisterServicesFromAssemblies(assembly);
        });

        _ = services.AddValidatorsFromAssembly(assembly);

        _ = services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
