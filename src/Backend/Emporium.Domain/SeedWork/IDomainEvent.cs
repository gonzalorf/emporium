namespace Emporium.Domain.SeedWork;

public interface IDomainEvent
{
    public Guid Id { get; }
    public Guid RelatedId { get; }
    public string Action { get; }
    DateTime OccurredOn { get; }
    TenantId TenantId { get; set; }
}