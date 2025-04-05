using FluentValidation;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Stocks.Exceptions;

namespace Emporium.Domain.Stocks.Validators;
public class StockValidator : AbstractValidator<Stock>
{
    public StockValidator()
    {
        RuleFor(p => p.ProductId).NotNull();
        RuleFor(p => p.Count).GreaterThanOrEqualTo(0);
    }

    public static void ValidateStock(Stock stock)
    {
        var validator = new StockValidator();
        var validationResult = validator.Validate(stock);

        if (!validationResult.IsValid)
        {
            throw new StockInvalidStateException(validationResult.ToString());
        }
    }
}