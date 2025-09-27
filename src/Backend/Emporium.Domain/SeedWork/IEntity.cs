using System.Text.Json.Serialization;

namespace Emporium.Domain.SeedWork;
public interface IEntity<TIdType> : IBaseEntity<TIdType> where TIdType : TypedIdValueBase
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
    bool Deleted { get; set; }
}
