using Common.Domain.Primitives.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;

namespace Common.Infrastructure.Extensions;

public static class ResultExtensions
{
    private const string? BadRequestErrorMessage = "Bad Request";
    private const string? NotFoundErrorMessage  = "Not Found";
    private const string? ConflictErrorMessage  = "Already Exist";

    public static ActionResult ToActionResult(this Result result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => new OkResult(),
            StatusCodes.Status400BadRequest => new BadRequestObjectResult(BadRequestErrorMessage),
            StatusCodes.Status404NotFound => new NotFoundObjectResult(NotFoundErrorMessage),
            StatusCodes.Status409Conflict => new ConflictObjectResult(ConflictErrorMessage),
            _ => new StatusCodeResult(result.StatusCode)
        };
    }

    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => new OkObjectResult(result.Data),
            StatusCodes.Status400BadRequest => new BadRequestObjectResult(BadRequestErrorMessage),
            StatusCodes.Status404NotFound => new NotFoundObjectResult(NotFoundErrorMessage),
            StatusCodes.Status409Conflict => new ConflictObjectResult(ConflictErrorMessage),
            _ => new StatusCodeResult(result.StatusCode)
        };
    }
}
