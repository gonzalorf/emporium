using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Commands.CreateProvider;
internal class CreateProviderCommandHandler : ICommandHandler<CreateProviderCommand, ProviderId>
{
    private readonly IProviderRepository providerRepository;

    public CreateProviderCommandHandler(IProviderRepository providerRepository)
    {
        this.providerRepository = providerRepository;
    }

    public async Task<ProviderId> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = Provider.CreateProvider(
            request.Name
            , request.BankAccountNumber
            , request.BankAccountAlias
            );
        await providerRepository.Add(provider);

        return provider.Id;
    }
}