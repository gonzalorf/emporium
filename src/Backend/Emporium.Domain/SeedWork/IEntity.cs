namespace Emporium.Domain.SeedWork;
public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();

    TenantId TenantId { get; set; }
    bool Deleted { get; set; }
}
