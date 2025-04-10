using Emporium.Domain.Products.Events;
using Emporium.Domain.Products.Validators;
using Emporium.Domain.Providers;
using Emporium.Domain.Variants;

namespace Emporium.Domain.Products;
public class Product : AuditableEntity<ProductId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Brand { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ProductType ProductType { get; private set; } = null!;
    public decimal Price { get; private set; }
    public decimal StrikethroughPrice { get; private set; }
    public bool UsesVariantPricingAndStock { get; private set; }
    public bool Published { get; private set; }
    public ProviderId? ProviderId { get; private set; }

    private readonly List<string> tags = new();
    public IReadOnlyCollection<string> Tags => tags.AsReadOnly();
    
    private readonly List<Variant> assignedVariants = new();
    public IReadOnlyCollection<Variant> AssignedVariants => assignedVariants.AsReadOnly();

    private readonly List<ProductVariant> productVariants = new();
    public IReadOnlyCollection<ProductVariant> ProductVariants => productVariants.AsReadOnly();

    private Product() : base() { }

    private Product(ProductId id, string name, string brand, string description, ProductType productType, decimal price, decimal strikethroughPrice, bool usesVariantPricingAndStock, bool published, ProviderId? providerId, string[] tags) : base(id)
    {
        Name = name;
        Brand = brand;
        Description = description;
        Price = price;
        StrikethroughPrice = strikethroughPrice;
        ProductType = productType;
        UsesVariantPricingAndStock = usesVariantPricingAndStock;
        Published = published;
        ProviderId = providerId;
    }

    public static Product CreateProduct(string name, string brand, string description, ProductType productType, decimal price, decimal strikethroughPrice, bool usesVariantPricingAndStock, bool published, ProviderId? providerId, string[] tags)
    {
        var product = new Product(new ProductId(Guid.NewGuid()), name, brand, description, productType, price, strikethroughPrice, usesVariantPricingAndStock, published, providerId, tags);

        ProductValidator.ValidateProduct(product);
        product.AddDomainEvent(new ProductCreatedEvent(product.Id));

        return product;
    }

    public void SetProvider(ProviderId? providerId)
    {
        ProviderId = providerId;

        ProductValidator.ValidateProduct(this);
    }

    public void UpdateMainProperties(string name, string brand, string description)
    {
        Name = name;
        Brand = brand;
        Description = description;

        ProductValidator.ValidateProduct(this);
    }

    public void UpdatePrices(decimal price, decimal strikethroughPrice)
    {
        Price = price;
        StrikethroughPrice = strikethroughPrice;

        ProductValidator.ValidateProduct(this);
    }

    public void UpdateProductType(ProductType productType)
    {
        ProductType = productType;

        ProductValidator.ValidateProduct(this);
    }

    public void SetUsesVariantPricingAndStock(bool usesVariantPricingAndStock)
    {
        UsesVariantPricingAndStock = usesVariantPricingAndStock;

        ProductValidator.ValidateProduct(this);
    }


    public void SetPublished(bool published)
    {
        Published = published;
    }

    public void AddAssignedVariant(Variant variant)
    {
        if (variant == null) throw new ArgumentNullException(nameof(variant));
        if (!assignedVariants.Contains(variant))
        {
            assignedVariants.Add(variant);
            AddDomainEvent(new AssignedVariantAddedEvent(Id, variant.Id));
        }
    }

    public void RemoveAssignedVariant(Variant variant)
    {
        if (variant == null) throw new ArgumentNullException(nameof(variant));
        if (assignedVariants.Remove(variant))
        {
            AddDomainEvent(new AssignedVariantRemovedEvent(Id, variant.Id));
        }
    }

    public ProductVariant AddProductVariant(ProductVariant productVariant)
    {
        productVariants.Add(productVariant);
        AddDomainEvent(new ProductVariantAddedEvent(Id, productVariant.Id));

        return productVariant;
    }

    public void RemoveProductVariant(ProductVariant productVariant)
    {
        productVariants.Remove(productVariant);
        AddDomainEvent(new ProductVariantRemovedEvent(Id, productVariant.Id));
    }
}