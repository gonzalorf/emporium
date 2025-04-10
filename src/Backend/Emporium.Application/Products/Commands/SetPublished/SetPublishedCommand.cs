using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;

namespace Emporium.Application.Products.Commands.SetPublished;

public record SetPublishedCommand(
        Guid ProductId,
        bool Published
    ) : CommandBase<Result>;
