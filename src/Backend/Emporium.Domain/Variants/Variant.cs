using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Variants;
public class Variant : Entity<VariantId>
{
    public string Name { get; private set; } = string.Empty;

    private readonly List<VariantValue> variantValues = new();
    public IReadOnlyCollection<VariantValue> VariantValues => variantValues.AsReadOnly();

    private Variant() : base() { }

    private Variant(VariantId id, string name) : base(id)
    {
        Name = name;
    }

    public static Variant CreateVariant(string name)
    {
        return new Variant(new VariantId(Guid.NewGuid()), name);
    }

    public void AddVariantValue(VariantValue variantValue)
    {
        variantValues.Add(variantValue);
    }

    public void RemoveVariantValue(VariantValue variantValue)
    {
        variantValues.Remove(variantValue);
    }
}