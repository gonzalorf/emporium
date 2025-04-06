using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Commands.RemoveProvider;

public record RemoveProviderCommand
(
    ProviderId ProviderId
) : CommandBase;