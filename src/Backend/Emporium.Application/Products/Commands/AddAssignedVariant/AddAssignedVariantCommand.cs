namespace Emporium.Application.Products.Commands;

public record AddAssignedVariantCommand(
        Guid ProductId,
        Guid VariantId
    ) : CommandBase<Result>;