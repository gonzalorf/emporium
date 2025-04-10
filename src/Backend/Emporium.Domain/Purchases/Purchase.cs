using Emporium.Domain.Providers;

namespace Emporium.Domain.Purchases;

public class Purchase : AuditableEntity<PurchaseId>, IAggregateRoot
{
    public DateOnly Date { get; private set;}
    public ProviderId ProviderId { get; private set; }

    private Purchase(){}
}
