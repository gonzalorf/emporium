using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductPrices;
internal class UpdateProductPricesCommandHandler : ICommandHandler<UpdateProductPricesCommand>
{
    private readonly IProductRepository productRepository;

    public UpdateProductPricesCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task Handle(UpdateProductPricesCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        
        product.UpdatePrices(request.Price, request.StrikethroughPrice);

        productRepository.Update(product);
    }
}
