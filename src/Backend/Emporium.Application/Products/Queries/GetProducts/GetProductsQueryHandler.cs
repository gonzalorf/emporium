using Emporium.Application.Common;
using Emporium.Application.Configuration.Queries;
using Emporium.Application.Products.Dtos;
using Emporium.Domain.Products;
using MapsterMapper;

namespace Emporium.Application.Products.Queries.GetProducts;
internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async ValueTask<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAll();

        if (products == null || !products.Any())
        {
            return Result.Failure<IEnumerable<ProductDto>>(new Error("No products found."));
        }

        var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);
        return Result.Success(productDtos);
    }
}