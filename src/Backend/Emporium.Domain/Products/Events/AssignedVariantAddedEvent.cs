using Emporium.Domain.Variants;

namespace Emporium.Domain.Products.Events;
public record AssignedVariantAddedEvent(ProductId ProductId, VariantId VariantId) : DomainEventBase;