namespace Emporium.Domain.SeedWork;

public record DomainEventBase : IDomainEvent
{
    public DomainEventBase()
    {
        OccurredOn = DateTime.UtcNow;
    }

    public DateTime OccurredOn { get; }

    public TenantId TenantId { get; set; }
}