using Microsoft.AspNetCore.Http;

namespace Common.Domain.Primitives.Results;

public sealed class OkResult() : Result(StatusCodes.Status200OK);

public sealed class OkResult<T>(T data) : Result<T>(StatusCodes.Status200OK, data);