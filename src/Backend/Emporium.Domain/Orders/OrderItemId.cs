using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Orders;
public record OrderItemId(Guid Value) : TypedIdValueBase(Value);