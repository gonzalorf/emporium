using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Users.Events;
public record LoginPerformedEvent(UserId UserId) : DomainEventBase;