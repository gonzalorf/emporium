using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products.Events;
public record ProductVariantAddedEvent(ProductId ProductId, VariantValueId VariantValueId) : DomainEventBase;