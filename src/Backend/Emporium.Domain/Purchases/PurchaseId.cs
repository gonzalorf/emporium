using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Purchases;

public record PurchaseId(Guid Value) : TypedIdValueBase(Value);