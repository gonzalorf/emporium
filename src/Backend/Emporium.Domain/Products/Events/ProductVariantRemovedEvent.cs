namespace Emporium.Domain.Products.Events;
public record ProductVariantRemovedEvent(ProductId ProductId, ProductVariantId ProductVariantId) : DomainEventBase;