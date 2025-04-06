using Emporium.Domain.Products;
using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Purchases;

public class PurchaseDetail : Entity<PurchaseDetailId>
{
    public ProductId ProductId { get; private set; }
    public VariantValueId? VariantValueId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    private PurchaseDetail(){}
}
