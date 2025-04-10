using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;

namespace Emporium.Application.Providers.Commands.UpdateProvider;

public record UpdateProviderCommand(
        Guid ProviderId,
        string Name,
        string BankAccountNumber,
        string BankAccountAlias
    ) : CommandBase<Result>;