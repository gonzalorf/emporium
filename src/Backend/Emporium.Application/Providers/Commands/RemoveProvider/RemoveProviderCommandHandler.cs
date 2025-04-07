using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;
using Emporium.Domain.Providers.Exceptions;

namespace Emporium.Application.Providers.Commands.RemoveProvider;
internal class RemoveProviderCommandHandler : ICommandHandler<RemoveProviderCommand>
{
    private readonly IProviderRepository providerRepository;

    public RemoveProviderCommandHandler(IProviderRepository ProviderRepository)
    {
        providerRepository = ProviderRepository;
    }

    public async ValueTask<Mediator.Unit> Handle(RemoveProviderCommand request, CancellationToken cancellationToken)
    {
        var Provider = await providerRepository.GetById(request.ProviderId) ?? throw new ProviderNotFoundException(request.ProviderId);

        providerRepository.Remove(Provider);

        return Mediator.Unit.Value;
    }
}