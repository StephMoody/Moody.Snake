using System;
using Moody.Common.Contracts;
using NLog;

namespace Moody.Common.Logging
{
    public class LogManager : ILogManager
    {
        public void Error(Exception exception, string message = null)
        {
             Logger logger = NLog.LogManager.GetCurrentClassLogger();
             logger?.Error(exception, message);
        }
    }
}