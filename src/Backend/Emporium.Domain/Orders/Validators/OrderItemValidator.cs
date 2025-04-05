using FluentValidation;
using Emporium.Domain.Orders.Exceptions;

namespace Emporium.Domain.Orders.Validators;

public class OrderDetailValidator : AbstractValidator<OrderItem>
{
    public OrderDetailValidator()
    {
        RuleFor(p => p.Quantity).GreaterThan(0);
    }

    public static void ValidateOrderDetail(OrderItem orderItem)
    {
        var validator = new OrderDetailValidator();
        var validationResult = validator.Validate(orderItem);

        if (!validationResult.IsValid)
        {
            throw new OrderInvalidStateException(validationResult.ToString());
        }
    }
}