using Emporium.Domain.SeedWork;

namespace Emporium.Application.Configuration.Services;

public interface IUnitOfWork
{
    Task<IList<IEntity>> CommitAsync(CancellationToken cancellationToken = default);
}