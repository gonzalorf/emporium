using Emporium.Application.Common;
using Emporium.Application.Configuration.Queries;
using Emporium.Application.Products.Dtos;

namespace Emporium.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid ProductId) : IQuery<Result<ProductDto>>;
