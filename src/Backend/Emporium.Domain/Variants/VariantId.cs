using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Variants;
public record VariantId(Guid Value) : TypedIdValueBase(Value);