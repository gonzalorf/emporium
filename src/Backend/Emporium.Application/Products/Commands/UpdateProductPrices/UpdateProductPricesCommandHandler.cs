using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;
using Emporium.Application.Common;

namespace Emporium.Application.Products.Commands.UpdateProductPrices;
internal class UpdateProductPricesCommandHandler : ICommandHandler<UpdateProductPricesCommand, Result>
{
    private readonly IProductRepository productRepository;

    public UpdateProductPricesCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Result> Handle(UpdateProductPricesCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(new ProductId(request.ProductId));
        if (product == null)
        {
            return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
        }

        product.UpdatePrices(request.Price, request.StrikethroughPrice);

        productRepository.Update(product);

        return Result.Success();
    }
}
