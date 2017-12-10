using System;
using NLog;
using ILogger = Transport.Interfaces.ILogger;

namespace Logger
{
    public class NLogProvider : ILogger
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void Error<T>(T error)
        {
            Logger.Error(error);
        }

        public void Debug<T>(T message)
        {
            Logger.Debug(message);
        }

        public void Info<T>(T message)
        {
            Logger.Info(message);
        }

        public void Trace<T>(T message)
        {
            Logger.Trace(message);
        }
    }
}