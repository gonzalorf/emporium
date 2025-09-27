
namespace Emporium.Domain.SeedWork;

public record DomainEventBase : IDomainEvent
{
    public DomainEventBase(TypedIdValueBase relatedId, string action)
    {
        Id = new DomainEventId(Guid.NewGuid());
        OccurredOn = DateTime.UtcNow;
        RelatedId = relatedId;
        Action = action;
        //TenantId = tenantId;
    }

    public DateTime OccurredOn { get; }

    public TenantId TenantId { get; set; }

    public string Action { get; }

    public TypedIdValueBase RelatedId { get; }

    public DomainEventId Id { get; set; }

    public string? Version { get; set; }

    public string EntityType { get => GetType().Name; }
}