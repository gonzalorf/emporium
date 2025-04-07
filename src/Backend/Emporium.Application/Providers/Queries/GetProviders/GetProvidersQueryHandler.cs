using AutoMapper;
using Emporium.Application.Configuration.Queries;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Queries.GetProviders;
internal class GetProvidersQueryHandler : IQueryHandler<GetProvidersQuery, IEnumerable<Provider>>
{
    private readonly IProviderRepository providerRepository;

    public GetProvidersQueryHandler(IProviderRepository providerRepository)
    {
        this.providerRepository = providerRepository;
    }

    public async ValueTask<IEnumerable<Provider>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        return await providerRepository.GetAll();
    }
}