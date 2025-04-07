using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductType;
internal class UpdateProductTypeCommandHandler : ICommandHandler<UpdateProductTypeCommand>
{
    private readonly IProductRepository productRepository;

    public UpdateProductTypeCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Mediator.Unit> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        var productType = await productRepository.GetProductTypeById(new ProductTypeId(request.ProductTypeId));

        product.UpdateProductType(productType);

        productRepository.Update(product);

        return Mediator.Unit.Value;
    }
}
