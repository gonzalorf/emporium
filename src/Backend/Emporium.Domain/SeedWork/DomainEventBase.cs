
namespace Emporium.Domain.SeedWork;

public record DomainEventBase : IDomainEvent, IEntity
{
    private DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    public DomainEventBase(Guid relatedId, string action) : this()
    {
        RelatedId = relatedId;
        Action = action;
    }

    public Guid Id { get; }

    public Guid RelatedId { get; }

    public DateTime OccurredOn { get; }

    public TenantId TenantId { get; set; }

    public string Action { get; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => new List<IDomainEvent>();

    public bool Deleted { get; set; }

    public void ClearDomainEvents()
    {
    }
}