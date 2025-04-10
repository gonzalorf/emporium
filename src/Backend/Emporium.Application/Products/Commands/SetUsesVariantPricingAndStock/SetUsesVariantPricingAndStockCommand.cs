using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;

namespace Emporium.Application.Products.Commands.SetUsesVariantPricingAndStock;

public record SetUsesVariantPricingAndStockCommand(
        Guid ProductId,
        bool UsesVariantPricingAndStock
    ) : CommandBase<Result>;
