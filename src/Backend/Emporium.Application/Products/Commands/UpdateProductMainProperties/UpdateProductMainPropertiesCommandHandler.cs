using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.UpdateProductMainProperties;
internal class UpdateProductMainPropertiesCommandHandler : ICommandHandler<UpdateProductMainPropertiesCommand, Result>
{
    private readonly IProductRepository productRepository;

    public UpdateProductMainPropertiesCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result> Handle(UpdateProductMainPropertiesCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(new ProductId(request.ProductId));
        if (product == null)
        {
            return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
        }

        product.UpdateMainProperties(
            request.Name,
            request.Brand,
            request.Description
        );

        productRepository.Update(product);

        return Result.Success();
    }
}