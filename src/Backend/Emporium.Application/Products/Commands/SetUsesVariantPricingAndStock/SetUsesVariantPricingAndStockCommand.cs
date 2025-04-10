namespace Emporium.Application.Products.Commands.SetUsesVariantPricingAndStock;

public record SetUsesVariantPricingAndStockCommand(
        Guid ProductId,
        bool UsesVariantPricingAndStock
    ) : CommandBase<Result>;
