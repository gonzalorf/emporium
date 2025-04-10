using Emporium.Domain.Products;

namespace Emporium.Domain.CartItems;
public class CartItem : Entity<CartItemId>
{
    public Product Product { get; private set; }
    public ProductVariant? Variant { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public CartItemStatus Status { get; private set; }

    private CartItem() : base() { }

    private CartItem(CartItemId id, Product product, ProductVariant? variant, int quantity, decimal price, CartItemStatus status) : base(id)
    {
        Product = product;
        Variant = variant;
        Quantity = quantity;
        Status = status;
        Price = price;
    }

    public static CartItem CreateCartItem(Product product, ProductVariant? variant, int quantity, decimal price)
    {
        return new CartItem(new CartItemId(Guid.NewGuid()), product, variant, quantity, price, CartItemStatus.Active);
    }
}

public enum CartItemStatus
{
    Active = 1,
    Purchased
}