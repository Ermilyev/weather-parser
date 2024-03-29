﻿using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Utility.Serilog;

public static class WebHelper
{
    public static void LogWebError(string product, string layer, Exception ex, HttpContext context)
    {
        var details = GetWebLogDetail(product, layer, null, context, null);
        details.Exception = ex;

        AppLogger.WriteError(details);
    }
    
    public static void LogWebUsage(string product, string layer, string activityName, HttpContext context,
        Dictionary<string, object>? additionalInfo = null)
    {
        var details = GetWebLogDetail(product, layer, activityName, context, additionalInfo);
        AppLogger.WriteUsage(details);
    }


    public static void LogWebDiagnostic(string product, string layer, string message, HttpContext context,
        Dictionary<string, object>? diagnosticInfo = null)
    {
        var details = GetWebLogDetail(product, layer, message, context, diagnosticInfo);
        AppLogger.WriteDiagnostic(details);
    }
    
    public static LogDetail GetWebLogDetail(string product, string layer, string? activityName,
        HttpContext context, Dictionary<string, object>? additionalInfo = null)
    {
        var detail = new LogDetail()
        {
            Product = product,
            Layer = layer,
            Message = activityName ?? string.Empty,
            Hostname = Environment.MachineName,
            CorrelationId = Activity.Current?.Id ?? context.TraceIdentifier,
            AdditionalInfo = additionalInfo ?? new Dictionary<string, object>()
        };

        GetUserData(detail, context);
        GetRequestData(detail, context);

        return detail;
    }

    private static void GetUserData(LogDetail detail, HttpContext context)
    {
        var userId = "";
        var userName = "";
        var user = context.User;
        {
            var i = 1;
            foreach (var claim in user.Claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.NameIdentifier:
                        userId = claim.Value;
                        break;
                    case "name":
                        userName = claim.Value;
                        break;
                    default:
                        detail.AdditionalInfo.Add($"UserClaim-{i++}-{claim.Type}", claim.Value);
                        break;
                }
            }
        }

        detail.UserId = userId;
        detail.UserName = userName;
    }

    private static void GetRequestData(LogDetail detail, HttpContext context)
    {
        var request = context.Request;
        detail.Location = request.Path;

        detail.AdditionalInfo.Add("UserAgent", request.Headers["User-Agent"]);
        detail.AdditionalInfo.Add("Languages", request.Headers["Accept-Language"]);

        var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(request.QueryString.ToString());
        foreach (var key in query.Keys)
        {
            detail.AdditionalInfo.Add($"QueryString-{key}", query[key]);
        }
    }
}
