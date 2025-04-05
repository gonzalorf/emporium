namespace Emporium.Domain.SeedWork;

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class Entity<TIdType> : IEntity where TIdType : TypedIdValueBase
{
    public TIdType Id { get; protected set; }
    public TenantId TenantId { get; set; } = new(Guid.Empty);
    public bool Deleted { get; set; }

    protected Entity(TIdType id)
    {
        Id = id;
    }
    protected Entity() { }

    readonly List<IDomainEvent> domainEvents = new();

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => domainEvents?.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
}