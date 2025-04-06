using Emporium.Application.Configuration.Queries;
using Emporium.Application.Products.Dtos;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Queries.GetProduct;

public record GetProductQuery(ProductId ProductId) : IQuery<ProductDto>;
