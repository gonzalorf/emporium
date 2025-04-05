using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Providers;

public record ProviderId(Guid Value) : TypedIdValueBase(Value);