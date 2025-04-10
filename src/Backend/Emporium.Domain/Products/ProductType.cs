namespace Emporium.Domain.Products;

public class ProductType : Entity<ProductTypeId>
{
    public string Name { get; private set; } = string.Empty;

    private ProductType() : base() { }

    public ProductType(ProductTypeId id, string name) : base(id)
    {
        Name = name;
    }

    public static ProductType CreateProductType(string name)
    {
        return new ProductType(new ProductTypeId(Guid.NewGuid()), name);
    }
}