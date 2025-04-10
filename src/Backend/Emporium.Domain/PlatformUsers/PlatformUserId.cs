namespace Emporium.Domain.PlatformUsers;

public record PlatformUserId(Guid Value) : TypedIdValueBase(Value);
