using Emporium.Domain.Variants;

namespace Emporium.Domain.Products.Events;
public record AssignedVariantRemovedEvent(ProductId ProductId, VariantId VariantId) : DomainEventBase;