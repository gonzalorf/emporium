using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Commands.UpdateProvider;

public record UpdateProviderCommand(
        ProviderId ProviderId,
        string Name,
        string BankAccountNumber,
        string BankAccountAlias
    ) : CommandBase;