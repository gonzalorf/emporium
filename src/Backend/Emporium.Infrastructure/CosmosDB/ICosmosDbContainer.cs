using Microsoft.Azure.Cosmos;

namespace Emporium.Infrastructure.CosmosDB;
internal interface ICosmosDbContainer
{
    /// <summary>
    ///     Instance of Azure Cosmos DB Container class
    /// </summary>
    Container _container { get; }
}
