namespace Emporium.Domain.Stocks.Exceptions;
public class StockInvalidStateException : ApplicationException
{
    public StockInvalidStateException(string message) : base(message) { }
}