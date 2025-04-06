namespace Emporium.Application.Products.Dtos;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    decimal StrikethroughPrice,
    string[] Tags,
    Guid ProviderId,
    string ProviderName
)
{
    public ProductDto() : this(Guid.Empty, string.Empty, string.Empty, 0, 0, Array.Empty<string>(), Guid.Empty, string.Empty)
    {

    }
};