namespace Emporium.Domain.Providers;

public interface IProviderRepository
{
    Task Add(Provider provider);
    void Remove(Provider provider);
    void Update(Provider provider);
    Task<Provider?> GetById(ProviderId id);
    Task<IEnumerable<Provider>> GetAll();
}
