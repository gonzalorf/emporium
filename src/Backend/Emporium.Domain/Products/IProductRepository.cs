using Emporium.Domain.Providers;

namespace Emporium.Domain.Products;
public interface IProductRepository
{
    Task Add(Product product);
    void Remove(Product product);
    void Update(Product product);
    Task<Product?> GetById(ProductId id);
    Task<IEnumerable<Product>> GetAll();
}
