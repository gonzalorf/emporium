using Emporium.Domain.Products;
using Emporium.Domain.SeedWork;
using Emporium.Domain.Stocks.Validators;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Stocks;
public class Stock : AuditableEntity<StockId>, IAggregateRoot
{
    public ProductId ProductId { get; private set; }
    public ProductVariantId? ProductVariantId { get; private set; }
    public int Count { get; private set; }
    private Stock() : base() { }

    private Stock(StockId id, ProductId productId, ProductVariantId? productVariantId, int count) : base(id)
    {
        ProductId = productId;
        ProductVariantId = productVariantId;
        Count = count;
    }

    public static Stock CreateStock(ProductId productId, ProductVariantId? productVariantId, int count)
    {
        var stock = new Stock(new StockId(Guid.NewGuid()), productId, productVariantId, count);
        StockValidator.ValidateStock(stock);
        return stock;
    }
}
