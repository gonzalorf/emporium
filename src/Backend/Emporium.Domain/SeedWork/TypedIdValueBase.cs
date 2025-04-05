namespace Emporium.Domain.SeedWork;

public record TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Value { get; }

    public bool IsEmpty()
    {
        return Value == Guid.Empty;
    }

    protected TypedIdValueBase(Guid value)
    {
        Value = value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}