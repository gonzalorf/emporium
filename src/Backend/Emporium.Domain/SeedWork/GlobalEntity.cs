namespace Emporium.Domain.SeedWork;

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class GlobalEntity<TIdType> : IGlobalEntity where TIdType : TypedIdValueBase
{
    public TIdType Id { get; protected set; }
    public bool Deleted { get; set; }

    protected GlobalEntity(TIdType id)
    {
        Id = id;
    }
    protected GlobalEntity() { }

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