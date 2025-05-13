using Emporium.Domain.Orders;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;
public interface IOrderPartitionKeyProvider
{
    public string GetPartitionKey(Order order);
    public string GetPartitionKey(string id);
}
