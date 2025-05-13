using Emporium.Domain.Orders;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;
public class OrderPartitionKeyProvider : IOrderPartitionKeyProvider
{
    public string GetPartitionKey(Order order)
    {
        return order.Id.Value.ToString();
    }

    public string GetPartitionKey(string id)
    {
        return id;
    }
}
