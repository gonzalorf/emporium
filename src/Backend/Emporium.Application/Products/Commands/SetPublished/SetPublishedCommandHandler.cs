using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.SetPublished;
internal class SetPublishedCommandHandler : ICommandHandler<SetPublishedCommand, Result>
{
    private readonly IProductRepository productRepository;

    public SetPublishedCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Result> Handle(SetPublishedCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(new ProductId(request.ProductId));
        if (product == null)
        {
            return Result.Failure(new Error($"Product with ID {request.ProductId} not found."));
        }

        product.SetPublished(request.Published);

        productRepository.Update(product);

        return Result.Success();
    }
}
