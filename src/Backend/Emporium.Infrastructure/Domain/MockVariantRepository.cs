using Emporium.Domain.Variants;
using System.Collections.Concurrent;

namespace Emporium.Infrastructure.Domain;

public class MockVariantRepository : IVariantRepository
{
    private readonly ConcurrentDictionary<Guid, Variant> _variants = new();

    public MockVariantRepository()
    {
        // Seed with mock variants
        var mockVariants = new List<Variant>
        {
           Variant.CreateVariant("Variant A"),
           Variant.CreateVariant("Variant B"),
           Variant.CreateVariant("Variant C")
        };

        foreach (var variant in mockVariants)
        {
            _variants[variant.Id.Value] = variant;
        }
    }

    public Task<Variant?> GetByIdAsync(VariantId id)
    {
        _variants.TryGetValue(id.Value, out var variant);
        return Task.FromResult(variant);
    }

    public Task<IEnumerable<Variant>> GetAllAsync()
    {
        return Task.FromResult(_variants.Values.AsEnumerable());
    }
}