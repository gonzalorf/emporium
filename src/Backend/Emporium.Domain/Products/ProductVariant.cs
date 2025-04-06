using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products;
public class ProductVariant : ValueObject
{
    public VariantValueId VariantValueId { get; private set; }
    public decimal Price { get; private set; }
    public decimal StrikethroughPrice { get; private set; }

    private ProductVariant() : base() { }

    private ProductVariant(VariantValueId variantValueId, decimal price, decimal strikethroughPrice)
    {
        VariantValueId = variantValueId;
        Price = price;
        StrikethroughPrice = strikethroughPrice;
    }

    public static ProductVariant CreateProductVariant(VariantValueId variantValueId, decimal price, decimal strikethroughPrice)
    {
        return new ProductVariant(variantValueId, price, strikethroughPrice);
    }

    public void UpdatePrices(decimal price, decimal strikethroughPrice)
    {
        Price = price;
        StrikethroughPrice = strikethroughPrice;
    }
}