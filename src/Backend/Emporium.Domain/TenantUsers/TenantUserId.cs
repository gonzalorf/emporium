using Emporium.Domain.SeedWork;

namespace Emporium.Domain.TenantUsers;

public record TenantUserId(Guid Value) : TypedIdValueBase(Value);
