using Emporium.Domain.Products;

namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;
internal class ProductRepository : IProductRepository
{
    public Task Add(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByFilters(string? name, string? category)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetById(ProductId id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductType> GetProductTypeById(ProductTypeId id)
    {
        throw new NotImplementedException();
    }

    public void Remove(Product product)
    {
        throw new NotImplementedException();
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
}
