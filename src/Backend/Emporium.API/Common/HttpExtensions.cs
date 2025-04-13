using Emporium.Application.Common;

namespace Emporium.API.Common;

public static class HttpExtensions
{
    public static IResult ToHttpResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok();
        }
        return Results.BadRequest(result.Error.Message);
    }

    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Data);
        }
        return Results.BadRequest(result.Error.Message);
    }
}