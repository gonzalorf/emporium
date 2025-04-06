namespace Emporium.Domain.SeedWork;
public interface IGlobalEntity
{
    // Para Entities que no pertenecen a ningún Tenant, sino que son globales.
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    bool Deleted { get; set; }
}
