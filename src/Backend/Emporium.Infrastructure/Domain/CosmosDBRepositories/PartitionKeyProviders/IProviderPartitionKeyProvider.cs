using Emporium.Domain.Providers;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories.PartitionKeyProviders;
public interface IProviderPartitionKeyProvider
{
    string GetPartitionKey(Provider provider);
    string GetPartitionKey(string id);
}
