using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductMainProperties;
internal class UpdateProductMainPropertiesCommandHandler : ICommandHandler<UpdateProductMainPropertiesCommand>
{
    private readonly IProductRepository productRepository;

    public UpdateProductMainPropertiesCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Mediator.Unit> Handle(UpdateProductMainPropertiesCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        
        product.UpdateMainProperties(request.Name,
                                 request.Brand,
                                 request.Description);

        productRepository.Update(product);

        return Mediator.Unit.Value;
    }
}