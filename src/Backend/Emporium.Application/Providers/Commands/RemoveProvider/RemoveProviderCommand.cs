using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;

namespace Emporium.Application.Providers.Commands.RemoveProvider;

public record RemoveProviderCommand
(
    Guid ProviderId
) : CommandBase<Result>;