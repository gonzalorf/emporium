using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.SetPublished;
internal class SetPublishedCommandHandler : ICommandHandler<SetPublishedCommand>
{
    private readonly IProductRepository productRepository;

    public SetPublishedCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task Handle(SetPublishedCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        
        product.SetPublished(request.Published);

        productRepository.Update(product);
    }
}
