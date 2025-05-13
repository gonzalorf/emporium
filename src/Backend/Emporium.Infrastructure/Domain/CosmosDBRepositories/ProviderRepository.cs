using Emporium.Domain.Orders;
using Emporium.Domain.Providers;
using Emporium.Infrastructure.Context;
using Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;

internal class ProviderRepository : IProviderRepository
{
    private readonly IContainerContext context;
    private readonly IProviderPartitionKeyProvider partitionKeyProvider;

    public ProviderRepository(IContainerContext context, IProviderPartitionKeyProvider partitionKeyProvider)
    {
        this.context = context;
        this.partitionKeyProvider = partitionKeyProvider;
    }

    public Task Add(Provider provider)
    {
        var o = new DataObject<Provider>(provider.Id.Value.ToString()
            , partitionKeyProvider.GetPartitionKey(provider)
            , nameof(Provider)
            , provider
            , null
            , -1
            , EntityState.Created);

        context.Add(o);
        return Task.CompletedTask;
    }

    public void Remove(Provider provider)
    {
        throw new NotImplementedException();
    }

    public void Update(Provider provider)
    {
        throw new NotImplementedException();
    }

    public Task<Provider?> GetById(ProviderId id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Provider>> GetAll()
    {
        throw new NotImplementedException();
    }
}