using Emporium.Application.Configuration.Queries;
using Emporium.Application.Products.Dtos;
using Emporium.Domain.Providers;

namespace Emporium.Application.Products.Queries.GetProducts;

public record GetProductsQuery() : IQuery<IEnumerable<ProductDto>>;
