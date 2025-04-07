using Emporium.Domain.SeedWork;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products.Events;
public record AssignedVariantRemovedEvent(ProductId ProductId, VariantId VariantId) : DomainEventBase;