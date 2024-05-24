using System.Text.Json.Serialization;

namespace Common.Domain.Primitives.Results;

public abstract class Result<T>(int statusCode, T data)
    : Result(statusCode)
{
    public T Data { get; } = data;
}

public abstract class Result(int statusCode)
{
    [JsonIgnore]
    public int StatusCode { get; } = statusCode;

    #region Results

    public static OkResult<T> Ok<T>(T data)
    {
        return new OkResult<T>(data);
    }
    
    public static OkResult Ok()
    {
        return new OkResult();
    }
    
    public static NotFoundResult<T> NotFound<T>()
    {
        return new NotFoundResult<T>();
    }

    public static NotFoundResult NotFound()
    {
        return new NotFoundResult();
    }
    
    public static BadRequestResult<T> BadRequest<T>()
    {
        return new BadRequestResult<T>();
    }
    
    public static BadRequestResult BadRequest()
    {
        return new BadRequestResult();
    }
    
    public static ConflictResult<T> Conflict<T>()
    {
        return new ConflictResult<T>();
    }
    
    public static ConflictResult Conflict()
    {
        return new ConflictResult();
    }

    #endregion
}