using Emporium.Domain.SeedWork;

namespace Emporium.Domain.CartItems;
public record CartItemId(Guid Value) : TypedIdValueBase(Value);