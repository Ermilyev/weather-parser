using Microsoft.AspNetCore.Http;

namespace Common.Domain.Primitives.Results;

public sealed class ConflictResult() : Result(StatusCodes.Status409Conflict);

public sealed class ConflictResult<T>() : Result<T>(StatusCodes.Status409Conflict, default);