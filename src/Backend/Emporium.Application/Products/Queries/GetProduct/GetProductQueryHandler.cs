using Emporium.Application.Configuration.Queries;
using Emporium.Domain.Products.Exceptions;
using Emporium.Domain.Products;
using Emporium.Application.Products.Dtos;
using MapsterMapper;

namespace Emporium.Application.Products.Queries.GetProduct;
internal class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async ValueTask<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);

        return mapper.Map<ProductDto>(product);
    }
}