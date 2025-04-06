using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductMainProperties;

public record UpdateProductMainPropertiesCommand(
        ProductId ProductId,
        string Name,
        string Brand,
        string Description
    ) : CommandBase;