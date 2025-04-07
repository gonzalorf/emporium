using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.RemoveProduct;
internal class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand>
{
    private readonly IProductRepository productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async ValueTask<Mediator.Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId);

        if (product == null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        productRepository.Remove(product);

        return Mediator.Unit.Value;
    }
}