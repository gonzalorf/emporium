using Emporium.Domain.Orders;
using System.Collections.Concurrent;

namespace Emporium.Infrastructure.Domain;

public class MockOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<OrderId, Order> _orders = new();

    public Task Add(Order order)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetById(OrderId id)
    {
        _orders.TryGetValue(id, out var order);
        return Task.FromResult(order);
    }

    public Task<Order[]> GetByDates(DateOnly from, DateOnly to)
    {
        var orders = _orders.Values
            .Where(order => order.Date >= from.ToDateTime(TimeOnly.MinValue)
                         && order.Date <= to.ToDateTime(TimeOnly.MaxValue))
            .ToArray();
        return Task.FromResult(orders);
    }
}
