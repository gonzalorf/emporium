using Emporium.Application.Configuration.Services;
using Emporium.Application.Services;
using Emporium.Domain.Orders;
using Emporium.Domain.Products;
using Emporium.Domain.Providers;
using Emporium.Domain.Variants;
using Emporium.Infrastructure.CosmosDB;
using Emporium.Infrastructure.Domain.CosmosDBRepositories;
using Emporium.Infrastructure.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Emporium.Infrastructure;
public static class DependencyInjection
{
    public static async Task<IServiceCollection> AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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



        // Crear instancia del cliente Cosmos DB
        var cosmosClient = new CosmosClient(configuration.GetSection("Cosmos")["Url"], configuration.GetSection("Cosmos")["Key"], new CosmosClientOptions()
        {
            // Opciones del cliente, por ejemplo, modo de conexión, serializador, etc.
            ApplicationName = "Emporium" // Buen hábito para identificar la aplicación en logs/métricas
        });
        Console.WriteLine("Cliente Cosmos DB creado.");

        // Obtener o crear la base de datos
        var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(configuration.GetSection("Cosmos")["Db"]);
        Console.WriteLine($"Base de datos '{database.Database.Id}' asegurada.");

        // --- Contenedor de Productos ---
        // Especificamos el nombre y la clave de partición.
        // Elegir una buena clave de partición es CRUCIAL para el rendimiento y costo.
        // "/id" es simple para demo, pero en un caso real podrías usar algo como "/categoriaId"
        // si consultas frecuentemente productos por categoría.
        var productContainerProperties = new ContainerProperties(id: configuration.GetSection("Cosmos")["Container"], partitionKeyPath: "/id");
        var productContainer = await database.Database.CreateContainerIfNotExistsAsync(productContainerProperties, throughput: 400); // 400 RU/s es el mínimo
        Console.WriteLine($"Contenedor '{productContainer.Container.Id}' asegurado.");

        //        _ = services.AddScoped<IContainerContext, CosmosContainerContext>();

        _ = services.AddSingleton(cosmosClient);
        _ = services.AddSingleton(database.Database);
        _ = services.AddSingleton<IContainerFactory, ContainerFactory>();

        _ = services.AddTransient<IDateTimeService, DateTimeService>();
        _ = services.AddScoped<IPasswordHashService, PasswordHashService>();

        _ = services.AddScoped<IUnitOfWork, UnitOfWork>();

        _ = services.AddScoped<IOrderRepository, OrderRepository>();

        _ = services.AddScoped<IProviderRepository, ProviderRepository>();

        _ = services.AddScoped<IProductRepository, ProductRepository>();
        _ = services.AddScoped<IVariantRepository, VariantRepository>();


//        _ = services.AddScoped<IEventRepository, EventRepository>();

        //_ = services.AddScoped<IProviderRepository, MockProviderRepository>();
        //_ = services.AddScoped<IProductRepository, MockProductRepository>();
        //_ = services.AddScoped<IOrderRepository, MockOrderRepository>();
        //_ = services.AddScoped<IStockRepository, MockStockRepository>();
        //_ = services.AddScoped<IVariantRepository, MockVariantRepository>();



        return services;
    }
}
