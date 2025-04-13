namespace Emporium.Domain.Variants;

public interface IVariantRepository
{
    Task<Variant?> GetByIdAsync(VariantId id);
}