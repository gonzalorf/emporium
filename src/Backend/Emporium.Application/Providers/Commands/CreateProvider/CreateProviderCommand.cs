
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Commands.CreateProvider;

public record CreateProviderCommand(
    string Name,
    string BankAccountNumber,
    string BankAccountAlias
) : CommandBase<ProviderId>;