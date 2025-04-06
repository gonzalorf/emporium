using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductPrices;

public record UpdateProductPricesCommand(
        ProductId ProductId,
        decimal Price,
        decimal StrikethroughPrice
    ) : CommandBase;
