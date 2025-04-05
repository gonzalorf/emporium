using FluentValidation;
using Emporium.Domain.Orders.Exceptions;

namespace Emporium.Domain.Orders.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        // NO RULES FOR NOW
    }

    public static void ValidateOrder(Order order)
    {
        var validator = new OrderValidator();
        var validationResult = validator.Validate(order);

        if (!validationResult.IsValid)
        {
            throw new OrderInvalidStateException(validationResult.ToString());
        }
    }
}