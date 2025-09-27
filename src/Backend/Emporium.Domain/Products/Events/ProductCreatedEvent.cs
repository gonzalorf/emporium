namespace Emporium.Domain.Products.Events;
public record ProductCreatedEvent(ProductId ProductId) : DomainEventBase(ProductId, nameof(ProductCreatedEvent));