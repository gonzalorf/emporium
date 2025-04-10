using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductMainProperties;

public record UpdateProductMainPropertiesCommand(
        Guid ProductId,
        string Name,
        string Brand,
        string Description
    ) : CommandBase<Result>;