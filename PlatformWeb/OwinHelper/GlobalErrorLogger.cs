
using Platform;
using Platform.Utilities;
using PlatformWeb;
using System.Web.Http.ExceptionHandling;

public class GlobalErrorLogger : ExceptionLogger
{
    public override void Log(ExceptionLoggerContext context)
    {
        var exception = context.Exception;

    //    LoggerHelper.WriteErrorToFile(exception,context.Request);
        // Write your custom logging code here
    }
}