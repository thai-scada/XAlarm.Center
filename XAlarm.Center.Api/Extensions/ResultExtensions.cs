using Microsoft.AspNetCore.Http.HttpResults;
using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Extensions;

public static class ResultExtensions
{
    public static ProblemHttpResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess) throw new InvalidOperationException("Can't convert success result to problem");

        return TypedResults.Problem(statusCode: StatusCodes.Status400BadRequest, title: "Bad Request",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?> { { "errors", new[] { result.Error } } });
    }
}