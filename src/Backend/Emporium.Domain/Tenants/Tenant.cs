using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Tenants;
public class Tenant
{
    public TenantId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

    private Tenant() { } // EF

    public static Tenant CreateTenant(TenantId id, string name)
    {
        return new Tenant() { Id = id, Name = name };
    }
}