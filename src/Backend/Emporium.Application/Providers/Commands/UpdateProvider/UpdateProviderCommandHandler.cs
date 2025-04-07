using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;
using Emporium.Domain.Providers.Exceptions;

namespace Emporium.Application.Providers.Commands.UpdateProvider;
internal class UpdateProviderCommandHandler : ICommandHandler<UpdateProviderCommand>
{
    private readonly IProviderRepository providerRepository;

    public UpdateProviderCommandHandler(IProviderRepository providerRepository)
    {
        this.providerRepository = providerRepository;
    }

    public async ValueTask<Mediator.Unit> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await providerRepository.GetById(request.ProviderId) ?? throw new ProviderNotFoundException(request.ProviderId);

        provider.Update(request.Name, request.BankAccountNumber, request.BankAccountAlias);

        providerRepository.Update(provider);

        return Mediator.Unit.Value;
    }
}