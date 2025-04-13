using Emporium.Domain.Providers;
using System.Collections.Concurrent;

namespace Emporium.Infrastructure.Domain;

public class MockProviderRepository : IProviderRepository
{
    private readonly ConcurrentDictionary<ProviderId, Provider> _providers = new();

    public MockProviderRepository()
    {
        // Initialize with some mock data
        var provider1 = Provider.CreateProvider("Provider One", "", "");
        var provider2 = Provider.CreateProvider("Provider Two", "", "");
        var provider3 = Provider.CreateProvider("Provider Three", "", "");

        _providers[provider1.Id] = provider1;
        _providers[provider2.Id] = provider2;
        _providers[provider3.Id] = provider3;
    }

    public Task Add(Provider provider)
    {
        _providers[provider.Id] = provider;
        return Task.CompletedTask;
    }

    public void Remove(Provider provider)
    {
        _providers.TryRemove(provider.Id, out _);
    }

    public void Update(Provider provider)
    {
        _providers[provider.Id] = provider;
    }

    public Task<Provider?> GetById(ProviderId id)
    {
        _providers.TryGetValue(id, out var provider);
        return Task.FromResult(provider);
    }

    public Task<IEnumerable<Provider>> GetAll()
    {
        return Task.FromResult<IEnumerable<Provider>>(_providers.Values);
    }
}