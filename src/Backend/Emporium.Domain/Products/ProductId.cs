using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Products;
public record ProductId(Guid Value) : TypedIdValueBase(Value);