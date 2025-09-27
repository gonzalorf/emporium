using Newtonsoft.Json;

namespace Emporium.Domain.SeedWork;

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class Entity<TIdType> : IEntity<TIdType> where TIdType : TypedIdValueBase
{
    protected Entity(TIdType id)
    {
        this.Id = id;
    }
    
    readonly List<IDomainEvent> domainEvents = new();

    [JsonIgnore] 
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => domainEvents?.AsReadOnly();

    public TIdType Id { get; }
    public TenantId TenantId { get; set; } = new(Guid.Empty);
    public bool Deleted { get; set; }

    public string? Version { get; set; }

    public string EntityType { get => GetType().Name; }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
}