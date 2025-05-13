using Emporium.Domain.Variants;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;
internal class VariantRepository : IVariantRepository
{
    public Task<Variant?> GetByIdAsync(VariantId id)
    {
        throw new NotImplementedException();
    }
}
