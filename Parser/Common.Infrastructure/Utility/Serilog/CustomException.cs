using System.Text;

namespace Common.Infrastructure.Utility.Serilog;

public abstract class CustomException
{
    private string? ExceptionName { get; set; }
    private string? ModuleName { get; set; }
    private string? DeclaringTypeName { get; set; }
    private string? TargetSiteName { get; set; }
    private string? Message { get; set; }
    private string? StackTrace { get; set; }
    private List<DictEntry>? Data { get; set; }
    private CustomException? InnerException { get; set; }

    public CustomException GetBaseError()
    {
        return InnerException != null ? InnerException.GetBaseError() : this;
    }

    public override string ToString()
    {
        return ToBetterString();
    }

    private string ToBetterString(string? prepend = null)
    {
        var exceptionMessage = new StringBuilder();

        exceptionMessage.Append("\n" + prepend + "Exception:" + ExceptionName);
        exceptionMessage.Append("\n" + prepend + "Message:" + Message);

        exceptionMessage.Append("\n" + prepend + "ModuleName:" + ModuleName);
        exceptionMessage.Append("\n" + prepend + "DeclaringType:" + DeclaringTypeName);
        exceptionMessage.Append("\n" + prepend + "TargetSite:" + TargetSiteName);
        exceptionMessage.Append("\n" + prepend + "StackTrace:" + StackTrace);

        exceptionMessage.Append(GetExceptionData("\n" + prepend));

        exceptionMessage.Append("\n" + prepend + "InnerException: "
                                + InnerException?.ToBetterString(prepend + "\t"));

        return exceptionMessage.ToString();
    }

    private string GetExceptionData(string prependText)
    {
        var exData = new StringBuilder();

        if (Data is null) 
            return exData.ToString();

        foreach (var item in Data.Where(a => a.Value != null))
        {
            exData.Append(prependText + $"DATA-{item.Key}:{item.Value}");
        }

        return exData.ToString();
    }
}

public abstract class DictEntry
{
    public string? Key { get; set; }
    public string? Value { get; set; }
}