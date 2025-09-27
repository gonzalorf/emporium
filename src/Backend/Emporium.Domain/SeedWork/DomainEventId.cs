namespace Emporium.Domain.SeedWork;
public record DomainEventId(Guid Value) : TypedIdValueBase(Value);