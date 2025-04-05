using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Variants;
public record VariantValueId(Guid Value) : TypedIdValueBase(Value);