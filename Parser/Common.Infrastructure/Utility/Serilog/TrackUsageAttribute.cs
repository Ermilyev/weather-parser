using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Infrastructure.Utility.Serilog;

public class TrackUsageAttribute : ActionFilterAttribute
{
    private readonly string _product;
    private readonly string _layer;
    private readonly string _activityName;

    public TrackUsageAttribute(string product, string layer, string activityName)
    {
        _product = product;
        _layer = layer;
        _activityName = activityName;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
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

        WebHelper.LogWebUsage(_product, _layer, _activityName, context.HttpContext, dict);
    }
}
