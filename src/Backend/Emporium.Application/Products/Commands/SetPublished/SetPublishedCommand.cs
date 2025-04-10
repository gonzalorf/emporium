namespace Emporium.Application.Products.Commands.SetPublished;

public record SetPublishedCommand(
        Guid ProductId,
        bool Published
    ) : CommandBase<Result>;
