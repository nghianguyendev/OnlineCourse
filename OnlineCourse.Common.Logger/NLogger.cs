using NLog;
using OnlineCourse.Common.Logger.Interface;
using OnlineCourse.Common.Logger.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Common.Logger
{
    public class NLogger<T> : ILog
    {

        private static readonly NLog.Logger _logger = LogManager.GetLogger(typeof(T).FullName);

        public NLogger()
        {
        }


        public void Log(LogEntry entry)
        {
            try
            {
                switch (entry.Severity)
                {
                    case LoggingEventType.Trace:
                        _logger.Trace(entry.Exception, entry.Message);
                        break;

                    case LoggingEventType.Debug:
                        _logger.Debug(entry.Exception, entry.Message);
                        break;

                    case LoggingEventType.Error:
                        string exceptionString = " Exception: " + entry.Exception?.Message;

                        if (entry.Exception is AggregateException)
                        {
                            string flattenedException = GetAggregateExceptionMessages((AggregateException)entry.Exception);
                            exceptionString += " " + flattenedException;
                        }

                        _logger.Error(entry.Exception, entry.Message + exceptionString);
                        break;

                    case LoggingEventType.Fatal:
                        exceptionString = " Exception: " + entry.Exception?.Message;

                        if (entry.Exception is AggregateException)
                        {
                            string flattenedException = GetAggregateExceptionMessages((AggregateException)entry.Exception);
                            exceptionString += " " + flattenedException;
                        }

                        _logger.Fatal(entry.Exception, entry.Message + exceptionString);
                        break;

                    case LoggingEventType.Information:
                        _logger.Info(entry.Exception, entry.Message);
                        break;

                    case LoggingEventType.Warning:
                        _logger.Warn(entry.Exception, entry.Message);
                        break;

                    default:
                        _logger.Error(entry.Exception, "Severity not set. Orig message is: " + entry.Message);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetAggregateExceptionMessages(AggregateException exception)
        {
            StringBuilder sb = new StringBuilder();

            var aggrExc = exception;
            foreach (var exc in aggrExc.Flatten().InnerExceptions)
            {
                sb.AppendLine(exc.Message);
            }

            return sb.ToString();
        }
    }
}
