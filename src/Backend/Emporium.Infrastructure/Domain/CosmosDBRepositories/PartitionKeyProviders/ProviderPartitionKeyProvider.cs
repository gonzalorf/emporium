using Emporium.Domain.Providers;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;
public class ProviderPartitionKeyProvider : IProviderPartitionKeyProvider
{
    public string GetPartitionKey(Provider provider)
    {
        return provider.Id.Value.ToString();
    }

    public string GetPartitionKey(string id)
    {
        return id;
    }
}
