using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;

namespace Emporium.Application.Products.Commands.RemoveProduct;

public record RemoveProductCommand
(
    Guid ProductId
) : CommandBase<Result>;