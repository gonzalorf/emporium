using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.SetUsesVariantPricingAndStock;

public record SetUsesVariantPricingAndStockCommand(
        ProductId ProductId,
        bool UsesVariantPricingAndStock
    ) : CommandBase;
