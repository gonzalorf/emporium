using Emporium.Application.Configuration.Services;
using Emporium.Domain.Providers;
using Emporium.Infrastructure.CosmosDB;
using Microsoft.Azure.Cosmos;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;

internal class ProviderRepository : IProviderRepository
{
    private readonly UnitOfWork _unitOfWork;
    private readonly string _containerName = "Provider";

    public ProviderRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = (UnitOfWork)(unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)));
    }

    public Task Add(Provider provider)
    {
        //var o = new DataObject<Provider>(provider.Id.Value.ToString()
        //    , partitionKeyProvider.GetPartitionKey(provider)
        //    , nameof(Provider)
        //    , provider
        //    , null
        //    , -1
        //    , EntityState.Created);

        //context.Add(o);
        //return Task.CompletedTask;


        // Extrae el valor de la PK (category) y lo pasa al UoW
        _unitOfWork.RegisterChange(provider, _containerName, EntityState.Created, new PartitionKey(provider.Id.Value.ToString()));
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