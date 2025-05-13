using Emporium.Domain.Orders;
using Emporium.Infrastructure.Context;
using Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;
public class OrderRepository : IOrderRepository
{
    private readonly IContainerContext context;
    private readonly IOrderPartitionKeyProvider partitionKeyProvider;

    public OrderRepository(IContainerContext context, IOrderPartitionKeyProvider partitionKeyProvider)
    {
        this.context = context;
        this.partitionKeyProvider = partitionKeyProvider;
    }

    public Task Add(Order order)
    {
        var o = new DataObject<Order>(order.Id.Value.ToString()
            , partitionKeyProvider.GetPartitionKey(order)
            , nameof(Order)
            , order
            , null
            , -1
            , EntityState.Created);

        context.Add(o);
        return Task.CompletedTask;
    }

    public Task<Order[]> GetByDates(DateOnly from, DateOnly to)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetById(OrderId id)
    {
        throw new NotImplementedException();
    }
}
