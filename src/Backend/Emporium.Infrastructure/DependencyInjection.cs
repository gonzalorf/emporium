using Emporium.Application.Configuration.Services;
using Emporium.Application.Services;
using Emporium.Domain.Orders;
using Emporium.Domain.Products;
using Emporium.Domain.Providers;
using Emporium.Domain.SeedWork;
using Emporium.Domain.Stocks;
using Emporium.Domain.Variants;
using Emporium.Infrastructure.Context;
using Emporium.Infrastructure.Domain.CosmosDBRepositories;
using Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;
using Emporium.Infrastructure.Domain.MockRepositories;
using Emporium.Infrastructure.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;


namespace Emporium.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        var cOpts = new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions()
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                IgnoreNullValues = true
            }
        };
        var containers = new List<(string, string)>
        {
            (configuration.GetSection("Cosmos")["Db"], configuration.GetSection("Cosmos")["Container"])
        };
        var cosmosClient = CosmosClient.CreateAndInitializeAsync(configuration.GetSection("Cosmos")["Url"],
            configuration.GetSection("Cosmos")["Key"], containers, cOpts).Result;

        var container = cosmosClient.GetContainer(configuration.GetSection("Cosmos")["Db"],
            configuration.GetSection("Cosmos")["Container"]);

        _ = services.AddSingleton(container);

        _ = services.AddScoped<IContainerContext, CosmosContainerContext>();

        _ = services.AddTransient<IDateTimeService, DateTimeService>();
        _ = services.AddScoped<IPasswordHashService, PasswordHashService>();

        _ = services.AddScoped<IUnitOfWork, UnitOfWork>();

        _ = services.AddScoped<IOrderRepository, OrderRepository>();
        _ = services.AddSingleton<IOrderPartitionKeyProvider, OrderPartitionKeyProvider>();

        _ = services.AddScoped<IProviderRepository, ProviderRepository>();
        _ = services.AddScoped<IProviderPartitionKeyProvider, ProviderPartitionKeyProvider>();

        _ = services.AddScoped<IProductRepository, ProductRepository>();
        _ = services.AddScoped<IVariantRepository, VariantRepository>();


        _ = services.AddScoped<IEventRepository, EventRepository>();

        //_ = services.AddScoped<IProviderRepository, MockProviderRepository>();
        //_ = services.AddScoped<IProductRepository, MockProductRepository>();
        //_ = services.AddScoped<IOrderRepository, MockOrderRepository>();
        //_ = services.AddScoped<IStockRepository, MockStockRepository>();
        //_ = services.AddScoped<IVariantRepository, MockVariantRepository>();



        return services;
    }
}
