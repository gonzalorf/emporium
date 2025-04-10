namespace Emporium.Application.Products.Commands.UpdateProductPrices;

public record UpdateProductPricesCommand(
        Guid ProductId,
        decimal Price,
        decimal StrikethroughPrice
    ) : CommandBase<Result>;
