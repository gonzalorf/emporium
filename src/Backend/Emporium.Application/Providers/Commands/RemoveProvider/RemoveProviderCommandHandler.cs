using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;
using Emporium.Domain.Providers.Exceptions;

namespace Emporium.Application.Providers.Commands.RemoveProvider;
internal class RemoveProviderCommandHandler : ICommandHandler<RemoveProviderCommand, Result>
{
    private readonly IProviderRepository providerRepository;

    public RemoveProviderCommandHandler(IProviderRepository ProviderRepository)
    {
        providerRepository = ProviderRepository;
    }

    public async Task<Result> Handle(RemoveProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await providerRepository.GetById(new ProviderId(request.ProviderId));
        if (provider == null)
        {
            return Result.Failure(new Error($"Provider with ID {request.ProviderId} not found."));
        }

        providerRepository.Remove(provider);

        return Result.Success();
    }
}