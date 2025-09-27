using Newtonsoft.Json;

namespace Emporium.Domain.SeedWork;

[JsonConverter(typeof(TypedIdValueConverter<TenantId>))]
public record TenantId(Guid Value) : TypedIdValueBase(Value);