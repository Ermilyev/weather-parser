using System.Web;
namespace Parser.Infrastructure;

internal static class Extensions
{
    internal static string HtmlDecode(this string text)
    {
        return string.IsNullOrWhiteSpace(text) 
            ? text 
            : HttpUtility.HtmlDecode(text.Replace("&minus;", "-"));
    }
}