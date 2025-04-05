using Emporium.Domain.SeedWork;
using Emporium.Domain.Products;

namespace Emporium.Domain.Orders;
public class OrderItem : Entity<OrderItemId>
{
    public Product Product { get; private set; }
    public ProductVariant? ProductVariant { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    private OrderItem() : base() { }

    private OrderItem(OrderItemId id, Product product, ProductVariant? productVariant, int quantity, decimal price) : base(id)
    {
        Product = product;
        ProductVariant = productVariant;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItem CreateOrderItem(Product product, ProductVariant? productVariant, int quantity, decimal price)
    {
        return new OrderItem(new OrderItemId(Guid.NewGuid()), product, productVariant, quantity, price);
    }
}