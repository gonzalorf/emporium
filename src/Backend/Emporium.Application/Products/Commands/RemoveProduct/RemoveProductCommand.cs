namespace Emporium.Application.Products.Commands.RemoveProduct;

public record RemoveProductCommand
(
    Guid ProductId
) : CommandBase<Result>;