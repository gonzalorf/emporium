using AutoMapper;
using Emporium.Application.Configuration.Queries;
using Emporium.Application.Products.Dtos;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Queries.GetProducts;
internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ProductDto>>(await productRepository.GetAll());
    }
}