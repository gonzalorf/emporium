namespace Emporium.Domain.Products.Exceptions;
public class ProductInvalidStateException : ApplicationException
{
    public ProductInvalidStateException(string message) : base(message) { }
}