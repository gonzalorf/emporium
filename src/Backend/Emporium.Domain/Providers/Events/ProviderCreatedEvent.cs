namespace Emporium.Domain.Providers.Events;
public record ProviderCreatedEvent(ProviderId ProviderId) : DomainEventBase(ProviderId, nameof(ProviderCreatedEvent));