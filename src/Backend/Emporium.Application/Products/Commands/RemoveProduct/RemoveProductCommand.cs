using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.RemoveProduct;

public record RemoveProductCommand
(
    ProductId ProductId
) : CommandBase;