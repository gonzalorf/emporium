using Emporium.Domain.Orders;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;
public class OrderRepository : IOrderRepository
{
    public Task Add(Order order)
    {
        throw new NotImplementedException();
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
