using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.RemoveProduct;
internal class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand, Result>
{
    private readonly IProductRepository productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
{
    var product = await productRepository.GetById(new ProductId(request.ProductId));

    if (product == null)
    {
        return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
    }

    productRepository.Remove(product);

    return Result.Success();
}
}