namespace Emporium.Domain.Variants;
public class VariantValue : Entity<VariantValueId>
{
    public string Name { get; private set; } = string.Empty;

    private VariantValue() : base() { }

    private VariantValue(VariantValueId id, string name) : base(id)
    {
        Name = name;
    }

    public static VariantValue CreateVariantValue(string name)
    {
        return new VariantValue(new VariantValueId(Guid.NewGuid()), name);
    }
}