using Emporium.Domain.Products;
using Emporium.Domain.Variants;

namespace Emporium.Application.Products.Commands.AddAssignedVariant;

internal class AddAssignedVariantCommandHandler : ICommandHandler<AddAssignedVariantCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IVariantRepository _variantRepository;

    public AddAssignedVariantCommandHandler(IProductRepository productRepository, IVariantRepository variantRepository)
    {
        _productRepository = productRepository;
        _variantRepository = variantRepository;
    }

    public async Task<Result> Handle(AddAssignedVariantCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(new ProductId(request.ProductId));
        if (product == null)
        {
            return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
        }

        var variant = await _variantRepository.GetByIdAsync(new VariantId(request.VariantId));
        if (variant == null)
        {
            return Result.Failure(new Error($"Variant with ID {request.VariantId} not found."));
        }

        if (!product.AssignedVariants.Contains(variant))
        {
            product.AddAssignedVariant(variant);
        }

        _productRepository.Update(product);
        return Result.Success();
    }
}