namespace Emporium.Application.Products.Commands.AddAssignedVariant;

public record AddAssignedVariantCommand(
        Guid ProductId,
        Guid VariantId
    ) : CommandBase<Result>;
