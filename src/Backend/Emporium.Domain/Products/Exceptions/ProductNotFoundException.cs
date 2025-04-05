namespace Emporium.Domain.Products.Exceptions;
public sealed class ProductNotFoundException : ApplicationException
{
    public ProductNotFoundException(ProductId id) : base($"Product with id {id.Value} not found.")
    {
    }
}