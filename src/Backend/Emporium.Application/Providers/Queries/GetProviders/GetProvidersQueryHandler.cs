using Emporium.Application.Common;
using Emporium.Application.Configuration.Queries;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Queries.GetProviders;
internal class GetProvidersQueryHandler : IQueryHandler<GetProvidersQuery, Result<IEnumerable<Provider>>>
{
    private readonly IProviderRepository providerRepository;

    public GetProvidersQueryHandler(IProviderRepository providerRepository)
    {
        this.providerRepository = providerRepository;
    }

    public async Task<Result<IEnumerable<Provider>>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var providers = await providerRepository.GetAll();

        if (providers == null || !providers.Any())
        {
            return Result.Failure<IEnumerable<Provider>>(new Error("No providers found."));
        }

        return Result.Success(providers);
    }
}