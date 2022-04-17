using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Infrastructure.Utility.Logging;

public class TrackPerformanceFilter : IActionFilter
{
    private PerfTracker _tracker;
    private readonly string _product;
    private readonly string _layer;

    public TrackPerformanceFilter(string product, string layer)
    {
        _product = product;
        _layer = layer;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        var activity = $"{request.Path} - {request.Method}";

        var dict = new Dictionary<string, object>();
        var values = context.RouteData.Values;
        var keys = values?.Keys;
        if (keys is not null)
        {
            foreach (var key in keys)
            {
                if (values?[key] == null) 
                    continue;

                var value = values[key]?.ToString();

                if (value is not null)
                    dict.Add($"RouteData-{key}", value);
            }

        }

        var details = WebHelper.GetWebLogDetail(_product, _layer, activity, context.HttpContext, dict);
        _tracker = new PerfTracker(details);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _tracker.Stop();
    }
}