using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;
using Emporium.Application.Common;

namespace Emporium.Application.Products.Commands.SetUsesVariantPricingAndStock;
internal class SetUsesVariantPricingAndStockCommandHandler : ICommandHandler<SetUsesVariantPricingAndStockCommand, Result>
{
    private readonly IProductRepository productRepository;

    public SetUsesVariantPricingAndStockCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    
    public async ValueTask<Result> Handle(SetUsesVariantPricingAndStockCommand request, CancellationToken cancellationToken)
{
    var product = await productRepository.GetById(new ProductId(request.ProductId));
    if (product == null)
    {
        return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
    }

    product.SetUsesVariantPricingAndStock(request.UsesVariantPricingAndStock);

    productRepository.Update(product);

    return Result.Success();
}
}
