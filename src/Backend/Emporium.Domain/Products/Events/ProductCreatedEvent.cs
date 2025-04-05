using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Products.Events;
public record ProductCreatedEvent(ProductId ProductId) : DomainEventBase;