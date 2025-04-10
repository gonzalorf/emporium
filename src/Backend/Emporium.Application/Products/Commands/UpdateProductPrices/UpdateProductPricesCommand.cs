using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductPrices;

public record UpdateProductPricesCommand(
        Guid ProductId,
        decimal Price,
        decimal StrikethroughPrice
    ) : CommandBase<Result>;
