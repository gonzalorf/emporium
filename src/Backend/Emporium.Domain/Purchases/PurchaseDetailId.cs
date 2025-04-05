using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Purchases;

public record PurchaseDetailId(Guid Value) : TypedIdValueBase(Value);