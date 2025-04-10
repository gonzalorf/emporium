using Emporium.Application.Common;
using Emporium.Application.Configuration.Queries;
using Emporium.Application.Products.Dtos;

namespace Emporium.Application.Products.Queries.GetProducts;

public record GetProductsQuery() : IQuery<Result<IEnumerable<ProductDto>>>;
