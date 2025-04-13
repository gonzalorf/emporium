using Emporium.Application.Services;
using Emporium.Domain.Orders;
using Emporium.Domain.Products;
using Emporium.Domain.Providers;
using Emporium.Domain.Stocks;
using Emporium.Domain.Variants;
using Emporium.Infrastructure.Domain;
using Emporium.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Emporium.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        _ = services.AddTransient<IDateTimeService, DateTimeService>();

        //_ = services.AddScoped<IUnitOfWork, UnitOfWork>();

        _ = services.AddScoped<IProviderRepository, MockProviderRepository>();
        _ = services.AddScoped<IProductRepository, MockProductRepository>();
        _ = services.AddScoped<IOrderRepository, MockOrderRepository>();
        _ = services.AddScoped<IStockRepository, MockStockRepository>();
        _ = services.AddScoped<IVariantRepository, MockVariantRepository>();


        _ = services.AddScoped<IPasswordHashService, PasswordHashService>();

        return services;
    }
}
