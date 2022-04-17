using System.Net;
using Common.Infrastructure.Utility.RestSharp.Store;
using Newtonsoft.Json;
using RestSharp;

namespace Common.Infrastructure.Utility.RestSharp;

public class RestRepository :IRestRepository
{
    public async Task<TResponse> Insert<TResponse, TRequest>(Uri url, TRequest request) 
        where TResponse : class, new() where TRequest : class 
        => await PostAsync<TResponse, TRequest>(url,request);
    
    public async Task<T> Get<T>(Uri url) 
        where T : class, new() 
        => await GetAsync<T>(url);
    
    public async Task<TResponse> Update<TResponse, TRequest>(Uri url, TRequest request) 
        where TResponse : class, new() where TRequest : class 
        => await PutAsync<TResponse, TRequest>(url, request);
    

    private static async Task<T> GetAsync<T>(Uri url) 
        where T : class, new()
    {
        var client = new RestClient();
        var rest = new RestRequest(url);
        var restResponse = await client.ExecuteAsync(rest);

        if (restResponse.Content is null) 
            return new T();
        try
        {
            var response = JsonConvert.DeserializeObject<T>(restResponse.Content);
            return response ?? new T();
        }
        catch 
        {
            return new T();
        }
    }
    
    private static async Task<TResponse> PostAsync<TResponse, TRequest>(Uri url, TRequest request)
        where TResponse : class, new() where TRequest : class
    {
        var client = new RestClient();
        var rest = new RestRequest(url, Method.Post).AddJsonBody(request);
        var restResponse = await client.ExecuteAsync(rest);
        
        if (restResponse.Content is null) 
            return new TResponse();
        try
        {
            var response = JsonConvert.DeserializeObject<TResponse>(restResponse.Content);
            return response ?? new TResponse();
        }
        catch
        {
            return new TResponse();
        }
    }
    
    private static async Task<TResponse> PutAsync<TResponse, TRequest>(Uri url, TRequest request)
        where TResponse : class, new() where TRequest : class
    {
        var client = new RestClient();
        var rest = new RestRequest(url, Method.Put).AddJsonBody(request);
        var restResponse = await client.ExecuteAsync(rest);
        
        if (restResponse.Content is null) 
            return new TResponse();
        try
        {
            var response = JsonConvert.DeserializeObject<TResponse>(restResponse.Content);
            return response ?? new TResponse();
        }
        catch
        {
            return new TResponse();
        }
    }
    
    public async Task<HttpStatusCode> GetStatusConnection(Uri api)
    {
        var client = new RestClient();
        var rest = new RestRequest(api);
        var restResponse = await client.ExecuteAsync(rest);
        var statusCode = restResponse.StatusCode;
        if (statusCode is 0)
            statusCode = HttpStatusCode.RequestTimeout;
        if (statusCode is HttpStatusCode.NotFound)
            statusCode = HttpStatusCode.OK;
        return statusCode;
    }
}