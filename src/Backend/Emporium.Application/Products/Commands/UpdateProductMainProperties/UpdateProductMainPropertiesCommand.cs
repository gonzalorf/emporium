namespace Emporium.Application.Products.Commands.UpdateProductMainProperties;

public record UpdateProductMainPropertiesCommand(
        Guid ProductId,
        string Name,
        string Brand,
        string Description
    ) : CommandBase<Result>;