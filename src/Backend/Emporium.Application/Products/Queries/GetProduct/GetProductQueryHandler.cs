using Emporium.Application.Configuration.Queries;
using Emporium.Domain.Products;
using Emporium.Application.Products.Dtos;
using MapsterMapper;

namespace Emporium.Application.Products.Queries.GetProduct;
internal class GetProductQueryHandler : IQueryHandler<GetProductQuery, Result<ProductDto>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

public async ValueTask<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
{
    var product = await productRepository.GetById(new ProductId(request.ProductId));
    if (product == null)
    {
        return Result.Failure<ProductDto>(new Error($"Product with ID {request.ProductId} not found."));
    }

    var productDto = mapper.Map<ProductDto>(product);
    return Result.Success(productDto);
}
}