using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products;
public class ProductVariant : Entity<ProductVariantId>
{
    public VariantValue VariantValue { get; private set; }
    public decimal Price { get; private set; }
    public decimal StrikethroughPrice { get; private set; }

    private ProductVariant() : base() { }

    private ProductVariant(ProductVariantId id, VariantValue variantValue, decimal price, decimal strikethroughPrice) : base(id)
    {
        VariantValue = variantValue;
        Price = price;
        StrikethroughPrice = strikethroughPrice;
    }

    public static ProductVariant CreateProductVariant(VariantValue variantValue, decimal price, decimal strikethroughPrice)
    {
        return new ProductVariant(new ProductVariantId(Guid.NewGuid()), variantValue, price, strikethroughPrice);
    }
}