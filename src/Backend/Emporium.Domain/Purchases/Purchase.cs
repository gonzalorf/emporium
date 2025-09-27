using Emporium.Domain.Products;
using Emporium.Domain.Providers;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Purchases;

public class Purchase : AuditableEntity<PurchaseId>, IAggregateRoot
{
    public DateOnly Date { get; private set;}
    public ProviderId ProviderId { get; private set; }

    private Purchase(PurchaseId id)
    : base(id)
    {
    }
}
