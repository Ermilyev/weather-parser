using Microsoft.AspNetCore.Http;

namespace Common.Domain.Primitives.Results;

public sealed class BadRequestResult() : Result(StatusCodes.Status400BadRequest);

public sealed class BadRequestResult<T>() : Result<T>(StatusCodes.Status400BadRequest, default);