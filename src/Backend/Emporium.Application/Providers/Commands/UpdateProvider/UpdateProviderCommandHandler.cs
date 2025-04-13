using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;
using Emporium.Domain.Providers.Exceptions;

namespace Emporium.Application.Providers.Commands.UpdateProvider;
internal class UpdateProviderCommandHandler : ICommandHandler<UpdateProviderCommand, Result>
{
    private readonly IProviderRepository providerRepository;

    public UpdateProviderCommandHandler(IProviderRepository providerRepository)
    {
        this.providerRepository = providerRepository;
    }

public async Task<Result> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
{
    var provider = await providerRepository.GetById(new ProviderId(request.ProviderId));
    if (provider == null)
    {
        return Result.Failure(new Error($"Provider with ID {request.ProviderId} not found."));
    }

    provider.Update(request.Name, request.BankAccountNumber, request.BankAccountAlias);

    providerRepository.Update(provider);

    return Result.Success();
}
}