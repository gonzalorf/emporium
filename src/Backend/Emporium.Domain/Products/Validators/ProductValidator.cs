using FluentValidation;
using Emporium.Domain.Products.Exceptions;

namespace Emporium.Domain.Products.Validators;
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(256);
        RuleFor(p => p.Description).NotEmpty().MaximumLength(256);
        RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
        RuleFor(p => p.Tags).NotNull();
    }

    public static void ValidateProduct(Product product)
    {
        var validator = new ProductValidator();
        var validationResult = validator.Validate(product);

        if (!validationResult.IsValid)
        {
            throw new ProductInvalidStateException(validationResult.ToString());
        }
    }
}