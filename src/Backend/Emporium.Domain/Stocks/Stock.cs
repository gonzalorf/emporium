using Emporium.Domain.Products;
using Emporium.Domain.SeedWork;
using Emporium.Domain.Stocks.Validators;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Stocks;
public class Stock : AuditableEntity<StockId>, IAggregateRoot
{
    public ProductId ProductId { get; private set; }
    public VariantValueId? VariantValueId { get; private set; }
    public int Count { get; private set; }
    private Stock() : base() { }

    private Stock(StockId id, ProductId productId, VariantValueId? variantValueId, int count) : base(id)
    {
        ProductId = productId;
        VariantValueId = variantValueId;
        Count = count;
    }

    public static Stock CreateStock(ProductId productId, VariantValueId? variantValueId, int count)
    {
        var stock = new Stock(new StockId(Guid.NewGuid()), productId, variantValueId, count);
        StockValidator.ValidateStock(stock);
        return stock;
    }
}
