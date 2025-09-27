namespace Emporium.Domain.SeedWork;

public interface IDomainEvent : IBaseEntity<DomainEventId>
{    
    public TypedIdValueBase RelatedId { get; }
    public string Action { get; }
    DateTime OccurredOn { get; }
}