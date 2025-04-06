using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.SetUsesVariantPricingAndStock;
internal class SetUsesVariantPricingAndStockCommandHandler : ICommandHandler<SetUsesVariantPricingAndStockCommand>
{
    private readonly IProductRepository productRepository;

    public SetUsesVariantPricingAndStockCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task Handle(SetUsesVariantPricingAndStockCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        
        product.SetUsesVariantPricingAndStock(request.UsesVariantPricingAndStock);

        productRepository.Update(product);
    }
}
