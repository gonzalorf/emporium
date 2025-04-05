using Emporium.Domain.Products;
using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Purchases;

public class PurchaseDetail : Entity<PurchaseDetailId>
{
    public ProductId ProductId { get; private set; }
    public ProductVariantId? ProductVariantId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    private PurchaseDetail(){}
}
