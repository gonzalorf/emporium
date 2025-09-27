using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Emporium.Infrastructure.CosmosDB;

public class ContainerFactory : IContainerFactory
{
    private readonly Database database; // Guardamos la instancia de la DB
    private readonly ConcurrentDictionary<string, Container> containers = new();

    public ContainerFactory(Database database)
    {
        // Obtenemos la referencia a la base de datos al inicio
        this.database = database;
    }

    public Task EnsureDbSetupAsync()
    {
        throw new NotImplementedException();
    }

    public Container GetContainer(string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
            throw new ArgumentNullException(nameof(containerName));

        // Intenta obtener del caché, si no existe, obtiene la referencia del cliente Cosmos.
        // Asumimos que el contenedor existe (debería ser creado al inicio de la aplicación).
        return containers.GetOrAdd(containerName, name => database.GetContainer(name));
    }
}