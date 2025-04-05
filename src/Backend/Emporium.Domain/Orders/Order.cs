using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Orders;
public class Order : Entity<OrderId>
{
    private readonly List<OrderItem> orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => orderItems.AsReadOnly();
    public DateTime Date { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Customer { get; private set; }
    public string CustomerEmail { get; private set; }
    public string CustomerTaxId { get; private set; }

    private Order() : base() { }

    private Order(OrderId id, DateTime orderDate, decimal totalAmount, string customer, string customerEmail, string customerTaxId) : base(id)
    {
        Date = orderDate;
        TotalAmount = totalAmount;
        Customer = customer;
        CustomerEmail = customerEmail;
        CustomerTaxId = customerTaxId;
    }

    public static Order CreateOrder(DateTime orderDate, decimal totalAmount, string customer, string customerEmail, string customerTaxId)
    {
        return new Order(new OrderId(Guid.NewGuid()), orderDate, totalAmount, customer, customerEmail, customerTaxId);
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        orderItems.Add(orderItem);
    }
}