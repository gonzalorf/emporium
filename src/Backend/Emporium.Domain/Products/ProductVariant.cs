using Emporium.Domain.Variants;

namespace Emporium.Domain.Products;
public class ProductVariant : Entity<ProductVariantId>
{
    private readonly List<VariantValueId> variantValueIds = new();
    public IReadOnlyCollection<VariantValueId> VariantValueIds => variantValueIds.AsReadOnly();
    public decimal Price { get; private set; }
    public decimal StrikethroughPrice { get; private set; }

    private ProductVariant(ProductVariantId id, List<VariantValueId> variantValueIds, decimal price, decimal strikethroughPrice)
        : base(id)
    {
        this.variantValueIds = variantValueIds;
        Price = price;
        StrikethroughPrice = strikethroughPrice;
    }

    public static ProductVariant CreateProductVariant(ProductVariantId id, List<VariantValueId> variantValueIds, decimal price, decimal strikethroughPrice)
    {
        return new ProductVariant(id, variantValueIds, price, strikethroughPrice);
    }

    public void UpdatePrices(decimal price, decimal strikethroughPrice)
    {
        Price = price;
        StrikethroughPrice = strikethroughPrice;
    }
}
