using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Products.Events;
public record ProductVariantAddedEvent(ProductId ProductId, ProductVariantId ProductVariantId) : DomainEventBase;