using Emporium.Domain.Products;
using System.Collections.Concurrent;

namespace Emporium.Infrastructure.Domain;

public class MockProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<ProductId, Product> _products = new();
    private readonly ConcurrentDictionary<ProductTypeId, ProductType> _productTypes = new();

    public Task Add(Product product)
    {
        _products[product.Id] = product;
        return Task.CompletedTask;
    }

    public void Remove(Product product)
    {
        _products.TryRemove(product.Id, out _);
    }

    public void Update(Product product)
    {
        _products[product.Id] = product;
    }

    public Task<Product?> GetById(ProductId id)
    {
        _products.TryGetValue(id, out var product);
        return Task.FromResult(product);
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        return Task.FromResult<IEnumerable<Product>>(_products.Values);
    }

    public Task<ProductType> GetProductTypeById(ProductTypeId id)
    {
        _productTypes.TryGetValue(id, out var productType);
        return Task.FromResult(productType);
    }

    public Task<IEnumerable<Product>> GetByFilters(string? name, string? category)
    {
        var filteredProducts = _products.Values.Where(p =>
            (string.IsNullOrEmpty(name) || p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
        );
        return Task.FromResult(filteredProducts);
    }
}