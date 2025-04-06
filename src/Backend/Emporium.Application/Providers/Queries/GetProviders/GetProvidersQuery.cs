using Emporium.Application.Configuration.Queries;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Queries.GetProviders;

public record GetProvidersQuery() : IQuery<IEnumerable<Provider>>;
