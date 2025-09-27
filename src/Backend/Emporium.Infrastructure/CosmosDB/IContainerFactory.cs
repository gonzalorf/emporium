using Microsoft.Azure.Cosmos;

namespace Emporium.Infrastructure.CosmosDB;
public interface IContainerFactory
{
    /// <summary>
    ///     Returns a CosmosDbContainer wrapper
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Container GetContainer(string containerName);

    /// <summary>
    ///     Ensure the database is created
    /// </summary>
    /// <returns></returns>
    Task EnsureDbSetupAsync();
}
