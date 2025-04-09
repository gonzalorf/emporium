using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductType;

public record UpdateProductTypeCommand(
        ProductId ProductId,
        Guid ProductTypeId
    ) : CommandBase<Result>;
