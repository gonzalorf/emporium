using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Users;
public record UserId(Guid Value) : TypedIdValueBase(Value);
