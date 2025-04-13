using Carter;
using Emporium.API.Common;
using Emporium.Application.Providers.Commands.CreateProvider;
using Emporium.Application.Providers.Commands.RemoveProvider;
using Emporium.Application.Providers.Commands.UpdateProvider;
using Emporium.Application.Providers.Queries.GetProviders;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Emporium.API.Endpoints;

public class ProviderEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Provider");

        _ = group.MapPost("", CreateProvider);
        _ = group.MapPut("", UpdateProvider);
        _ = group.MapDelete("", RemoveProvider);
        _ = group.MapGet("", GetProviders);
    }

    //[Authorize]
    private static async Task<IResult> CreateProvider([FromBody] CreateProviderCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToHttpResult();
    }

    //[Authorize]
    private static async Task<IResult> UpdateProvider(Guid id, [FromBody] UpdateProviderRequest request, ISender sender)
    {
        var command = new UpdateProviderCommand(
                id
                , request.Name
                , request.BankAccountNumber
                , request.BankAccountAlias
                );

        await sender.Send(command);
        return Results.Ok();
    }

    //[Authorize]
    private static async Task<IResult> RemoveProvider([FromQuery] Guid id, ISender sender)
    {
        await sender.Send(new RemoveProviderCommand(id));
        return Results.Ok();
    }

    //[Authorize]
    private static async Task<IResult> GetProviders(ISender sender)
    {
        var result = await sender.Send(new GetProvidersQuery());
        return result.ToHttpResult();
    }
}
