using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Products;
public record ProductTypeId(Guid Value) : TypedIdValueBase(Value);