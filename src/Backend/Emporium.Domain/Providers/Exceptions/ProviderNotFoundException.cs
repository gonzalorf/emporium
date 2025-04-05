namespace Emporium.Domain.Providers.Exceptions;
public sealed class ProviderNotFoundException : ApplicationException
{
    public ProviderNotFoundException(ProviderId id) : base($"Provider with id {id.Value} not found.")
    {
    }
}