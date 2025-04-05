namespace Emporium.Domain.SeedWork;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    TenantId TenantId { get; set; }
}