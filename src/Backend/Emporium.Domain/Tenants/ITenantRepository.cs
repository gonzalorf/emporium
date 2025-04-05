using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Tenants;
public interface ITenantRepository
{
    Task<Tenant?> GetById(TenantId id);
}
