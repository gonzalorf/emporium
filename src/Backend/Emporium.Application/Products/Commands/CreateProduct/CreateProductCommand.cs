using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(
        string Name,
        string Brand,
        string Description,
        Guid ProductTypeId,
        decimal Price,
        decimal StrikethroughPrice,
        bool UsesVariantPricingAndStock,
        bool Published,
        Guid ProviderId,
        string[] Tags
    ) : CommandBase<Result<ProductId>>;