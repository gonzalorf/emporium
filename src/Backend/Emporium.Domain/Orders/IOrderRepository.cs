namespace Emporium.Domain.Orders;
public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order?> GetById(OrderId id);
    Task<Order[]> GetByDates(DateOnly from, DateOnly to);
}
