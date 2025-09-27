using Newtonsoft.Json;

namespace Emporium.Domain.Providers;

[JsonConverter(typeof(TypedIdValueConverter<ProviderId>))]
public record ProviderId(Guid Value) : TypedIdValueBase(Value);