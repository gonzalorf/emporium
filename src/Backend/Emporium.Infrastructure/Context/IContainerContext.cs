using Emporium.Domain.SeedWork;
using Microsoft.Azure.Cosmos;

namespace Emporium.Infrastructure.Context;

public interface IContainerContext
{
    public Container Container { get; }
    public List<IDataObject<IEntity>> DataObjects { get; }
    public void Add(IDataObject<IEntity> entity);
    public Task<List<IDataObject<IEntity>>> SaveChangesAsync(CancellationToken cancellationToken = default);
    public void Reset();
}
