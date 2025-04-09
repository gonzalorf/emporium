using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;
using Emporium.Application.Common;

namespace Emporium.Application.Products.Commands.UpdateProductType;
internal class UpdateProductTypeCommandHandler : ICommandHandler<UpdateProductTypeCommand, Result>
{
    private readonly IProductRepository productRepository;

    public UpdateProductTypeCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Result> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId);
        if (product == null)
        {
            return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
        }
    
        var productType = await productRepository.GetProductTypeById(new ProductTypeId(request.ProductTypeId));
        if (productType == null)
        {
            return Result.Failure(new Error($"Product type with ID {request.ProductTypeId} not found."));
        }
    
        product.UpdateProductType(productType);
        productRepository.Update(product);
    
        return Result.Success();
    }
}
