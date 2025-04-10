using Emporium.Domain.Products.Events;
using Emporium.Domain.Products;
using Emporium.Domain.Providers;

namespace Emporium.Application.Products.Commands.CreateProduct;
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result<ProductId>>
{
    private readonly IProductRepository productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productType = await productRepository.GetProductTypeById(new ProductTypeId(request.ProductTypeId));
        if (productType == null)
        {
            return Result.Failure<ProductId>(new Error($"Product type with ID {request.ProductTypeId} not found."));
        }

        var product = Product.CreateProduct(
            request.Name,
            request.Brand,
            request.Description,
            productType,
            request.Price,
            request.StrikethroughPrice,
            request.UsesVariantPricingAndStock,
            request.Published,
            request.ProviderId != Guid.Empty ? new ProviderId(request.ProviderId) : null,
            request.Tags
        );

        await productRepository.Add(product);
        product.AddDomainEvent(new ProductCreatedEvent(product.Id));

        return Result.Success(product.Id);
    }
}