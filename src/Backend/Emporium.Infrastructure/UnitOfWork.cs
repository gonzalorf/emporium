using Emporium.Application.Configuration.Services;
using Emporium.Domain.SeedWork;
using Emporium.Infrastructure.Context;

namespace Emporium.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IContainerContext _context;
    
    public UnitOfWork(IContainerContext ctx)
    {
        _context = ctx;
    }

    public async Task<IList<IEntity>> CommitAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result.Select(d => d.Data).ToList();
    }
}