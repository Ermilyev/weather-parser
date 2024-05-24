using Microsoft.AspNetCore.Http;

namespace Common.Domain.Primitives.Results;

public sealed class NotFoundResult() : Result(StatusCodes.Status404NotFound);

public sealed class NotFoundResult<T>() : Result<T>(StatusCodes.Status404NotFound, default);