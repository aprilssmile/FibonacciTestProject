using System;
using System.Web.Http.ExceptionHandling;

namespace WebApiApp1
{
    public class ConsoleExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Console.WriteLine(context.ExceptionContext.Exception.ToString());
        }
    }
}