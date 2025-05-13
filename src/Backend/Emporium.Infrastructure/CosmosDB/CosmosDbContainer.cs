using Microsoft.Azure.Cosmos;

namespace Emporium.Infrastructure.CosmosDB;
internal class CosmosDbContainer : ICosmosDbContainer
{
    public Container _container { get; }

    public CosmosDbContainer(CosmosClient cosmosClient,
                             string databaseName,
                             string containerName)
    {
        this._container = cosmosClient.GetContainer(databaseName, containerName);
    }
}
