using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products.Events;
public record ProductVariantRemovedEvent(ProductId ProductId, VariantValueId VariantValueId) : DomainEventBase;