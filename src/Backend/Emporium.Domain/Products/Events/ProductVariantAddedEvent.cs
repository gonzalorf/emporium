using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products.Events;
public record VariantValueAddedToProductEvent(ProductId ProductId, VariantValueId VariantValueId) : DomainEventBase;