using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Products;

public record ProductVariantId(Guid Value) : TypedIdValueBase(Value);