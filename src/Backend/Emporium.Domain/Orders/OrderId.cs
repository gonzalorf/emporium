namespace Emporium.Domain.Orders;
public record OrderId(Guid Value) : TypedIdValueBase(Value);