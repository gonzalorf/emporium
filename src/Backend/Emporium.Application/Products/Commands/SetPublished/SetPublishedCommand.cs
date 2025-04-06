using Emporium.Application.Configuration.Commands;
using Emporium.Domain.Products;

namespace Emporium.Application.Products.Commands.SetPublished;

public record SetPublishedCommand(
        ProductId ProductId,
        bool Published
    ) : CommandBase;
