using Newtonsoft.Json;

namespace Emporium.Domain.SeedWork;
public interface IBaseEntity<out TIdType> where TIdType : TypedIdValueBase
{
    [JsonProperty(PropertyName = "id")]
    TIdType Id { get; }
    TenantId TenantId { get; set; }
    string? Version { get; set; } // Nombre genérico para concurrencia (mapeará a _etag en infra)
    string EntityType { get; } // Discriminador de tipo
}
