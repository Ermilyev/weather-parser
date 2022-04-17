using System.Net;

namespace Common.Infrastructure.Utility.RestSharp.Store;

public interface IRestRepository
{
    Task<TResponse> Insert<TResponse, TRequest>(Uri url, TRequest request) 
        where TResponse : class, new() 
        where TRequest : class;
    
    Task<T> Get<T>(Uri url) 
        where T : class, new();
    
    Task<TResponse> Update<TResponse, TRequest>(Uri url, TRequest request) 
        where TResponse : class, new() 
        where TRequest : class;
    
    Task<HttpStatusCode> GetStatusConnection(Uri api);
}