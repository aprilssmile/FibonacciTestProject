using System;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace WebApiApp1
{
    public class ExceptionLogger : System.Web.Http.ExceptionHandling.ExceptionLogger
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(context.ExceptionContext.Exception.ToString());
        }
    }
}